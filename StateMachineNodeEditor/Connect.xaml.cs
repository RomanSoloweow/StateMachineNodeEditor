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
        public Point position;
        public Point StartPoint
        {
            get { return pathFigure.StartPoint; }
            set { pathFigure.StartPoint = value; Update();}
        }
        public Brush Stroke
        {
            get { return path.Stroke; }
            set { path.Stroke = value; }
        }
        public Point EndPoint
        {
            get { return bezierSegment.Point3; }
            set { bezierSegment.Point3 = value; Update(); }
        }
        static Connect()
        {
            InputConnectorProperty = DependencyProperty.Register("InputNode", typeof(Connector), typeof(Connect), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(InputChange)));
            OutputConnectorProperty = DependencyProperty.Register("OutputNode", typeof(Connector), typeof(Connect), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OutputChange)));
        }

        public Connect()
        {
            InitializeComponent();
        
        }
        public void OnThumbDragStarted(object sender, DragStartedEventArgs args)
        {
            
        }
        public void OnThumbDragDelta(object sender, DragDeltaEventArgs args)
        {
            update();
        }
        public Connect(Connector inputConnector):this()
        {
            InputConnector = inputConnector;
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            update(position);
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
        private void InputPositionChange(object sender, EventArgs e)
        {
            StartPoint = InputConnector.Position;
        }
        private void OutputPositionChange(object sender, EventArgs e)
        {
            EndPoint = OutputConnector.Position;
        }
    }
}
