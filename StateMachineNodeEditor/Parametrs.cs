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
   public class Parametrs
    {
        public struct Text
        {
            public Brush BrushSelection;
            public Brush BrushForeground;
            public Brush BrushCaret;
            public int MaxLength;
        }

        public class Node
        {
            public Parametrs.Text text;
            public Brush BrushHeader;
            public Brush BrushBody;
            public Pen Pen;
            public double Radius;        
        }
    }
}
