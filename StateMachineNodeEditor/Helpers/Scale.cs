using ReactiveUI;
using ReactiveUI.Fody.Helpers;
namespace StateMachineNodeEditor.Helpers
{
    public class Scale:ReactiveObject
    {
        [Reactive] public double ScaleX { get; set; } = 1;
        [Reactive] public double ScaleY { get; set; } = 1;
        [Reactive] public double CenterX { get; set; }
        [Reactive] public double CenterY { get; set; }
    }
}
