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
    public class NodeEditor2:Grid
    {
        Transform transform;
        private VisualCollection _children;
        public Point? _movePoint = null;
        private double zoom = 1;
        Grid grid = new Grid();
        public NodeEditor2()
        {
            grid.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Background = Brushes.Red;
            this.ClipToBounds = true;
            grid.Background = Brushes.Blue;
            grid.ClipToBounds = true;
           


            transform = new Transform(grid);
            Size panelDesiredSize = new Size();
            //Node node = new Node();
            //node.HorizontalAlignment = HorizontalAlignment.Center;
            //node.VerticalAlignment = VerticalAlignment.Center;
            //this.Children.Add(grid);
            //grid.Children.Add(node);
            this.MouseDown += _MouseDown;
            this.MouseUp += _MouseUp;
            this.MouseMove += _MouseMove;
            this.MouseWheel += _MouseWheel;
        }
        private void _MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.CaptureMouse();
        }

        private void _MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            this.Cursor = Cursors.Arrow;
            _movePoint = null;
        }
        public void _MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                return;
            if (Mouse.Captured == this)
            {
                if (_movePoint != null)
                {
                    this.Cursor = Cursors.Hand;
                  double deltaX = (e.GetPosition(transform.parent).X - _movePoint.Value.X);
                  double deltaY = (e.GetPosition(transform.parent).Y - _movePoint.Value.Y);
                  bool XMax = ((deltaX > 0) && (transform.translate.X > Constants.TranslateXMax));
                  bool XMin = ((deltaX < 0) && (transform.translate.X < Constants.TranslateXMin));
                  bool YMax = ((deltaY > 0) && (transform.translate.Y > Constants.TranslateYMax));
                  bool YMin = ((deltaY < 0) && (transform.translate.Y < Constants.TranslateXMin));
                    if (XMax||XMin||YMax|| YMin)
                        return;
                    transform.translate.X += deltaX;
                    transform.translate.Y += deltaY;
                }
                _movePoint = e.GetPosition(transform.parent);
            }
        }
        private void _MouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool Delta0 = (e.Delta == 0);
            bool DeltaMax = ((e.Delta > 0) && (zoom > Constants.ScaleMax));
            bool DeltaMin = ((e.Delta < 0) && (zoom < Constants.ScaleMin));
            if (Delta0||DeltaMax|| DeltaMin)
                return;
            zoom += (e.Delta > 0) ? Constants.scale : -Constants.scale;
            transform.scale.ScaleX = zoom;
            transform.scale.ScaleY = zoom;
          //  transform.scale.CenterX= e.GetPosition(transform.parent).Y;
           // transform.scale.CenterY = e.GetPosition(transform.parent).Y;
        }
    }
}
