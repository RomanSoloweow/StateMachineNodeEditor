using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
namespace StateMachineNodeEditor
{
    public class Managers:Transforms
    {
        public Point? _movePoint { get; private set; } = null;
 
        public bool canMovie;
        public bool canScale;
        public double ScaleMax = 5;
        public double ScaleMin = 0.1;
        public double TranslateXMax = 10000;
        public double TranslateXMin = -10000;
        public double TranslateYMax = 10000;
        public double TranslateYMin = -10000;

        public double zoom { get; set; } = 1;
        public double scales { get;  set; } = 0.05;
        //public Point Position1
        //{
        //    get { return GetPosition1(translate);}
        //}
        //public Point Position2
        //{
        //    get { return GetPosition2(parent, translate); }
        //}       
        public Managers(FrameworkElement _parent):base(_parent)
        {
            parent.MouseDown += mouseDown;
            parent.MouseUp += mouseUp;
        }
        public void mouseDown(object sender, MouseButtonEventArgs e)
        {
            _movePoint = null;
            _movePoint= Mouse.GetPosition(parent);
            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                parent.CaptureMouse();
            }
        }
        public void mouseUp(object sender, MouseButtonEventArgs e)
        {
            _movePoint = null;
            parent.ReleaseMouseCapture();
        }
        public Point GetDeltaMove(Point? CurrentPosition=null)
        {
            Point result = new Point();

            if (CurrentPosition == null)
                CurrentPosition = Mouse.GetPosition(parent);
        
            if (_movePoint != null)
            {
                result = ForPoint.Subtraction(CurrentPosition.Value, _movePoint.Value);
            }
            _movePoint = CurrentPosition;
            return result;
        }
        public void Move(Point? CurrentPosition = null)
        {
            if (_movePoint != CurrentPosition)
            {
                Point delta = GetDeltaMove();
                Move(delta);
            }
        }
        public void Move(Point delta)
        {
            delta = ForPoint.DivisionOnScale(delta, scale);
            bool XMax = ((delta.X > 0) && (translate.X > TranslateXMax));
            bool XMin = ((delta.X < 0) && (translate.X < TranslateXMin));
            bool YMax = ((delta.Y > 0) && (translate.Y > TranslateYMax));
            bool YMin = ((delta.Y < 0) && (translate.Y < TranslateXMin));
            if (XMax || XMin || YMax || YMin)
                return;
            ForPoint.Addition(translate, delta);
        }
        public void Scale(int Delta)
        {
            bool Delta0 = (Delta == 0);
            bool DeltaMax = ((Delta > 0) && (zoom > ScaleMax));
            bool DeltaMin = ((Delta < 0) && (zoom < ScaleMin));
            if (Delta0 || DeltaMax || DeltaMin)
                return;

            zoom += (Delta > 0) ? scales : -scales;
            ForPoint.EqualityScale(scale, zoom);
        }
    }
}
