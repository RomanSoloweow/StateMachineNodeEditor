using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Media;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;

namespace StateMachineNodeEditor
{
    public class Control : UIElement
    {
        public UIElement parent;
        public Transform transform;
        public Control(UIElement _parent)
        {
            parent = _parent;
            transform = new Transform(this);
        }
    }
}
