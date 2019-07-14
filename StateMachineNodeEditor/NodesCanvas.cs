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
namespace StateMachineNodeEditor
{
    public class NodesCanvas : Grid, ManagedElement
    {
        public List<Node> nodes = new List<Node>();
        static NodesCanvas()
        {
           // ControlTemplate 
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
            NodeStyle.AddSetter(Node.BorderBrushProperty, null);
            NodeStyle.AddSetter(Node.BackgroundProperty, null);
            NodeStyle.AddSetter(Node.TextWrappingProperty, TextWrapping.NoWrap);
            NodeStyle.AddSetter(Node.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            NodeStyle.AddSetter(Node.VerticalAlignmentProperty, VerticalAlignment.Top);
            NodeStyle.AddSetter(Node.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            NodeStyle.AddSetter(Node.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            NodeStyle.AddSetter(Node.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            NodeStyle.AddSetter(Node.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            NodeStyle.AddSetter(Node.BorderThicknessProperty, new Thickness(0, 0, 0, 0));
            NodeStyle.AddSetter(Node.MaxLengthProperty, 100);
            NodeStyle.AddSetter(Node.MinWidthProperty, (double)60);
            NodeStyle.AddSetter(Node.SelectionBrushProperty, new SolidColorBrush(Color.FromRgb(0, 120, 215)));
            NodeStyle.AddSetter(Node.ContextMenuProperty, null);
            NodeStyle.AddSetter(Node.DataContextProperty, null);
            NodeStyle.AddSetter(Node.CaretBrushProperty, Brushes.DarkGray);
            NodeStyle.AddSetter(Node.ForegroundProperty, Brushes.White);
            #endregion Base (TextBox)              
            #region Header
            NodeStyle.AddSetter(Node.HeaderRadiusProperty, (double)5);
            NodeStyle.AddSetter(Node.HeaderBrushProperty, (Brush)new SolidColorBrush(Color.FromRgb(18, 61, 106)));
            NodeStyle.AddSetter(Node.HeaderPenProperty, new Pen());
            #endregion  Header         
            #region Body
            NodeStyle.AddSetter(Node.BodyRadiusProperty, (double)5);
            NodeStyle.AddSetter(Node.BodyBrushProperty, (Brush)new SolidColorBrush(Color.FromRgb(45, 45, 48)));
            NodeStyle.AddSetter(Node.BodyPenProperty, new Pen());
            #endregion Body
            NodeStyle.AddSetter(Node.BorderProperty, new Thickness(10, 2, 10, 2));
            NodeStyle.AddSetter(Node.InOutTextCultureProperty, new System.Globalization.CultureInfo("en-US"));
            NodeStyle.AddSetter(Node.InOutSpaceProperty, (double)10);
            ///NodeStyle.AddSetter(Node.TemplateProperty, (double)10);
            #region Input
            #region Figure
            NodeStyle.AddSetter(Node.InputVisibleProperty, true);
            NodeStyle.AddSetter(Node.InputSizeProperty, new Size(10, 10));
            NodeStyle.AddSetter(Node.InputBrushProperty, new SolidColorBrush(Color.FromRgb(92, 83, 83)));
            NodeStyle.AddSetter(Node.InputSelectBrushProperty, Brushes.Green);
            NodeStyle.AddSetter(Node.InputPenProperty, new Pen());

            #endregion Figure
            #region Text
            NodeStyle.AddSetter(Node.InputTextProperty, "Input");
            NodeStyle.AddSetter(Node.InputTextBrushProperty, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            #endregion Text
            #endregion Input
            #region Output
            #region Figure
            NodeStyle.AddSetter(Node.OutputVisibleProperty, true);
            NodeStyle.AddSetter(Node.OutputSizeProperty, new Size(10, 10));
            NodeStyle.AddSetter(Node.OutputBrushProperty, new SolidColorBrush(Color.FromRgb(92, 83, 83)));
            NodeStyle.AddSetter(Node.OutputSelectBrushProperty, Brushes.Green);
            NodeStyle.AddSetter(Node.OutputPenProperty, new Pen());
            #endregion Figure
            #region Text
            NodeStyle.AddSetter(Node.OutputTextProperty, "Output");
            NodeStyle.AddSetter(Node.OutputTextBrushProperty, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            #endregion Text
            #endregion Output
            NodeStyle.TargetType = typeof(Node);
            Application.Current.Resources.Add(typeof(Node), NodeStyle);
            #endregion Style for class Node
        }
        public Managers Manager { get; set; }
        public NodesCanvas()
        {
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
            this.MouseDown += mouseDown;
        }
        public UIElement parent;
        public void NodeOutputClick(object sender, RoutedEventArgs e)
        {

        }
        public NodesCanvas(UIElement _parent):this()
        {
            parent = _parent;
            this.Background = Brushes.Red;
            this.AllowDrop = true;;
     
          //  this.Children.Add(new Node("State1"));
          //  this.Children.Add(new Node("State2"));
        }


        public void Add_Click(object sender, RoutedEventArgs e)
        {
            Node node = new Node("State " + this.Children.Count.ToString());
            this.Name = "State" + this.Children.Count.ToString();
            node.OutputMouseUpEvent += NodeOutputClick;
            Point position = Mouse.GetPosition(this.parent);
            node.Manager.translate.X = position.X;
            node.Manager.translate.Y = position.Y;
            this.Children.Add(node);
        }
        public void mouseDown(object sender, MouseButtonEventArgs e)
        {
            //_movePoint = null;
            //if (Mouse.Captured == null)
            //{
            //    Keyboard.ClearFocus();
            //    parent.CaptureMouse();
            //}
        }
        //public void mouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    _movePoint = null;

        //    ((UIElement)sender).ReleaseMouseCapture();
        //    ((FrameworkElement)sender).Cursor = Cursors.Arrow;
        //}

        //public void mouseMove(object sender, MouseEventArgs e)
        //{
        //    if ((Mouse.LeftButton != MouseButtonState.Pressed) || (!canMove))
        //        return;
        //    if (Mouse.Captured == parent)
        //    {
        //        if (_movePoint != null)
        //        {
        //            ((FrameworkElement)sender).Cursor = Cursors.SizeAll;
        //            Point Position = e.GetPosition(parent);
        //            double deltaX = (e.GetPosition(parent).X - _movePoint.Value.X);
        //            double deltaY = (e.GetPosition(parent).Y - _movePoint.Value.Y);
        //            bool XMax = ((deltaX > 0) && (translate.X > TranslateXMax));
        //            bool XMin = ((deltaX < 0) && (translate.X < TranslateXMin));
        //            bool YMax = ((deltaY > 0) && (translate.Y > TranslateYMax));
        //            bool YMin = ((deltaY < 0) && (translate.Y < TranslateXMin));
        //            if (XMax || XMin || YMax || YMin)
        //                return;

        //            //foreach (var children in childrens)
        //            //{
        //            //    children.Manager.translate.X += deltaX / children.Manager.scale.ScaleX;
        //            //    children.Manager.translate.Y += deltaY / children.Manager.scale.ScaleY;
        //            //}
        //            //if (test)
        //            //{
        //            translate.X += deltaX;
        //            translate.Y += deltaY;
        //            // }
        //        }
        //        _movePoint = e.GetPosition(parent);
        //    }
        //}
        //private void _MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (Mouse.Captured != null)
        //        return;
        //    bool Delta0 = (e.Delta == 0);
        //    bool DeltaMax = ((e.Delta > 0) && (zoom > ScaleMax));
        //    bool DeltaMin = ((e.Delta < 0) && (zoom < ScaleMin));
        //    if (Delta0 || DeltaMax || DeltaMin)
        //        return;

        //    zoom += (e.Delta > 0) ? scales : -scales;
        //    //foreach (var children in childrens)
        //    //{
        //    //    children.Manager.scale.ScaleX = zoom;
        //    //    children.Manager.scale.ScaleY = zoom;
        //    //}
        //    scale.ScaleX = zoom;
        //    scale.ScaleY = zoom;
        //}
    }
}
