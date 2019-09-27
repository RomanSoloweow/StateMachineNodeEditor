using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace StateMachineNodeEditor.Helpers
{
    public class Translate:ReactiveObject
    {
        [Reactive] public MyPoint Value { get; set; } = new MyPoint(1,1);
    }
}
