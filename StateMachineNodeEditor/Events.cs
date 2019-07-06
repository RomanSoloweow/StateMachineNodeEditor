using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
namespace StateMachineNodeEditor
{
    public class Events
    {
        public UIElement parent;
        public Events(UIElement _parent)
        {
            parent = _parent;
            _parent.MouseDown += mouseDown;
            _parent.MouseUp += mouseUp;
        }

        public void mouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured==null)
            ((UIElement)sender).CaptureMouse();
            
        }
        public void mouseUp(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).ReleaseMouseCapture();
            ((FrameworkElement)sender).Cursor = Cursors.Arrow;   
        }
    }
}
