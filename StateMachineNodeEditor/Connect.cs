using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Globalization;
namespace StateMachineNodeEditor
{
   public class Connect
    {
        public Point startPoint;
        Connect(Point _startPoint)
        {
            startPoint = _startPoint;
        }
    }
}
