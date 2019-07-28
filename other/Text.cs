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
        static Text()
        {
            
        }
        public Text(bool setStyle=true)
        {
           // base.ContextMenu = null;
            if(setStyle)
            this.Style = Application.Current.FindResource(typeof(Text)) as Style;
        }
        public Text(string text,bool setStyle = true):this(setStyle)
        {
            this.Text = text;
        }
    }
}
