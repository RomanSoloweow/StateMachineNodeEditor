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
    public class NodesCanvas : Grid
    {
        public ObservableCollection<Node> nodes = new ObservableCollection<Node>();
        public ObservableCollection<Connect> connects = new ObservableCollection<Connect>();
        TextBox textBox = new TextBox();
        public MouseEventHandler Moves;
        Point position_click;
        static NodesCanvas()
        {

        }
        public Managers Manager { get; set; }
        protected override void OnDragOver(DragEventArgs e)
        {
           if( e.Data.GetData("object") is Connect obj)
            {
                obj.EndPoint= e.GetPosition(this);
            }
            base.OnDragOver(e);
        }
        public NodesCanvas()
        {
            AllowDrop = true;
            nodes.CollectionChanged += NodesChange;
            connects.CollectionChanged += ConnectsChange;           
            ContextMenu contex = new ContextMenu();
            MenuItem add = new MenuItem();
            add.Name = "Add";
            add.Header = "Add";
            add.Click += Add_Click;
            contex.Items.Add(add);
            contex.Margin = new Thickness(10, 0, 0, 0);
            add.Icon = null;
            this.ContextMenu = contex;
            Manager = new Managers(this);
            this.ClipToBounds = true;
            this.MouseMove += Move;
            textBox.HorizontalAlignment = HorizontalAlignment.Right;
            textBox.VerticalAlignment = VerticalAlignment.Top;
            this.Children.Add(textBox);
            //UserControl3 userControl3 = new UserControl3();
            //userControl3.StartPoint = new Point(0, 0);
            //userControl3.EndPoint = new Point(500, 500);
            //userControl3.Stroke = Brushes.White;
            //this.Children.Add(userControl3);


            //this.Children.Add(new UserControl1());
        }
        public NodesCanvas(UIElement _parent) : this()
        {
            parent = _parent;
            this.Background = Brushes.Red;
            this.AllowDrop = true;
        }
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            position_click = e.GetPosition(this);
            base.OnMouseRightButtonDown(e);
        }
        public UIElement parent;
        public void Move(object sender, EventArgs e)
        {
            //Console.WriteLine("Двигаем мышь");
        }
        public void NodeMove(object sender, EventArgs e)
        {
            //Console.WriteLine("Изменилась Локация");
        }
        public void NodesChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    if (element is Node node)
                        this.Children.Add(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var element in e.OldItems)
                {
                    if (element is Node node)
                        this.Children.Remove(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.Children.Clear();
            }
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            textBox.Text = e.GetPosition(this).ToString();
            base.OnMouseDown(e);

        }
        public void ConnectsChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    if (element is Connect node)
                        this.Children.Add(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var element in e.OldItems)
                {
                    if (element is Connect node)
                        this.Children.Remove(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var element in this.Children)
                {
                    if (element is Connect connect)
                        this.Children.Remove(connect);
                }
            }
        }


        public void Add_Click(object sender, RoutedEventArgs e)
        {
            Node node = AddNodes(position_click);
        }
        public Node AddNodes(Point position)
        {
            Node node = new Node("State " + this.nodes.Count.ToString(),this);
            this.Name = "State" + this.nodes.Count.ToString();
            node.Manager.translate.X = position.X;
            node.Manager.translate.Y = position.Y;
            nodes.Add(node);

            return node;
        }
        public Connect AddConnect(Connect connect)
        {          
            connect.Name = "Connect_" + this.connects.Count.ToString();
            connects.Add(connect);
            //int k = Panel.GetZIndex(connect);
            Panel.SetZIndex(connect, 2);
            return connect;
        }
    }
}
