using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for VIewNodesCanvas.xaml
    /// </summary>
    public partial class ViewNodesCanvas : UserControl
    {
        public ViewNodesCanvas()
        {
            InitializeComponent();
            ViewModelNodesCanvas viewModelNodesCanvas = new ViewModelNodesCanvas(new ModelNodesCanvas());
            DataContext = viewModelNodesCanvas;
        }
        public static DependencyProperty RegisterCommandBindingsProperty = DependencyProperty.RegisterAttached("RegisterCommandBindings", typeof(CommandBindingCollection), typeof(ViewNodesCanvas), new PropertyMetadata(null, OnRegisterCommandBindingChanged));
        public static void SetRegisterCommandBindings(UIElement element, CommandBindingCollection value)
        {
            if (element != null)
                element.SetValue(RegisterCommandBindingsProperty, value);
        }
        public static CommandBindingCollection GetRegisterCommandBindings(UIElement element)
        {
            return (element != null ? (CommandBindingCollection)element.GetValue(RegisterCommandBindingsProperty) : null);
        }
        private static void OnRegisterCommandBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                CommandBindingCollection bindings = e.NewValue as CommandBindingCollection;
                if (bindings != null)
                {
                    element.CommandBindings.AddRange(bindings);
                }
            }
        }


        public static DependencyProperty RegisterInputBindingsProperty = DependencyProperty.RegisterAttached("RegisterInputBindings", typeof(InputBindingCollection), typeof(ViewNodesCanvas), new PropertyMetadata(null, OnRegisterInputBindingsChanged));
        public static void SetRegisterInputBindings(UIElement element, InputBindingCollection value)
        {
            if (element != null)
                element.SetValue(RegisterInputBindingsProperty, value);
        }
        public static InputBindingCollection GetRegisterInputBindings(UIElement element)
        {
            return (element != null ? (InputBindingCollection)element.GetValue(RegisterInputBindingsProperty) : null);
        }
        private static void OnRegisterInputBindingsChanged
        (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                InputBindingCollection bindings = e.NewValue as InputBindingCollection;
                if (bindings != null)
                {
                    element.InputBindings.AddRange(bindings);
                }
            }
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Keyboard.Focus(this);
        }
    }
}
