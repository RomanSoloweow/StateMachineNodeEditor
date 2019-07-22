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
    public class NodesCanvas : Grid, ManagedElement
    {
        //public ObservableCollection<Nodess> nodes = new ObservableCollection<Nodess>();
        public ObservableCollection<UserControl> nodes = new ObservableCollection<UserControl>();
        public ObservableCollection<Connect> connects = new ObservableCollection<Connect>();

        static NodesCanvas()
        {
            #region Style for class Text (TextBox)
            Styles TextStyle = new Styles();
            TextStyle.AddSetter(Text.BorderBrushProperty, null);
            TextStyle.AddSetter(Text.BackgroundProperty, null);
            TextStyle.AddSetter(Text.TextWrappingProperty, TextWrapping.NoWrap);
            TextStyle.AddSetter(Text.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            TextStyle.AddSetter(Text.VerticalAlignmentProperty, VerticalAlignment.Center);
            TextStyle.AddSetter(Text.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            TextStyle.AddSetter(Text.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            TextStyle.AddSetter(Text.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            TextStyle.AddSetter(Text.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            TextStyle.AddSetter(Text.BorderThicknessProperty, new Thickness(0, 0, 0, 0));
            TextStyle.AddSetter(Text.MaxLengthProperty, 100);
            TextStyle.AddSetter(Text.SelectionBrushProperty, new SolidColorBrush(Color.FromRgb(0, 120, 215)));
            TextStyle.AddSetter(Text.CaretBrushProperty, Brushes.DarkGray);
            TextStyle.AddSetter(Text.ForegroundProperty, Brushes.White);
            TextStyle.TargetType = typeof(Text);
            Application.Current.Resources.Add(typeof(Text), TextStyle);
            #endregion Style for class Text (TextBox)
            #region Style for class Node
            Styles NodeStyle = new Styles();
            #region Base (TextBox)
            NodeStyle.AddSetter(Nodess.BorderBrushProperty, null);
            NodeStyle.AddSetter(Nodess.BackgroundProperty, null);
            NodeStyle.AddSetter(Nodess.TextWrappingProperty, TextWrapping.NoWrap);
            NodeStyle.AddSetter(Nodess.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            NodeStyle.AddSetter(Nodess.VerticalAlignmentProperty, VerticalAlignment.Top);
            NodeStyle.AddSetter(Nodess.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            NodeStyle.AddSetter(Nodess.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            NodeStyle.AddSetter(Nodess.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            NodeStyle.AddSetter(Nodess.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            NodeStyle.AddSetter(Nodess.BorderThicknessProperty, new Thickness(0, 0, 0, 0));
            NodeStyle.AddSetter(Nodess.MaxLengthProperty, 100);
            NodeStyle.AddSetter(Nodess.MinWidthProperty, (double)60);
            NodeStyle.AddSetter(Nodess.SelectionBrushProperty, new SolidColorBrush(Color.FromRgb(0, 120, 215)));
            NodeStyle.AddSetter(Nodess.ContextMenuProperty, null);
            NodeStyle.AddSetter(Nodess.DataContextProperty, null);
            NodeStyle.AddSetter(Nodess.CaretBrushProperty, Brushes.DarkGray);
            NodeStyle.AddSetter(Nodess.ForegroundProperty, Brushes.White);
            #endregion Base (TextBox)              
            #region Header
            NodeStyle.AddSetter(Nodess.HeaderRadiusProperty, (double)5);
            NodeStyle.AddSetter(Nodess.HeaderBrushProperty, (Brush)new SolidColorBrush(Color.FromRgb(18, 61, 106)));
            NodeStyle.AddSetter(Nodess.HeaderPenProperty, new Pen());
            #endregion  Header         
            #region Body
            NodeStyle.AddSetter(Nodess.BodyRadiusProperty, (double)5);
            NodeStyle.AddSetter(Nodess.BodyBrushProperty, (Brush)new SolidColorBrush(Color.FromRgb(45, 45, 48)));
            NodeStyle.AddSetter(Nodess.BodyPenProperty, new Pen());
            #endregion Body
            NodeStyle.AddSetter(Nodess.BorderProperty, new Thickness(10, 2, 10, 2));
            NodeStyle.AddSetter(Nodess.InOutTextCultureProperty, new System.Globalization.CultureInfo("en-US"));
            NodeStyle.AddSetter(Nodess.InOutSpaceProperty, (double)10);
            ///NodeStyle.AddSetter(Node.TemplateProperty, (double)10);
            #region Input
            #region Figure
            NodeStyle.AddSetter(Nodess.InputVisibleProperty, true);
            NodeStyle.AddSetter(Nodess.InputSizeProperty, new Size(10, 10));
            NodeStyle.AddSetter(Nodess.InputBrushProperty, new SolidColorBrush(Color.FromRgb(92, 83, 83)));
            NodeStyle.AddSetter(Nodess.InputSelectBrushProperty, Brushes.Green);
            NodeStyle.AddSetter(Nodess.InputPenProperty, new Pen());

            #endregion Figure
            #region Text
            NodeStyle.AddSetter(Nodess.InputTextProperty, "Input");
            NodeStyle.AddSetter(Nodess.InputTextBrushProperty, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            #endregion Text
            #endregion Input
            #region Output
            #region Figure
            NodeStyle.AddSetter(Nodess.OutputVisibleProperty, true);
            NodeStyle.AddSetter(Nodess.OutputSizeProperty, new Size(10, 10));
            NodeStyle.AddSetter(Nodess.OutputBrushProperty, new SolidColorBrush(Color.FromRgb(92, 83, 83)));
            NodeStyle.AddSetter(Nodess.OutputSelectBrushProperty, Brushes.Green);
            NodeStyle.AddSetter(Nodess.OutputPenProperty, new Pen());
            #endregion Figure
            #region Text
            NodeStyle.AddSetter(Nodess.OutputTextProperty, "Output");
            NodeStyle.AddSetter(Nodess.OutputTextBrushProperty, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            #endregion Text
            #endregion Output
            NodeStyle.TargetType = typeof(Nodess);
            Application.Current.Resources.Add(typeof(Nodess), NodeStyle);
            #endregion Style for class Node

            #region Style for class Connect
            Styles ConnectStyle = new Styles();
            ConnectStyle.AddSetter(Connect.BorderBrushProperty, null);
            ConnectStyle.AddSetter(Connect.BackgroundProperty, null);
            ConnectStyle.AddSetter(Connect.TextWrappingProperty, TextWrapping.NoWrap);
            ConnectStyle.AddSetter(Connect.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            ConnectStyle.AddSetter(Connect.VerticalAlignmentProperty, VerticalAlignment.Top);
            ConnectStyle.AddSetter(Connect.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            ConnectStyle.AddSetter(Connect.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            ConnectStyle.AddSetter(Connect.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            ConnectStyle.AddSetter(Connect.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            ConnectStyle.AddSetter(Connect.BorderThicknessProperty, new Thickness(0, 0, 0, 0));
            ConnectStyle.AddSetter(Connect.MaxLengthProperty, 100);
            ConnectStyle.AddSetter(Connect.MinWidthProperty, (double)60);
            ConnectStyle.AddSetter(Connect.SelectionBrushProperty, new SolidColorBrush(Color.FromRgb(0, 120, 215)));
            ConnectStyle.AddSetter(Connect.ContextMenuProperty, null);
            ConnectStyle.AddSetter(Connect.DataContextProperty, null);
            ConnectStyle.AddSetter(Connect.CaretBrushProperty, Brushes.DarkGray);
            ConnectStyle.AddSetter(Connect.ForegroundProperty, Brushes.White);
            ConnectStyle.TargetType = typeof(Connect);

            Application.Current.Resources.Add(typeof(Connect), ConnectStyle);
            #endregion Style for class Connect
        }
        public Managers Manager { get; set; }
        public NodesCanvas()
        {
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
            Ellipse ellipse = sender as Ellipse;
            //UserControl1 outputNode = sender as UserControl1;
          
            var t = Mouse.GetPosition(this);
            // outputNode.UpdateOutputCenterLocation();
            Connect connect = AddConnect(t);
           // connect.InputNode = outputNode;
        }
        public void NodeMove(object sender, EventArgs e)
        {
            Console.WriteLine("Изменилась Локация");
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
                    if (element is UserControl1 node)
                        this.Children.Add(node);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var element in e.OldItems)
                {
                    if (element is UserControl1 node)
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
            AddNodes(position);
        }
        public Nodess AddNode(Point position)
        {
            Nodess node = new Nodess("State " + this.nodes.Count.ToString());
            this.Name = "State" + this.nodes.Count.ToString();
            node.OutputMouseUpEvent += NodeOutputClick;
            node.LocationChangeEvent += NodeMove;
            node.Manager.translate.X = position.X;
            node.Manager.translate.Y = position.Y;
           // nodes.Add(node);
            return node;
        }
        public UserControl1 AddNodes(Point position)
        {
            UserControl1 node = new UserControl1("State " + this.nodes.Count.ToString());
            this.Name = "State" + this.nodes.Count.ToString();
            node.OutputForm.MouseDown += NodeOutputClick;
            //node.LocationChangeEvent += NodeMove;
            node.Manager.translate.X = position.X;
            node.Manager.translate.Y = position.Y;
            nodes.Add(node);
            return node;
        }
        public Connect AddConnect(Point position)
        {
            Connect connect = new Connect(position, "Connect " + this.connects.Count.ToString());
            this.Name = "Connect" + this.connects.Count.ToString();
            connects.Add(connect);
            return connect;
        }
    }
}
