using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows;

namespace StateMachineNodeEditor.Helpers
{
    public class MyPoint: ReactiveObject
    {
        [Reactive] public double X { get; set; }
        [Reactive] public double Y { get; set; }

        public MyPoint(double x = 0,  double y = 0)
        {
            X = x;
            Y = y;
        }
        public MyPoint Add(MyPoint point)
        {
            X += point.X;
            Y += point.Y;

            return this;
        }
        public static Point ToPoint(MyPoint point)
        {            
            return (point != null)?new Point(point.X, point.Y): new Point();
        }
        public static MyPoint FromPoint(Point point)
        {
            return (point != null) ? new MyPoint(point.X, point.Y) : new MyPoint();
        }
    }
}
