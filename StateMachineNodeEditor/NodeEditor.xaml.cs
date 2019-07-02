using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class NodeEditor : UserControl
    {
        public Transform transform;
        private Point? _movePoint;
        private double zoom = 1;
        private double scale = 0.05;
        private double MaxScale = 3;
        private double MinScale = 0.1;
        public TranslateTransform translateTransform = new TranslateTransform();
        public NodeEditor()
        {
            InitializeComponent();
            transform = new Transform(Container);
            _movePoint = null;
            Line line = new Line();
            line.X1 = 0;
            line.X2 = 500;
            line.Y1 = 0;
            line.Y2 = 500;
            line.Stroke = Brushes.Red;
            Container.Children.Add(line);      
            translateTransform.X = 0;
            translateTransform.Y = 0;
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(translateTransform);
            El.RenderTransform = transformGroup;

        }

        private void GridMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Container.CaptureMouse();
        }

        private void GridMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Container.ReleaseMouseCapture();
            this.Cursor = Cursors.Arrow;
            _movePoint = null;
        }

        private void GridMain_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((e.Delta == 0) || ((e.Delta > 0) && (zoom > MaxScale)) || ((e.Delta < 0) && (zoom < MinScale)))
                return;
            zoom += (e.Delta > 0) ? scale : -scale;
            transform.scale.ScaleX = zoom;
            transform.scale.ScaleY = zoom;
            transform.scale.CenterX = e.GetPosition(this).X;
            transform.scale.CenterY = e.GetPosition(this).Y;
            //GridScale.ScaleX = zoom;
            //GridScale.ScaleY = zoom;
            //GridScale.CenterX = e.GetPosition(this).X;
            //GridScale.CenterY = e.GetPosition(this).Y;
        }

        private void GridMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                return;
            if (Mouse.Captured == Container)
            {
                if (_movePoint != null)
                {
                    this.Cursor = Cursors.Hand;
                    transform.translate.X += (e.GetPosition(Container).X - _movePoint.Value.X);
                    transform.translate.Y += (e.GetPosition(Container).Y - _movePoint.Value.Y);
                    //GridTranslate.X += (e.GetPosition(Container).X - _movePoint.Value.X) * GridScale.ScaleX;
                    //GridTranslate.Y += (e.GetPosition(Container).Y - _movePoint.Value.Y) * GridScale.ScaleY;
                }
                _movePoint = e.GetPosition(Container);
            }
        }

        private void El_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void El_MouseUp(object sender, MouseButtonEventArgs e)
        {
            translateTransform.X += 10;
        }

        private void El_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            translateTransform.X += 100;
        }
    }
}
