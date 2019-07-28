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
    public  partial class UserControl3 : UserControl
    {
        public static readonly DependencyProperty InputNodeProperty;
        public UserControl2 InputNode
        {
            get { return (UserControl2)GetValue(InputNodeProperty); }
            set { SetValue(InputNodeProperty, value); }
        }
        public static readonly DependencyProperty OutputNodeProperty;
        public UserControl2 OutputNode
        {
            get { return (UserControl2)GetValue(OutputNodeProperty); }
            set { SetValue(OutputNodeProperty, value); }
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
        static UserControl3()
        {
            InputNodeProperty = DependencyProperty.Register("InputNode", typeof(UserControl2), typeof(UserControl3), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(InputShange)));
            OutputNodeProperty = DependencyProperty.Register("OutputNode", typeof(UserControl2), typeof(UserControl3), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OutputShange)));
        }

        public UserControl3()
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
        public UserControl3(UserControl2 userControl2):this()
        {
            InputNode = userControl2;
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            update(position);
            base.OnGiveFeedback(e);
        }
        protected override void OnPreviewQueryContinueDrag(QueryContinueDragEventArgs e)
        {
            var t1 = Mouse.GetPosition(this);
            var t2 = Mouse.GetPosition(InputNode);
            var t3 = Mouse.GetPosition(InputNode.node);
            var t4 = Mouse.GetPosition(InputNode.node.nodesCanvas);
            base.OnPreviewQueryContinueDrag(e);
        }
        public void update(Point? _point=null)
        {
            Point point=new Point(0,0);
            if (_point == null)
                point = Mouse.GetPosition(InputNode.node.nodesCanvas);
            else
            {
                point.X = _point.Value.X - 1;
                point.Y = _point.Value.Y - 1;
            }
            EndPoint = point;
            //Console.WriteLine(EndPoint.ToString());
        }
        public void HeaderMouseMove(object sender, MouseEventArgs e)
        {
            update();
        }
        protected void Update()
        {
            Vector different = EndPoint - StartPoint;
            bezierSegment.Point1 = new Point(StartPoint.X + 3 * different.X / 8, StartPoint.Y + 1 * different.Y / 8);
            bezierSegment.Point2 = new Point(StartPoint.X + 5 * different.X / 8, StartPoint.Y + 7 * different.Y / 8);
        }
        public static void InputShange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            //Connect connect = (obj as Connect);
            //Nodess oldNode = (e.OldValue as Nodess);
            //Nodess newNode = (e.NewValue as Nodess);

            //if (oldNode != null)
            //    oldNode.LocationChangeEvent -= connect.InputLocationChange;
            //if (newNode != null)
            //    newNode.LocationChangeEvent += connect.InputLocationChange;
        }
        private static void OutputShange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            //Connect connect = (obj as Connect);
            //Nodess oldNode = (e.OldValue as Nodess);
            //Nodess newNode = (e.NewValue as Nodess);

            //if (oldNode != null)
            //    oldNode.LocationChangeEvent -= connect.InputLocationChange;
            //if (newNode != null)
            //    newNode.LocationChangeEvent += connect.InputLocationChange;
        }
        private void InputLocationChange(object sender, EventArgs e)
        {
 
        }
        private void OutputLocationChange(object sender, EventArgs e)
        {

        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            var t = Mouse.DirectlyOver;
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            update();
            // EndPoint = e.GetPosition(InputNode.node.nodesCanvas);
            base.OnMouseMove(e);

        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }
    }
}
