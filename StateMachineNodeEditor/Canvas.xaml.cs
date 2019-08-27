using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for Canvas.xaml
    /// </summary>
    public partial class Canvas : UserControl
    {
        public ObservableCollection<Node> nodes = new ObservableCollection<Node>();
        public ObservableCollection<Connect> connects = new ObservableCollection<Connect>();
        TextBox textBox = new TextBox();
        public Selector Selector;
        Point positionRightClick;
        //Point positionLeftClick;
        public UIElement parent;
        static Canvas()
        {

        }
        public Managers Manager { get; set; }
        public Canvas()
        {
            InitializeComponent();
            AllowDrop = true;
            nodes.CollectionChanged += NodesChange;
            connects.CollectionChanged += ConnectsChange;
            ContextMenu contex = new ContextMenu();
            MenuItem add = new MenuItem();
            add.Name = "Add";
            add.Header = "Add";
            add.Click += Add_Click;
            this.Focusable = true;
            contex.Items.Add(add);
            contex.Margin = new Thickness(10, 0, 0, 0);
            add.Icon = null;
            this.ContextMenu = contex;
            Manager = new Managers(this);
            Selector = new Selector(this);
            grid.Children.Add(Selector);
            //this.Children.Add(Selector);
            this.ClipToBounds = true;
            this.MouseMove += mouseMove;
            this.MouseWheel += mouseWheel;
            this.MouseUp += mouseUp;
            this.MouseDown += mouseDown;
            this.KeyUp += keyUp;
            this.KeyDown += keyDown;
            textBox.HorizontalAlignment = HorizontalAlignment.Right;
            textBox.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(textBox);
        }
        public Canvas(UIElement _parent) : this()
        {
            parent = _parent;
            //this.Background = new SolidColorBrush(Color.FromRgb(20, 20, 20));
            this.AllowDrop = true;
        }
        public void NodesChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    if (element is Node node)
                        grid.Children.Add(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var element in e.OldItems)
                {
                    if (element is Node node)
                        grid.Children.Remove(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                grid.Children.Clear();
            }
        }
        public void ConnectsChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    if (element is Connect node)
                        grid.Children.Add(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var element in e.OldItems)
                {
                    if (element is Connect node)
                        grid.Children.Remove(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var element in grid.Children)
                {
                    if (element is Connect connect)
                        grid.Children.Remove(connect);
                }
            }
        }

        public void keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                Selector.EndSelect();
            }
        }
        public void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Selector.StartSelect(Mouse.GetPosition(this));
                }
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            positionRightClick = e.GetPosition(this);
            base.OnMouseRightButtonDown(e);
        }
        public void mouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(this);
            textBox.Text = position.ToString();
            if (Mouse.Captured == this)
            {
                UnSelectedAllNodes();
            }
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Selector.StartSelect(position);
            }
        }
        public void mouseUp(object sender, MouseButtonEventArgs e)
        {
            Selector.EndSelect();
        }
        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point delta = Manager.GetDeltaMove();
                if (Mouse.Captured == this)
                {
                    if (!Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        foreach (Node node in nodes)
                        {
                            node.Selected = false;
                            node.Manager.Move(delta);
                        }
                    }
                    else
                    {
                        if (!Selector.IsVisible)
                        {
                            Selector.Visibility = Visibility.Visible;
                        }
                        Selector.Position2 = e.GetPosition(this);
                        UpdateSeletedNodes();
                    }

                }
                else
                {
                    foreach (Node node in nodes.Where(x => x.Selected == true).ToList())
                    {
                        node.Manager.Move(delta);
                    }

                }
            }
        }
        public void mouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                foreach (var node in nodes)
                {
                    node.Manager.Scale(e.Delta);
                }
            }
        }
        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData("Connect") is Connect obj)
            {
                obj.EndPoint = ForPoint.Subtraction(e.GetPosition(this), 2);
            }
            base.OnDragOver(e);
        }
        public int UpdateSeletedNodes()
        {
            Point selectorPoint1 = Selector.Position1;
            Point selectorPoint2 = Selector.Position2;
            if (nodes.Count > 0)
            {
                selectorPoint1 = ForPoint.Division(selectorPoint1, nodes.First().Manager.zoom);
                selectorPoint2 = ForPoint.Division(selectorPoint2, nodes.First().Manager.zoom);
            }
            foreach (Node node in nodes)
            {
                node.Selected = Functions.Intersect(node.Point1, node.Point2, selectorPoint1, selectorPoint2);
            }
            return 0;
        }
        public void UnSelectedAllNodes()
        {
            foreach (Node node in nodes)
            {
                node.Selected = false;
            }
        }
        public bool Check(Node from, Node to)
        {
            return (connects.Where(x => (x.InputConnector?.Node == from) && (x.OutputConnector?.Node == to)).Count() == 0);
        }

        public void Add_Click(object sender, RoutedEventArgs e)
        {
            Node node = AddNode(positionRightClick);
        }
        public Node AddNode(Point position)
        {
            Node node = new Node("State " + this.nodes.Count.ToString(), this);
            node.Name = "State" + this.nodes.Count.ToString();
            if (nodes.Count > 0)
            {
                Node firstNode = nodes.First();

            }
            ForPoint.Equality(node.Manager.translate, ForPoint.Division(position, node.Manager.zoom));
            nodes.Add(node);
            Panel.SetZIndex(node, nodes.Count());
            return node;
        }
        public Connect AddConnect(Connect connect)
        {
            connect.Name = "Connect_" + this.connects.Count.ToString();
            connects.Add(connect);
            return connect;
        }
        public Node DeleteNode(Node node)
        {
            if (node != null)
                nodes.Remove(node);
            return node;
        }
    }
}
