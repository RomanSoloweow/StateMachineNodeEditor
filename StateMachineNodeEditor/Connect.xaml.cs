using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Media.Effects;

namespace StateMachineNodeEditor
{
    public partial class Connect : UserControl
    {

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
        }
        public Connect(Connector inputConnector) : this()
        {
            InputConnector = inputConnector;
            UpdateZoom();
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
            Connector oldNode = (e.OldValue as Connector);
            Connector newNode = (e.NewValue as Connector);
            if (oldNode != null)
            {
                oldNode.PositionChange -= connect.InputPositionChange;
                oldNode.Node.ZoomChange -= connect.ZoomChange;
            }

            if (newNode != null)
            {
                newNode.PositionChange += connect.InputPositionChange;
                newNode.Node.ZoomChange += connect.ZoomChange;
                connect.StartPoint = newNode.Position;
            }
        }
        private static void OutputChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Connector oldNode = (e.OldValue as Connector);
            Connector newNode = (e.NewValue as Connector);

            if (oldNode != null)
                oldNode.PositionChange -= connect.OutputPositionChange;
            if (newNode != null)
            {
                connect.path.StrokeDashArray = null;
                newNode.PositionChange += connect.OutputPositionChange;
                connect.EndPoint = newNode.Position;             
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
    }
}
