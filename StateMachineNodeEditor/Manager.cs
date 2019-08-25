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
  //public struct Delta
  //  {
  //      public double deltaX;
  //      public double deltaY;
  //  }
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

        public Managers(UIElement _parent):base(_parent)
        {
            //Origin = new Point(0.5, 0.5);
            parent.MouseDown += mouseDown;
            parent.MouseUp += mouseUp;
            //parent.MouseMove += mouseMove;
            //parent.MouseWheel += _MouseWheel;
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
                //result.X = (CurrentPosition.Value.X - _movePoint.Value.X);
                //result.Y = (CurrentPosition.Value.Y - _movePoint.Value.Y);
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
                //delta.X /= scale.ScaleX;
                //delta.Y /= scale.ScaleY;
            delta = ForPoint.DivisionOnScale(delta, scale);
                bool XMax = ((delta.X > 0) && (translate.X > TranslateXMax));
                bool XMin = ((delta.X < 0) && (translate.X < TranslateXMin));
                bool YMax = ((delta.Y > 0) && (translate.Y > TranslateYMax));
                bool YMin = ((delta.Y < 0) && (translate.Y < TranslateXMin));
                if (XMax || XMin || YMax || YMin)
                    return;
                ForPoint.Addition(translate, delta);
                //translate.X += delta.X;
                //translate.Y += delta.Y;
        }
        //public void mouseMove(object sender, MouseEventArgs e)
        //{
        //    bool thisElementSelected = (Mouse.Captured == parent);
        //    bool leftButtonPressed = (Mouse.LeftButton != MouseButtonState.Pressed);
        //    bool MoveAllowed = canMove || canMoveChildren;

        //    if ((!thisElementSelected) || (!leftButtonPressed) || (!canMove))
        //        return;

        //    if (_movePoint != null)
        //    {
        //        Point Position = e.GetPosition(parent);
        //        double deltaX = (e.GetPosition(parent).X - _movePoint.Value.X);
        //        double deltaY = (e.GetPosition(parent).Y - _movePoint.Value.Y);
        //        bool XMax = ((deltaX > 0) && (translate.X > TranslateXMax));
        //        bool XMin = ((deltaX < 0) && (translate.X < TranslateXMin));
        //        bool YMax = ((deltaY > 0) && (translate.Y > TranslateYMax));
        //        bool YMin = ((deltaY < 0) && (translate.Y < TranslateXMin));
        //        if (XMax || XMin || YMax || YMin)
        //            return;

        //        if (canMoveChildren)
        //        {
        //            foreach (var children in childrenForMove)
        //            {
        //                children.Manager.translate.X += deltaX / children.Manager.scale.ScaleX;
        //                children.Manager.translate.Y += deltaY / children.Manager.scale.ScaleY;
        //            }
        //        }
        //        if (canMove)
        //        {
        //            translate.X += deltaX;
        //            translate.Y += deltaY;
        //        }
        //    }
        //    _movePoint = e.GetPosition(parent);
        //}
        public void Scale(int Delta)
        {
            bool Delta0 = (Delta == 0);
            bool DeltaMax = ((Delta > 0) && (zoom > ScaleMax));
            bool DeltaMin = ((Delta < 0) && (zoom < ScaleMin));
            if (Delta0 || DeltaMax || DeltaMin)
                return;

            zoom += (Delta > 0) ? scales : -scales;
            ForPoint.EqualityScale(scale, zoom);
            //scale.ScaleX = zoom;
            //scale.ScaleY = zoom;
        }
        //private void _MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    bool thisElementSelected = (Mouse.Captured == parent);
        //    bool ScaleAllowed = canScale || canScaleChildren;

        //    if ((!thisElementSelected) || (!ScaleAllowed))
        //        return;

        //    bool Delta0 = (e.Delta == 0);
        //    bool DeltaMax = ((e.Delta > 0) && (zoom > ScaleMax));
        //    bool DeltaMin = ((e.Delta < 0) && (zoom < ScaleMin));
        //    if (Delta0 || DeltaMax || DeltaMin)
        //        return;

        //    zoom += (e.Delta > 0) ? scales : -scales;

        //    if (canScaleChildren)
        //    {
        //        foreach (var children in childrenForMove)
        //        {
        //            children.Manager.scale.ScaleX = zoom;
        //            children.Manager.scale.ScaleY = zoom;
        //        }
        //    }
        //    if (canScale)
        //    {
        //        scale.ScaleX = zoom;
        //        scale.ScaleY = zoom;
        //    }
        //}
    }
}
