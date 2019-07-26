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
using System.Windows.Media.Effects;
using System.Globalization;
namespace StateMachineNodeEditor
{
   public class Connect: Text
    {
        public static readonly DependencyProperty InputNodeProperty;
        public Nodess InputNode
        {
            get { return (Nodess)GetValue(InputNodeProperty); }
            set { SetValue(InputNodeProperty, value); }
        }
        public static readonly DependencyProperty OutputNodeProperty;
        public Nodess OutputNode
        {
            get { return (Nodess)GetValue(OutputNodeProperty); }
            set { SetValue(OutputNodeProperty, value); }
        }
        public static readonly DependencyProperty EndPointProperty;
        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }
        public static readonly DependencyProperty StartPointProperty;
        public Point StartPoint
        {
            get { return (Point)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }
        PathGeometry path = new PathGeometry();
        BezierSegment bezierSegment = new BezierSegment();
        PathFigure pathFigure = new PathFigure();
        public Managers Manager { get; set; }
        static Connect()
        {       
            InputNodeProperty = DependencyProperty.Register("InputNode", typeof(Nodess), typeof(Connect), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(InputShange)));
            OutputNodeProperty = DependencyProperty.Register("OutputNode", typeof(Nodess), typeof(Connect), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OutputShange)));
            EndPointProperty = DependencyProperty.Register("EndPoint", typeof(Point), typeof(Connect), new FrameworkPropertyMetadata(new Point(0,0), FrameworkPropertyMetadataOptions.AffectsRender));
            StartPointProperty = DependencyProperty.Register("StartPoint", typeof(Point), typeof(Connect), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsRender));
        }
        public Connect(Point point) : base(false)
        {
            this.Style = Application.Current.FindResource(typeof(Connect)) as Style;
            pathFigure.Segments.Add(bezierSegment);
            path.Figures.Add(pathFigure);
            Manager = new Managers(this);
            Manager.translate.X = point.X;
            Manager.translate.Y = point.Y;
            Manager.canScale = false;
        }
        public Connect(Point point,string text) : this(point)
        {
            this.Text = text;
          
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // base.OnMouseMove(e);
            if (Mouse.Captured == this)
            {
                Point position = e.GetPosition(this.Parent as IInputElement);
                position.X -= Manager.translate.X;
                position.Y -= Manager.translate.Y;
                EndPoint = position;
            }
        }
        public static void InputShange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Nodess oldNode = (e.OldValue as Nodess);
            Nodess newNode = (e.NewValue as Nodess);

            if(oldNode!=null)
                oldNode.LocationChangeEvent -= connect.InputLocationChange;
            if (newNode != null)
                newNode.LocationChangeEvent += connect.InputLocationChange;
        }
        private void InputLocationChange(object sender, EventArgs e)
        {
            Point position = InputNode.OutputCenterLocation;
            position.X -= Manager.translate.X;
            position.Y -= Manager.translate.Y;
            StartPoint = position;
        }
        private static void OutputShange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Nodess oldNode = (e.OldValue as Nodess);
            Nodess newNode = (e.NewValue as Nodess);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            pathFigure.StartPoint = StartPoint;
            Point startPoint = pathFigure.StartPoint;
            Vector vector = EndPoint - startPoint;
            Point point1 = new Point(startPoint.X + 3 * vector.X / 8, startPoint.Y + 1 * vector.Y / 8);
            Point point2 = new Point(startPoint.X + 5 * vector.X / 8, startPoint.Y + 7 * vector.Y / 8);
            bezierSegment.Point1 = point1;
            bezierSegment.Point2 = point2;
            bezierSegment.Point3 = EndPoint;
            Pen pen = new Pen();
            pen.Brush = Brushes.White;
            drawingContext.DrawGeometry(null, pen,path);
            var t = this;
        }
    }
}
