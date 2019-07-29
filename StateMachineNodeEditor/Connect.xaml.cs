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
    public  partial class Connect : UserControl
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
            StartPointProperty = DependencyProperty.Register("StartPoint", typeof(Point), typeof(Connect), new FrameworkPropertyMetadata(new Point(0,0), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(StartPointChange)));
            EndPointProperty = DependencyProperty.Register("EndPoint", typeof(Point), typeof(Connect), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(EndPointChange)));
        }

        public Connect()
        {
            InitializeComponent();
            


        }
        public Connect(Connector inputConnector):this()
        {
            InputConnector = inputConnector;
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
           // update(position);
            base.OnGiveFeedback(e);
        }
        protected override void OnPreviewQueryContinueDrag(QueryContinueDragEventArgs e)
        {
            if(e.EscapePressed)
            {

            }
            base.OnPreviewQueryContinueDrag(e);
        }
        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs e)
        {
            base.OnQueryContinueDrag(e);
        }
        public void update(Point? _point=null)
        {
            Point point=new Point(0,0);
            if (_point == null)
                point = Mouse.GetPosition(nodesCanvas);
            else
            {
                point.X = _point.Value.X - 1;
                point.Y = _point.Value.Y - 1;
            }
            EndPoint = point;
            //Console.WriteLine(EndPoint.ToString());
        }
        protected void Update()
        {
            Vector different = EndPoint - StartPoint;
            //Console.WriteLine(this.Name+"  "+this.InputConnector.Name+ "  " + "StartPoint " + StartPoint.ToString());
            //Console.WriteLine(this.Name + "  " + this.InputConnector.Name + "  " + "EndPoint " + EndPoint.ToString());
            bezierSegment.Point1 = new Point(StartPoint.X + 3 * different.X / 8, StartPoint.Y + 1 * different.Y / 8);
            bezierSegment.Point2 = new Point(StartPoint.X + 5 * different.X / 8, StartPoint.Y + 7 * different.Y / 8);
        }
        public static void InputChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Connector oldNode = (e.OldValue as Connector);
            Connector newNode = (e.NewValue as Connector);
            if (oldNode != null)
                newNode.PositionChange -= connect.InputPositionChange;
            if (newNode != null)
                newNode.PositionChange += connect.InputPositionChange;
        }
        private static void OutputChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Connector oldNode = (e.OldValue as Connector);
            Connector newNode = (e.NewValue as Connector);

            if (oldNode != null)
                newNode.PositionChange -= connect.OutputPositionChange;
            if (newNode != null)
                newNode.PositionChange += connect.OutputPositionChange;
        }
        public static void EndPointChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("EndPointChange");
            Connect connect = (obj as Connect);
            connect.bezierSegment.Point3 = ((Point)e.NewValue);
            connect.Update();
        }
        private static void StartPointChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("StartPointChange");
            Connect connect = (obj as Connect);
            connect.pathFigure.StartPoint = ((Point)e.NewValue);
            connect.Update();
        }
        public void InputPositionChange(object sender, RoutedEventArgs e)
        {
           this.StartPoint = InputConnector.Position;
           Console.WriteLine("StartPoint " + StartPoint.ToString());
        }
        public void OutputPositionChange(object sender, RoutedEventArgs e)
        {        
            this.EndPoint = OutputConnector.Position;
            //Console.WriteLine("EndPoint " + EndPoint.ToString());
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            Console.WriteLine(this.Name+ " - OnRender");
            base.OnRender(drawingContext);
        }
    }
}
