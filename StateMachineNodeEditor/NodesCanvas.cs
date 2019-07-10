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
        public Managers Manager { get; set; }

        public UIElement parent;
        //Grid grid = new Grid();
        public NodesCanvas(UIElement _parent)
        {
            parent = _parent;
            //grid.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Background = Brushes.Red;
            this.ClipToBounds = true;
            // grid.Background = Brushes.Blue;
            //grid.ClipToBounds = true;
            #region TextStyle
            Styles TextStyle = new Styles();
            TextStyle.AddSetter(Text.BorderBrushProperty, null);
            TextStyle.AddSetter(TextBox.BackgroundProperty, null);
            TextStyle.AddSetter(TextBox.TextWrappingProperty, TextWrapping.NoWrap);
            TextStyle.AddSetter(TextBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            TextStyle.AddSetter(TextBox.VerticalAlignmentProperty, VerticalAlignment.Center);
            TextStyle.AddSetter(TextBox.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            TextStyle.AddSetter(TextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            TextStyle.AddSetter(TextBox.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            TextStyle.AddSetter(TextBox.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            TextStyle.AddSetter(TextBox.BorderThicknessProperty, new Thickness(0, 0, 0, 0));
            TextStyle.AddSetter(TextBox.MaxLengthProperty, 100);
            TextStyle.AddSetter(TextBox.SelectionBrushProperty, new SolidColorBrush(Color.FromRgb(0, 120, 215)));
            TextStyle.AddSetter(TextBox.CaretBrushProperty, Brushes.DarkGray);
            TextStyle.AddSetter(TextBox.ForegroundProperty, Brushes.White);
            #endregion
            #region NodeStyle
            Styles NodeStyle = new Styles();
            NodeStyle.AddSetter(Node.BorderBrushProperty, null);
            NodeStyle.AddSetter(Node.BackgroundProperty, null);
            NodeStyle.AddSetter(Node.TextWrappingProperty, TextWrapping.NoWrap);
            NodeStyle.AddSetter(Node.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            NodeStyle.AddSetter(Node.VerticalAlignmentProperty, VerticalAlignment.Center);
            NodeStyle.AddSetter(Node.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            NodeStyle.AddSetter(Node.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            NodeStyle.AddSetter(Node.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            NodeStyle.AddSetter(Node.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            NodeStyle.AddSetter(Node.BorderThicknessProperty, new Thickness(0, 0, 0, 0));
            NodeStyle.AddSetter(Node.MaxLengthProperty, 100);
            NodeStyle.AddSetter(Node.MinWidthProperty, (double)60);
            NodeStyle.AddSetter(Node.SelectionBrushProperty, new SolidColorBrush(Color.FromRgb(0, 120, 215)));
            NodeStyle.AddSetter(Node.CaretBrushProperty, Brushes.DarkGray);
            NodeStyle.AddSetter(Node.ForegroundProperty, Brushes.White);
            NodeStyle.AddSetter(Node.RadiusProperty, (double)5);
            NodeStyle.AddSetter(Node.BorderProperty, new Thickness(10, 2, 10, 2));
            NodeStyle.AddSetter(Node.HeaderBrushProperty, (Brush)new SolidColorBrush(Color.FromRgb(18, 61, 106)));
            NodeStyle.AddSetter(Node.HeaderPenProperty, new Pen());
            NodeStyle.AddSetter(Node.BodyBrushProperty, (Brush)new SolidColorBrush(Color.FromRgb(45, 45, 48)));
            NodeStyle.AddSetter(Node.BodyPenProperty, new Pen());
            #endregion
            Manager = new Managers(this);
            var node1 = new Node("State1", NodeStyle);
            node1.Manager.test = true;
            var node2 = new Node("State2", NodeStyle);
            node2.Manager.test = true;
            var t = new Txt();
            t.Width = 100;
            t.Height = 100;
            this.Children.Add(node1);
            this.Children.Add(node2);
            // this.Children.Add(t);
            //Manager.AddChildren(node1);
            // Manager.AddChildren(node2);
        }
    }
}
