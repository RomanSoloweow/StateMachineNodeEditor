using System.Windows;


namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            var t = new NodeEditor2();
           // t.Background = SystemColors.ControlDarkBrush;
            t.Width = 1000;
            t.Height = 1000;
            this.Content = t;
           // Dock.Children.Add(t);
        }

        private void Ellipse_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
    }
}
