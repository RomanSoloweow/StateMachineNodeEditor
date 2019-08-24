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
  public struct Delta
    {
        public double deltaX;
        public double deltaY;
    }
    public class Managers
    {
        public TransformGroup _transformGroup { get; private set; } = new TransformGroup();
        public TranslateTransform translate { get; private set; } = new TranslateTransform();
        public RotateTransform rotate { get; private set; } = new RotateTransform();
        public ScaleTransform scale { get;  set; } = new ScaleTransform();
        public SkewTransform skew { get; private set; } = new SkewTransform();
        public MatrixTransform matrix { get; private set; } = new MatrixTransform();
        public Point Origin
        {
            get
            {
                return parent.RenderTransformOrigin;
            }

            set
            {
                parent.RenderTransformOrigin = value;
            }
        }
        public Point? _movePoint { get; private set; } = null;
        public UIElement parent;
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

        public Managers(UIElement _parent)
        {
            _transformGroup.Children.Add(translate);
            _transformGroup.Children.Add(rotate);
            _transformGroup.Children.Add(scale);
            _transformGroup.Children.Add(skew);
            _transformGroup.Children.Add(matrix);
            parent = _parent;
            parent.RenderTransform = _transformGroup;
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
        public Delta GetDeltaMove(Point? CurrentPosition=null)
        {
            Delta result = new Delta();

            if (CurrentPosition == null)
                CurrentPosition = Mouse.GetPosition(parent);
        
            if (_movePoint != null)
            {
                result.deltaX = (CurrentPosition.Value.X - _movePoint.Value.X);
                result.deltaY = (CurrentPosition.Value.Y - _movePoint.Value.Y);
            }
            _movePoint = CurrentPosition;
            return result;
        }
        public void Move(Point? CurrentPosition = null)
        {
            if (_movePoint != CurrentPosition)
            {
                Delta delta = GetDeltaMove();
                Move(delta);
            }
        }
        public void Move(Delta delta)
        {
                delta.deltaX /= scale.ScaleX;
                delta.deltaY /= scale.ScaleY;
                bool XMax = ((delta.deltaX > 0) && (translate.X > TranslateXMax));
                bool XMin = ((delta.deltaX < 0) && (translate.X < TranslateXMin));
                bool YMax = ((delta.deltaY > 0) && (translate.Y > TranslateYMax));
                bool YMin = ((delta.deltaY < 0) && (translate.Y < TranslateXMin));
                if (XMax || XMin || YMax || YMin)
                    return;

                translate.X += delta.deltaX;
                translate.Y += delta.deltaY;
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
            scale.ScaleX = zoom;
            scale.ScaleY = zoom;
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
