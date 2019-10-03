using ReactiveUI;
using ReactiveUI.Fody.Helpers;
namespace StateMachineNodeEditor.Helpers
{
    public class Scale:ReactiveObject
    {
        [Reactive] public MyPoint Scales { get; set; } = new MyPoint(1, 1);
        [Reactive] public MyPoint Center { get; set; } = new MyPoint();

        [Reactive] public double Value { get; set; } = 1;

        public double ScaleX
        {
            get { return Scales.X; }
        }
        public double ScaleY
        {
            get { return Scales.Y; }
        }

        public double CenterX
        {
            get { return Center.X; }
        }
        public double CenterY
        {
            get { return Center.Y; }
        }

    }
}
