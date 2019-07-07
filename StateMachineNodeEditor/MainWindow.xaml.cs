using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager management;
        private double zoom = 1;
        public MainWindow()
        {
            InitializeComponent();
            
            Style TextStyle = new Style();
            TextStyle.Setters.Add(new Setter { Property = TextBox.BorderBrushProperty, Value = null });
            TextStyle.Setters.Add(new Setter { Property = TextBox.BackgroundProperty, Value = null });
            TextStyle.Setters.Add(new Setter { Property = TextBox.TextWrappingProperty, Value = TextWrapping.NoWrap });
            TextStyle.Setters.Add(new Setter { Property = TextBox.HorizontalAlignmentProperty, Value = HorizontalAlignment.Center});
            TextStyle.Setters.Add(new Setter { Property = TextBox.VerticalAlignmentProperty, Value = VerticalAlignment.Center});
            TextStyle.Setters.Add(new Setter { Property = TextBox.HorizontalContentAlignmentProperty, Value = HorizontalAlignment.Center });
            TextStyle.Setters.Add(new Setter { Property = TextBox.VerticalContentAlignmentProperty, Value = VerticalAlignment.Center });
            TextStyle.Setters.Add(new Setter { Property = TextBox.HorizontalScrollBarVisibilityProperty, Value = ScrollBarVisibility.Auto });
            TextStyle.Setters.Add(new Setter { Property = TextBox.VerticalScrollBarVisibilityProperty, Value = ScrollBarVisibility.Auto });
            TextStyle.Setters.Add(new Setter { Property = TextBox.BorderThicknessProperty, Value = new Thickness(0, 0, 0, 0) });
            TextStyle.Setters.Add(new Setter { Property = TextBox.MaxLengthProperty, Value = 100 });
            TextStyle.Setters.Add(new Setter { Property = TextBox.SelectionBrushProperty, Value = new SolidColorBrush(Color.FromRgb(0, 120, 215)) });
            TextStyle.Setters.Add(new Setter { Property = TextBox.CaretBrushProperty, Value = Brushes.DarkGray });
            TextStyle.Setters.Add(new Setter { Property = TextBox.ForegroundProperty, Value = Brushes.White });
            //TextStyle.Setters.Add(new Setter { Property = TextBox.ContextMenuProperty, Value = null });
            //this.Resources.Add("TextStyle", TextStyle);
         
            management = new Manager(this,grid);

           //Node nod = new Node("State", TextStyle);
            grid.Children.Add(new Node("State1", TextStyle));
            grid.Children.Add(new Node("State2", TextStyle));
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
