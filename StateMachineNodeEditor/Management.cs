using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
using System.Windows;
namespace StateMachineNodeEditor
{
    public class Management
    {
        private TransformGroup _transformGroup = new TransformGroup();
        public TranslateTransform translate = new TranslateTransform();
        public RotateTransform rotate = new RotateTransform();
        public ScaleTransform scale = new ScaleTransform();
        public SkewTransform skew = new SkewTransform();
        public MatrixTransform matrix = new MatrixTransform();
        public Point? _movePoint = null;
        public UIElement objectEvent;
        public UIElement objectTransform;
        public bool canMove = true;
        public Point Origin
        {
            get
            {
                return objectTransform.RenderTransformOrigin;
            }

            set
            {
                objectTransform.RenderTransformOrigin = value;
            }
        }

        public Management(UIElement _parentEvent, UIElement _parentTransform)
        {
            objectEvent = _parentEvent;
            objectTransform = _parentTransform;
            _transformGroup.Children.Add(translate);
            _transformGroup.Children.Add(rotate);
            _transformGroup.Children.Add(scale);
            _transformGroup.Children.Add(skew);
            _transformGroup.Children.Add(matrix);
            objectTransform.RenderTransform = _transformGroup;
            Origin = new Point(0.5, 0.5);
            objectEvent.MouseDown += mouseDown;
            objectEvent.MouseUp += mouseUp;
            objectEvent.MouseMove += mouseMove;
        }
        public Management(UIElement parent):this(parent, parent)
        {
        }

        public void mouseDown(object sender, MouseButtonEventArgs e)
        {
            _movePoint = null;
            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                objectEvent.CaptureMouse();            
            }
        }
        public void mouseUp(object sender, MouseButtonEventArgs e)
        {
            _movePoint = null;
            
            ((UIElement)sender).ReleaseMouseCapture();
            ((FrameworkElement)sender).Cursor = Cursors.Arrow;
            
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            if ((Mouse.LeftButton != MouseButtonState.Pressed)||(!canMove))
                return;
            if (Mouse.Captured == objectEvent)
            {
                if (_movePoint != null)
                {
                    ((FrameworkElement)sender).Cursor = Cursors.SizeAll;
                    double deltaX = (e.GetPosition(objectTransform).X - _movePoint.Value.X);
                    double deltaY = (e.GetPosition(objectTransform).Y - _movePoint.Value.Y);
                    bool XMax = ((deltaX > 0) && (translate.X > Constants.TranslateXMax));
                    bool XMin = ((deltaX < 0) && (translate.X < Constants.TranslateXMin));
                    bool YMax = ((deltaY > 0) && (translate.Y > Constants.TranslateYMax));
                    bool YMin = ((deltaY < 0) && (translate.Y < Constants.TranslateXMin));
                    if (XMax || XMin || YMax || YMin)
                        return;
                    translate.X += deltaX;
                    translate.Y += deltaY;
                }
                _movePoint = e.GetPosition(objectTransform);
            }
        }
    }
}
