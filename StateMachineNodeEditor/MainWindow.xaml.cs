using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;

namespace StateMachineNodeEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.grid.Children.Add(new NodesCanvas(this));
            //this.grid.Children.Add(new ViewConnector());
            //this.grid.Children.Add(new ViewNode());

            ViewNodesCanvas viewNodesCanvas = new ViewNodesCanvas();
            this.grid.Children.Add(viewNodesCanvas);
            //viewNodesCanvas.DataContext = new ViewModelNodesCanvas(new ModelNodesCanvas());

            //this.grid.Children.Add(new ViewSelector());
            //this.grid.Children.Add(new ViewConnect());
            //this.grid.Children.Add(new Canvas());
        }

    }

    
}
