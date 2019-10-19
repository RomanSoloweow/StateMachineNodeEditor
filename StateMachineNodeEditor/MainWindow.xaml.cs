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
        public MainWindow()
        {

            InitializeComponent();
            Header.PreviewMouseLeftButtonDown += HeaderClick;
            ButtonClose.Click += ButtonCloseClick;
            ButtonMin.Click += ButtonMinClick;
            ButtonMax.Click += ButtonMaxClick;
            //Header.PreviewMouseDoubleClick += MainMenuMouseDoubleClick;
            //ViewNodesCanvas viewNodesCanvas = new ViewNodesCanvas()
            //{
            //    ViewModel = new ViewModel.ViewModelNodesCanvas()
            //};
            //this.grid.Children.Add(viewNodesCanvas);

            //ViewConnect viewConnect = new ViewConnect()
            //{
            //    ViewModel = new ViewModel.ViewModelConnect()
            //};
            //this.grid.Children.Add(viewConnect);


            ////old version

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

        void StateNormalMaximaze()
        {
            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
        void ButtonCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void ButtonMinClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        void ButtonMaxClick(object sender, RoutedEventArgs e)
        {
            StateNormalMaximaze();
        }
        
        private void HeaderClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is DockPanel)
            {
                if (e.ClickCount == 1)
                {

                    if (this.WindowState == WindowState.Maximized)
                    {
                        var point = PointToScreen(e.MouseDevice.GetPosition(this));

                        if (point.X <= RestoreBounds.Width / 2)
                            Left = 0;
                        else if (point.X >= RestoreBounds.Width)
                            Left = point.X - (RestoreBounds.Width - (this.ActualWidth - point.X));
                        else
                            Left = point.X - (RestoreBounds.Width / 2);

                        Top = point.Y - (((FrameworkElement)sender).ActualHeight / 2);
                        WindowState = WindowState.Normal;
                    }

                    this.DragMove();
                }
                else
                {
                    StateNormalMaximaze();  
                }
                e.Handled = true;
            }
        }
    }
}
