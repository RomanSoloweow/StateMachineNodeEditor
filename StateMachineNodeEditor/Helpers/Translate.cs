using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows;

namespace StateMachineNodeEditor.Helpers
{
    public class Translate:ReactiveObject
    {
        [Reactive] public double X { get; set; } = 1;
        [Reactive] public double Y { get; set; } = 1;

        public void Add(Point point)
        {
            X += point.X;
            Y += point.Y;
        }
    }
}
