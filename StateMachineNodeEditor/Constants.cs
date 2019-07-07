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

namespace StateMachineNodeEditor
{
    public static class Constants
    {

        public static Pen nodeLine = new Pen()
        {
            Brush=Brushes.Black
        };
        public static Brush nodeBrushSelection = Brushes.White;
        public static Brush nodeBrushHeader = new SolidColorBrush(Color.FromRgb(18, 61, 106));
        public static Brush nodeBrushBody = new SolidColorBrush(Color.FromRgb(45, 45, 48));
        public static Brush nodeBrushForeground = Brushes.DarkGray;
        public static Brush nodeBrushCaret = Brushes.DarkGray; 
        
        public static Pen nodePen = new Pen();
        public static double nodeRadius = 5;
        public static int nodeMaxLength = 100;
        public static Thickness nodeTextBorder = new Thickness(10, 2, 10, 2);

        public static double scale = 0.05;
        public static double ScaleMax = 5;
        public static double ScaleMin = 0.1;
        public static double TranslateXMax = 10000;
        public static double TranslateXMin = -10000;
        public static double TranslateYMax = 10000;
        public static double TranslateYMin = -10000;


        public static Brush textSelectionBrush = Brushes.White;
        public static Brush textBrushForeground = Brushes.DarkGray;
        public static Brush textBrushCaret = Brushes.DarkGray;
        public static int textMaxLength = 100;

    }
}
