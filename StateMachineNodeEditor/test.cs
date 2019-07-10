using MS.Internal;
using System.Threading;
using System.Collections; // IEnumerator
using System.ComponentModel; // DefaultValue
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data; // Binding
using System.Windows.Documents;
using System.Windows.Automation.Peers;
using System.Windows.Input; // CanExecuteRoutedEventArgs, ExecuteRoutedEventArgs
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Primitives; // TextBoxBase
using System.Windows.Navigation;
using System.Windows.Markup; // IAddChild, XamlDesignerSerializer, ContentPropertyAttribute
using MS.Utility;
using MS.Internal.Text;
using MS.Internal.Automation;   // TextAdaptor
using MS.Internal.Documents;    // Undo
using MS.Internal.Commands;     // CommandHelpers
using MS.Internal.Telemetry.PresentationFramework;


class Txt : TextBox
{
    public Txt() 
    {
    //var t = th

    }
    protected override void OnGotFocus(RoutedEventArgs e)
    {
        int k = 4;
        if (k > 8)
            base.OnGotFocus(e);
    }
    protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
    {
        base.OnVisualChildrenChanged(visualAdded, visualRemoved);
    }
    protected override void OnVisualParentChanged(DependencyObject oldParent)
    {
        base.OnVisualParentChanged(oldParent);
    }
    protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
    {
        base.OnManipulationStarted(e);
    }
    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
    }
    protected override void OnMouseEnter(MouseEventArgs e)
    {
        int k = 4;
        if(k>8)
        base.OnMouseEnter(e);
    }
}