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
    public partial class NodesCanvas : UserControl
    {
        public ObservableCollection<Node> nodes = new ObservableCollection<Node>();
        public ObservableCollection<Connect> connects = new ObservableCollection<Connect>();
        public Selector Selector;
        Point positionRightClick;
        //Point positionLeftClick;
        public UIElement parent;
        static NodesCanvas()
        {

        }
        public Managers Manager { get; set; }
        public NodesCanvas()
        {
            InitializeComponent();      
            Manager = new Managers(this);
            Selector = new Selector(this);
            grid.Children.Add(Selector);
            nodes.CollectionChanged += NodesChange;
            connects.CollectionChanged += ConnectsChange;
            this.MouseMove += OnMouseMove;
            this.MouseWheel += OnMouseWheel;
            this.MouseLeftButtonDown += OnMouseLeftDown;
            this.MouseRightButtonDown += OnMouseRightDown;
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
        #region Events for mouse
       
        public void OnMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            positionRightClick = e.GetPosition(this);
        }
        public void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(this);
            TextBoxPosition.Text = position.ToString();
            if (Mouse.Captured == this)
            {
                UnSelectedAllNodes();
            }
        }
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point delta = Manager.GetDeltaMove();
                if (Mouse.Captured == this)
                {
                    foreach (Node node in nodes)
                    {
                        node.Selected = false;
                        node.Manager.Move(delta);
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
        public void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            foreach (var node in nodes)
            {
                node.Manager.Scale(e.Delta);
            }
        }
        //public void DragOver(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetData("Connect") is Connect obj)
        //    {
        //        obj.EndPoint = ForPoint.Subtraction(e.GetPosition(this), 2);
        //    }
        //}
        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData("Connect") is Connect obj)
            {
                obj.EndPoint = ForPoint.Subtraction(e.GetPosition(this), 2);
            }
            base.OnDragOver(e);
        }
        #endregion Events for mouse
        #region Commands
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Clipboard operation occured!");
        }
        private void Select(object sender, ExecutedRoutedEventArgs e)
        {
            this.Selector.StartSelect(Mouse.GetPosition(this));
        }
        private void SelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            this.SelectedAllNodes();
        }
        private void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedNodes().ForEach(x => x.Delete());
        }
        private void New(object sender, ExecutedRoutedEventArgs e)
        {
            Point point = new Point();
            if (positionRightClick != point)
                point = positionRightClick;
            else
                point = Mouse.GetPosition(this);

            GetNewNode(point);
            positionRightClick = new Point();
        }
    #endregion Commands
        #region Work with Select
        public int UpdateSeletedNodes()
        {
            Point selectorPoint1 = Selector.Position1;
            Point selectorPoint2 = Selector.Position2;
            if (nodes.Count > 0)
            {
                selectorPoint1 = ForPoint.Division(selectorPoint1, nodes.First().Manager.Zoom);
                selectorPoint2 = ForPoint.Division(selectorPoint2, nodes.First().Manager.Zoom);
            }
            foreach (Node node in nodes)
            {
                node.Selected = Functions.Intersect(node.Point1, node.Point2, selectorPoint1, selectorPoint2);
            }
            return 0;
        }
        private List<Node> GetSelectedNodes()
        {
            return nodes.Where(x => x.Selected == true).ToList();
        }
      
        public void UnSelectedAllNodes()
        {
            foreach (Node node in nodes)
            {
                node.Selected = false;
            }
        }
        public void SelectedAllNodes()
        {
            foreach (Node node in nodes)
            {
                node.Selected = true;
            }
        }
        public bool Check(Node from, Node to)
        {
            return (connects.Where(x => (x.InputConnector?.Node == from) && (x.OutputConnector?.Node == to)).Count() == 0);
        }
        #endregion Work with Select

        //private List<Connect> GetConnectsWithThisNode(Node node)
        //{
        //    return connects.Where(x => (x.InputConnector?.Node == node) || (x.OutputConnector?.Node == node)).ToList();
        //}

        public Connect GetNewConnect(Point position)
        {
            Connect connect = new Connect
            {
                Name = "Connect_" + this.connects.Count.ToString()
            };
            connects.Add(connect);
            connect.StartPoint = position;
            return connect;
        }
        public Connect DeleteConnect(Connect connect)
        {
            if (connect != null)
            {
                connects.Remove(connect);
            }
            return connect;
        }
        public Node GetNewNode(Point position)
        {
            Node node = new Node("State " + this.nodes.Count.ToString(), this)
            {
                Name = "State" + this.nodes.Count.ToString()
            };
            ForPoint.Equality(node.Manager.translate, ForPoint.Division(position, node.Manager.Zoom));
            nodes.Add(node);
            Panel.SetZIndex(node, nodes.Count());
            return node;
        }  
        public Node DeleteNode(Node node)
        {
            if (node != null)
            {
                nodes.Remove(node);
            }
            return node;
        }
    }
}
