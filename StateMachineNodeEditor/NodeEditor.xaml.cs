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

        private double _ScaleX;
        private double _ScaleY;
        private double _TransformX;
        private double _TransformY;
        private string _test;
        public string Test
        {
            get
            {
                return _test;
            }
            set
            {
                _test = value;
                // OnPropertyChanged("ScaleX");
            }
        }
        public double ScaleX
        {
            get
            {
                return _ScaleX;
            }
            set
            {
                _ScaleX = value;
                // OnPropertyChanged("ScaleX");
            }
        }
        public double ScaleY
        {
            get
            {
                return _ScaleY;
            }
            set
            {
                _ScaleY = value;
                // OnPropertyChanged("ScaleX");
            }
        }
        public double TransformX
        {
            get
            {
                return _TransformX;
            }
            set
            {
                _TransformX = value;
                // OnPropertyChanged("ScaleX");
            }
        }
        public double TransformY
        {
            get
            {
                return _TransformY;
            }
            set
            {
                _TransformY = value;
                // OnPropertyChanged("ScaleX");
            }
        }

        private Point? _movePoint;
        private double zoom = 1;
        private double scale = 0.05;
        private double MaxScale = 3;
        private double MinScale = 0.1;
        public NodeEditor()
        {
            InitializeComponent();
            _movePoint = null;
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
            GridScale.ScaleX = zoom;
            GridScale.ScaleY = zoom;
            GridScale.CenterX = e.GetPosition(this).X;
            GridScale.CenterY = e.GetPosition(this).Y;
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
                    GridTranslate.X += (e.GetPosition(Container).X - _movePoint.Value.X) * GridScale.ScaleX;
                    GridTranslate.Y += (e.GetPosition(Container).Y - _movePoint.Value.Y) * GridScale.ScaleY;
                }
                _movePoint = e.GetPosition(Container);
            }
        }
    }
}
