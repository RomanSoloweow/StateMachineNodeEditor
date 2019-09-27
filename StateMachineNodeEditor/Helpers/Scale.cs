using ReactiveUI;
using ReactiveUI.Fody.Helpers;
namespace StateMachineNodeEditor.Helpers
{
    public class Scale:ReactiveObject
    {
        [Reactive] public MyPoint Scales { get; set; } = new MyPoint(1, 1);
        [Reactive] public MyPoint Center { get; set; } = new MyPoint();
    }
}
