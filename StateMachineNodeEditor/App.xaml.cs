using System.Reflection;
using System.Windows;
using ReactiveUI;
using Splat;
using StateMachineNodeEditor.Helpers;

namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            Locator.CurrentMutable.RegisterConstant(new ConverterBoolAndVisibility(), typeof(IBindingTypeConverter));
        }
    }
}
