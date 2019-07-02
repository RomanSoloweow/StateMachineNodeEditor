using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Media;
namespace StateMachineNodeEditor
{
    public class ConnectNode
    {
        private Ellipse rectangle = new Ellipse();
        public Transform transform;
        public Connect parent;
        public ConnectNode(Connect _parent)
        {
            parent = _parent;
            transform = new Transform(rectangle);
        }
    }
}
