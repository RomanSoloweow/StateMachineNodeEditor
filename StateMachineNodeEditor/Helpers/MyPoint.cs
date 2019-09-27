using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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
    }
}
