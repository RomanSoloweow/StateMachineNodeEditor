using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace StateMachineNodeEditor.Helpers
{
    public class Translate:ReactiveObject
    {
        [Reactive] public double X { get; set; } = 1;
        [Reactive] public double Y { get; set; } = 1;
    }
}
