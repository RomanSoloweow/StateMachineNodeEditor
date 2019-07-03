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
        private Point? _movePoint = null;
        private double zoom = 1;
        public ConnectNode con;
        public NodeEditor()
        {
            InitializeComponent();
            transform = new Transform(Container);
            NodeEditor2 t = new NodeEditor2();
            GridMain.MouseMove += GridMain_MouseMove;
            Container.Children.Add(t);
            //con = new ConnectNode(this);
            // Container.Children.Add(con);
        }
        public void GridMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                return;
            if (Mouse.Captured == Container)
            {
                if (_movePoint != null)
                {
                    //this.Cursor = Cursors.Hand;
                    transform.translate.X += (e.GetPosition(Container).X - _movePoint.Value.X);
                    transform.translate.Y += (e.GetPosition(Container).Y - _movePoint.Value.Y);
                }
                _movePoint = e.GetPosition(Container);
            }
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
            if ((e.Delta == 0) || ((e.Delta > 0) && (zoom > Constants.ScaleMax)) || ((e.Delta < 0) && (zoom < Constants.ScaleMin)))
                return;
            zoom += (e.Delta > 0) ? Constants.scale : -Constants.scale;
            transform.scale.ScaleX = zoom;
            transform.scale.ScaleY = zoom;
            transform.scale.CenterX = e.GetPosition(this).X;
            transform.scale.CenterY = e.GetPosition(this).Y;          
        }



  
        private void El_DragEnter(object sender, DragEventArgs e)
        {

        }
    }
}
