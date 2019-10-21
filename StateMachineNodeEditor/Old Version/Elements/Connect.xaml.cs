using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
    public partial class Connect : UserControl
    {
        public static RoutedEvent BeforeDeleteEvent;
        public event RoutedEventHandler BeforeDelete
        {
            add { base.AddHandler(BeforeDeleteEvent, value); }
            remove { base.RemoveHandler(BeforeDeleteEvent, value); }
        }
        public static readonly DependencyProperty InputConnectorProperty;
        public Connector InputConnector
        {
            get { return (Connector)GetValue(InputConnectorProperty); }
            set { SetValue(InputConnectorProperty, value); }
        }
        public NodesCanvas nodesCanvas;
        public static readonly DependencyProperty OutputConnectorProperty;
        public Connector OutputConnector
        {
            get { return (Connector)GetValue(OutputConnectorProperty); }
            set { SetValue(OutputConnectorProperty, value); }
        }

        public static readonly DependencyProperty StartPointProperty;
        public Point StartPoint
        {
            get { return (Point)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }
        public Brush Stroke
        {
            get { return path.Stroke; }
            set { path.Stroke = value; }
        }
        public static readonly DependencyProperty EndPointProperty;
        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }
        static Connect()
        {
            BeforeDeleteEvent = EventManager.RegisterRoutedEvent("BeforeDelete", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(Connect));
            InputConnectorProperty = DependencyProperty.Register("InputNode", typeof(Connector), typeof(Connect), new FrameworkPropertyMetadata(new PropertyChangedCallback(InputChange)));
            OutputConnectorProperty = DependencyProperty.Register("OutputNode", typeof(Connector), typeof(Connect), new FrameworkPropertyMetadata(new PropertyChangedCallback(OutputChange)));
            StartPointProperty = DependencyProperty.Register("StartPoint", typeof(Point), typeof(Connect), new FrameworkPropertyMetadata(new Point(0, 0), new PropertyChangedCallback(StartPointChange)));
            EndPointProperty = DependencyProperty.Register("EndPoint", typeof(Point), typeof(Connect), new FrameworkPropertyMetadata(new Point(0, 0), new PropertyChangedCallback(EndPointChange)));
        }
        private double StrokeThickness;
        public Connect()
        {
            InitializeComponent();
            StrokeThickness = path.StrokeThickness;
            this.MouseEnter += mouseEnter;
            this.MouseLeave += mouseLeave;
        }
        public Connect(Connector inputConnector) : this()
        {
            InputConnector = inputConnector;
           
            UpdateZoom();
        }
        public void mouseEnter(object sender, MouseEventArgs e)
        {
            Stroke = Brushes.Red;
        }
        public void mouseLeave(object sender, MouseEventArgs e)
        {
            Stroke = Brushes.DarkGray;
        }
        protected void Update()
        {
            Vector different = EndPoint - StartPoint;
            bezierSegment.Point1 = new Point(StartPoint.X + 3 * different.X / 8, StartPoint.Y + 1 * different.Y / 8);
            bezierSegment.Point2 = new Point(StartPoint.X + 5 * different.X / 8, StartPoint.Y + 7 * different.Y / 8);
        }
        public static void InputChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Connector oldConnector = (e.OldValue as Connector);
            Connector newConnector = (e.NewValue as Connector);
            if (oldConnector != null)
            {
                oldConnector.PositionChange -= connect.InputPositionChange;
                oldConnector.Node.ZoomChange -= connect.ZoomChange;
                oldConnector.Node.BeforeDelete -= connect.BeforeDeleteInputConnector;
            }

            if (newConnector != null)
            {
                newConnector.PositionChange += connect.InputPositionChange;
                newConnector.Node.ZoomChange += connect.ZoomChange;
                newConnector.Node.BeforeDelete += connect.BeforeDeleteInputConnector;
                connect.StartPoint = newConnector.Position;
                connect.nodesCanvas = newConnector.Node.nodesCanvas;
            }
        }
        private static void OutputChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Connector oldConnector = (e.OldValue as Connector);
            Connector newConnector = (e.NewValue as Connector);

            if (oldConnector != null)
            {
                oldConnector.PositionChange -= connect.OutputPositionChange;
                oldConnector.Node.BeforeDelete -= connect.BeforeDeleteOutputConnector;
            }
            if (newConnector != null)
            {
                connect.path.StrokeDashArray = null;
                newConnector.PositionChange += connect.OutputPositionChange;
                newConnector.Node.BeforeDelete += connect.BeforeDeleteOutputConnector;
                connect.EndPoint = newConnector.Position;
                Panel.SetZIndex(connect, Panel.GetZIndex(newConnector.Node) - 1);
            }
        }
        private void ZoomChange(object sender, RoutedEventArgs e)
        {
            UpdateZoom();
        }
        public void UpdateZoom()
        {
            this.path.StrokeThickness = StrokeThickness * InputConnector.Node.Manager.scale.ScaleX;
        }
        public static void EndPointChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            connect.bezierSegment.Point3 = ((Point)e.NewValue);
            connect.Update();
        }
        private static void StartPointChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            connect.pathFigure.StartPoint = ((Point)e.NewValue);
            connect.Update();
        }
        public void InputPositionChange(object sender, RoutedEventArgs e)
        {
           this.StartPoint = InputConnector.Position;
        }
        public void OutputPositionChange(object sender, RoutedEventArgs e)
        {        
            this.EndPoint = OutputConnector.Position;
        }
        private void BeforeDeleteInputConnector(object sender, RoutedEventArgs e)
        {
            InputConnector = null;
            Delete();
        }
        private void BeforeDeleteOutputConnector(object sender, RoutedEventArgs e)
        {
            OutputConnector = null;
            Delete();
        }
        public Connect Delete()
        {                     
            RaiseEvent(new RoutedEventArgs(BeforeDeleteEvent, this));
            nodesCanvas.DeleteConnect(this);
            return this;
        }
    }
}
