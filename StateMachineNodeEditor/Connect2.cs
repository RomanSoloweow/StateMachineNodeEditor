using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace StateMachineNodeEditor
{
    public class Connect2:Grid
    {
        public static readonly DependencyProperty InputNodeProperty;
        public Connector InputNode
        {
            get { return (Connector)GetValue(InputNodeProperty); }
            set { SetValue(InputNodeProperty, value); }
        }
        public static readonly DependencyProperty OutputNodeProperty;
        public Connector OutputNode
        {
            get { return (Connector)GetValue(OutputNodeProperty); }
            set { SetValue(OutputNodeProperty, value); }
        }
        public Point position;
        public Point StartPoint
        {
            get { return pathFigure.StartPoint; }
            set { pathFigure.StartPoint = value; Update(); }
        }
        public Brush Stroke
        {
            get { return path.Stroke; }
            set { path.Stroke = value;  }
        }
        public Point EndPoint
        {
            get { return bezierSegment.Point3; }
            set { bezierSegment.Point3 = value; Update(); }
        }

        Path path = new Path();
        PathGeometry pathGeometry = new PathGeometry();
        PathFigure pathFigure = new PathFigure();
        BezierSegment bezierSegment = new BezierSegment();
       
        static Connect2()
        {
            InputNodeProperty = DependencyProperty.Register("InputNode", typeof(Connector), typeof(Connect), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(InputShange)));
            OutputNodeProperty = DependencyProperty.Register("OutputNode", typeof(Connector), typeof(Connect), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OutputShange)));
        }
        public Connect2() : base()
        {
            pathFigure.Segments.Add(bezierSegment);
            pathGeometry.Figures.Add(pathFigure);
            path.Data= pathGeometry;

            path.Stroke = Brushes.White;
            pathFigure.StartPoint = new Point(0, 0);
            bezierSegment.Point3 = new Point(500, 500);
            pathFigure.IsClosed = false;
            Update();
            this.Children.Add(path);
        }

        public Connect2(Connector userControl2) : this()
        {
            InputNode = userControl2;
        }
        public Connect2(Point point,Connector userControl2) : this(userControl2)
        {
            StartPoint = point;
            EndPoint = point;
        }
        public void update(Point? _point = null)
        {
            Point point = new Point(0, 0);
            if (_point == null)
                point = Mouse.GetPosition(InputNode.Node.nodesCanvas);
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

