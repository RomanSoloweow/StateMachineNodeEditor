using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using StateMachineNodeEditor.View;
namespace StateMachineNodeEditor
{
    public partial class MainWindow : Window
    {
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }
        public MainWindow()
        {

            InitializeComponent();



            ViewNodesCanvas viewNodesCanvas = new ViewNodesCanvas()
            {
                ViewModel = new ViewModel.ViewModelNodesCanvas()
            };
            this.grid.Children.Add(viewNodesCanvas);

            //ViewConnect viewConnect = new ViewConnect()
            //{
            //    ViewModel = new ViewModel.ViewModelConnect()
            //};
            //this.grid.Children.Add(viewConnect);



            //this.grid.Children.Add(new NodesCanvas(this));
            //this.grid.Children.Add(new ViewConnector());
            //this.grid.Children.Add(new ViewNode());


            //ViewNodesCanvas viewNodesCanvas = new ViewNodesCanvas();
            //this.grid.Children.Add(viewNodesCanvas);
            //viewNodesCanvas.DataContext = new ViewModelNodesCanvas(new ModelNodesCanvas());

            //this.grid.Children.Add(new ViewSelector());

            //ViewConnect viewConnect = new ViewConnect();
            //this.grid.Children.Add(viewNodesCanvas);
            //this.grid.Children.Add(viewConnect);
            //this.grid.Children.Add(new Canvas());
        }

    }

    
}
