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
        //public ObservableCollection<Nodess> nodes = new ObservableCollection<Nodess>();
        public ObservableCollection<Node> nodes = new ObservableCollection<Node>();
        public ObservableCollection<Connect> connects = new ObservableCollection<Connect>();
        // public ObservableCollection<Connect> connects = new ObservableCollection<Connect>();
        public MouseEventHandler Moves;
        static NodesCanvas()
        {

        }
        public Managers Manager { get; set; }
        protected override void OnDragOver(DragEventArgs e)
        {
           if( e.Data.GetData("object") is Connect obj)
            {
                obj.position= e.GetPosition(this);
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
        public UIElement parent;
        //public void NodeOutputClick(object sender, RoutedEventArgs e)
        //{
        //   Nodess outputNode = sender as Nodess;
        //   outputNode.UpdateOutputCenterLocation();
        //   Connect connect= AddConnect(outputNode.OutputCenterLocation);
        //   connect.InputNode = outputNode;
        //}
        public void NodeOutputClick(object sender, RoutedEventArgs e)
        {
            //UserControl1 outputNode = sender as UserControl1;
          
            var t = Mouse.GetPosition(this);
            // outputNode.UpdateOutputCenterLocation();
           // Connect connect = AddConnect(t);
           // connect.InputNode = outputNode;
        }
        public void Move(object sender, EventArgs e)
        {
            //Console.WriteLine("Двигаем мышь");
        }
        public void NodeMove(object sender, EventArgs e)
        {
            //Console.WriteLine("Изменилась Локация");
        }
        //public void NodesChange(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if(e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (var element in e.NewItems)
        //        {
        //            if(element is Nodess node)
        //            this.Children.Add(node);
        //        }
        //    }
        //    else if (e.Action == NotifyCollectionChangedAction.Remove)
        //    {
        //        foreach (var element in e.OldItems)
        //        {
        //            if (element is Nodess node)
        //                this.Children.Remove(node);
        //        }
        //    }
        //    else if (e.Action == NotifyCollectionChangedAction.Reset)
        //    {
        //        this.Children.Clear();
        //    }
        //}
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
            Point position = Mouse.GetPosition(this.parent);
            //AddNode(position);
            Node node =  AddNodes(position);

        }
        //public Nodess AddNode(Point position)
        //{
        //   // Nodess node = new Nodess("State " + this.nodes.Count.ToString());
        //   // this.Name = "State" + this.nodes.Count.ToString();
        //   // node.OutputMouseUpEvent += NodeOutputClick;
        //   // node.LocationChangeEvent += NodeMove;
        //   // node.Manager.translate.X = position.X;
        //   // node.Manager.translate.Y = position.Y;
        //   //// nodes.Add(node);
        //    return null;
        //}
        //public Connect AddConnect(Point position)
        //{
        //    Connect connect = new Connect(position, "Connect " + this.connects.Count.ToString());

        //    this.Name = "Connect" + this.connects.Count.ToString();
        //    connects.Add(connect);
        //    return connect;
        //}
        public Node AddNodes(Point position)
        {
            Node node = new Node("State " + this.nodes.Count.ToString(),this);
            this.Name = "State" + this.nodes.Count.ToString();
            node.OutputForm.MouseDown += NodeOutputClick;
            this.Moves += node.Moves;
           // this.PreviewMouseMove += NodeMove;
            //node.LocationChangeEvent += NodeMove;
            node.Manager.translate.X = position.X;
            node.Manager.translate.Y = position.Y;
            nodes.Add(node);

            return node;
        }
        public Connect AddConnect(Connector userControl2,Point position)
        {
            Connect connect = new Connect(userControl2);

            connect.StartPoint = position;
            connect.Stroke = Brushes.White;

           // this.PreviewMouseMove += connect.HeaderMouseMove;    
            connects.Add(connect);
            return connect;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                Moves.Invoke(this, e);

        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

    }
}
