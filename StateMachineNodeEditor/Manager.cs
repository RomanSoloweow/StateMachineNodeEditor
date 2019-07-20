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
    public class Managers
    {
        private TransformGroup _transformGroup = new TransformGroup();
        public TranslateTransform translate = new TranslateTransform();
        public RotateTransform rotate = new RotateTransform();
        public ScaleTransform scale = new ScaleTransform();
        public SkewTransform skew = new SkewTransform();
        public MatrixTransform matrix = new MatrixTransform();

        //public Point? _movePoint = null;
        private double zoom = 1;
        public UIElement parent;
        //public bool canMove = true;
        public bool canScale = true;
        public double scales = 0.05;
        public double ScaleMax = 5;
        public double ScaleMin = 0.1;
        //public double TranslateXMax = 10000;
        //public double TranslateXMin = -10000;
        //public double TranslateYMax = 10000;
        //public double TranslateYMin = -10000;


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
        public Managers(UIElement _parent)
        {
            _transformGroup.Children.Add(translate);
            _transformGroup.Children.Add(rotate);
            _transformGroup.Children.Add(scale);
            _transformGroup.Children.Add(skew);
            _transformGroup.Children.Add(matrix);

            parent = _parent;
            parent.RenderTransform = _transformGroup;
            Origin = new Point(0.5, 0.5);

            //parent.MouseDown += mouseDown;
            //parent.MouseUp += mouseUp;
            //parent.MouseMove += mouseMove;
            parent.MouseWheel += _MouseWheel;
        }

        //public void mouseDown(object sender, MouseButtonEventArgs e)
        //{
        //   // _movePoint = null;
        //    if (Mouse.Captured == null)
        //    {
        //        Keyboard.ClearFocus();
        //        parent.CaptureMouse();
        //    }
        //}
        //public void mouseUp(object sender, MouseButtonEventArgs e)
        //{
        //   // _movePoint = null;

        //    parent.ReleaseMouseCapture();
        //   // ((FrameworkElement)sender).Cursor = Cursors.Arrow;
        //}

        //public void mouseMove(object sender, MouseEventArgs e)
        //{
        //    if ((Mouse.LeftButton != MouseButtonState.Pressed) || (!canMove))
        //        return;
        //    if (Mouse.Captured == parent)
        //    {
        //        if (_movePoint != null)
        //        {
        //            ((FrameworkElement)sender).Cursor = Cursors.SizeAll;
        //            Point Position = e.GetPosition(parent);
        //            double deltaX = (e.GetPosition(parent).X - _movePoint.Value.X);
        //            double deltaY = (e.GetPosition(parent).Y - _movePoint.Value.Y);
        //            bool XMax = ((deltaX > 0) && (translate.X > TranslateXMax));
        //            bool XMin = ((deltaX < 0) && (translate.X < TranslateXMin));
        //            bool YMax = ((deltaY > 0) && (translate.Y > TranslateYMax));
        //            bool YMin = ((deltaY < 0) && (translate.Y < TranslateXMin));
        //            if (XMax || XMin || YMax || YMin)
        //                return;

        //            //foreach (var children in childrens)
        //            //{
        //            //    children.Manager.translate.X += deltaX / children.Manager.scale.ScaleX;
        //            //    children.Manager.translate.Y += deltaY / children.Manager.scale.ScaleY;
        //            //}
        //            //if (test)
        //            //{
        //            translate.X += deltaX;
        //            translate.Y += deltaY;
        //            // }
        //        }
        //        _movePoint = e.GetPosition(parent);
        //    }
        //}
        private void _MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((Mouse.Captured != null) || (!canScale))
                return;
            bool Delta0 = (e.Delta == 0);
            bool DeltaMax = ((e.Delta > 0) && (zoom > ScaleMax));
            bool DeltaMin = ((e.Delta < 0) && (zoom < ScaleMin));
            if (Delta0 || DeltaMax || DeltaMin)
                return;

            zoom += (e.Delta > 0) ? scales : -scales;
            //foreach (var children in childrens)
            //{
            //    children.Manager.scale.ScaleX = zoom;
            //    children.Manager.scale.ScaleY = zoom;
            //}
            scale.ScaleX = zoom;
            scale.ScaleY = zoom;
        }
    }
}
