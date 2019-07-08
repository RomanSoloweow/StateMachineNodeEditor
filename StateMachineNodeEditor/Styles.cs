using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace StateMachineNodeEditor
{
    public class Styles : Style
    {
        public Styles()
        { }
        public void AddSetter(DependencyProperty property, object value)
        {
            this.Setters.Add(new Setter { Property = property, Value = value });
        }
    }
}

