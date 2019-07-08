using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;

namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Manager management;
        private double zoom = 1;
        public MainWindow()
        {
            InitializeComponent();
            #region TextStyle
            Styles TextStyle = new Styles();
            TextStyle.AddSetter(TextBox.BorderBrushProperty, null);
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

            management = new Manager(this, grid);

            //Node nod = new Node("State", TextStyle);
            grid.Children.Add(new Node("State1", NodeStyle));
            grid.Children.Add(new Node("State2", NodeStyle));
            this.AllowDrop = true;
            this.MouseWheel += _MouseWheel;
            this.MouseMove += mouseMove;
        }
        public void mouseMove(object sender, MouseEventArgs e)
        {
            //if (Mouse.Captured == null)
            //  this.Cursor = Cursors.SizeAll;
        }
        private void _MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Mouse.Captured != null)
                return;
            bool Delta0 = (e.Delta == 0);
            bool DeltaMax = ((e.Delta > 0) && (zoom > Constants.ScaleMax));
            bool DeltaMin = ((e.Delta < 0) && (zoom < Constants.ScaleMin));
            if (Delta0 || DeltaMax || DeltaMin)
                return;
            zoom += (e.Delta > 0) ? Constants.scale : -Constants.scale;
            management.scale.ScaleX = zoom;
            management.scale.ScaleY = zoom;
        }
    }
}
