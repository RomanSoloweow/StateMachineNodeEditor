using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Media;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
namespace StateMachineNodeEditor
{
    public partial class ConnectNode : Control
    {
        private GeometryGroup figure = new GeometryGroup();
       public ConnectNode(UIElement _parent):base(_parent)
        {
            MouseEnter += ConnectNode_MouseEnter;
           // this.AddVisualChild()
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Pen t = new Pen();
            t.Brush = Brushes.Red;
            drawingContext.DrawGeometry(Brushes.Red,t,figure);
          
                drawingContext.DrawLine(t, new Point(0, 0), new Point(250, 250));
            drawingContext.DrawLine(t, new Point(300, 300), new Point(400, 400));

        }
        private void ConnectNode_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!(sender is ConnectNode))
                return;
        }
    }
}
