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
namespace StateMachineNodeEditor
{
    public class Text:TextBox
    {
        public Text(string text, Style textStyle)
        {
            base.Style = textStyle;
            base.Text = text;
            
        }
    }
}
