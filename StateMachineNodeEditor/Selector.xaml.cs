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
    public partial class Selector : UserControl
    {
        public Transforms transforms;
        public NodesCanvas nodesCanvas;
        public Selector(NodesCanvas _nodesCanvas)
        {
            InitializeComponent();
            transforms = new Transforms(this);
            nodesCanvas = _nodesCanvas;
            this.Visibility = Visibility.Collapsed;
            Panel.SetZIndex(this, 1000);
            this.IsVisibleChanged += VisualChange;
        }
        public void VisualChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.form.Width = 0;
            this.form.Height = 0;
        }
        public Point Position1
        {
            get { return ForPoint.GetPoint1WithAngle(this.form, transforms); }
            set
            {
                ForPoint.Equality(transforms.translate, value);
                ForPoint.EqualityCenter(transforms.rotate, value);
            }
        }
        public Point Position2
        {
            get
            {
                return ForPoint.GetPoint2WithAngle(this.form, transforms);
            }
            set
            {
                ChangePosition2(value);
            }
        }
        private void ChangePosition2(Point point)
        {
           void SwapWidthHeight()
            {
                double tmp = form.Width;
                form.Width = form.Height;
                form.Height = tmp;
            }
            Point position1 = ForPoint.GetValueAsPoint(transforms.translate);
            Point size = ForPoint.Subtraction(point, position1);
            form.Width = Math.Abs(size.X);
            form.Height = Math.Abs(size.Y);
            if ((size.X > 0) && (size.Y > 0))
            {
                transforms.rotate.Angle = 0;
            }
            else if ((size.X < 0) && (size.Y > 0))
            {
               
                transforms.rotate.Angle = 90;
                SwapWidthHeight();
            }
            else if ((size.X > 0) && (size.Y < 0))
            {
               
                transforms.rotate.Angle = 270;
                SwapWidthHeight();
            }
            else if ((size.X < 0) && (size.Y < 0))
            {
                transforms.rotate.Angle = 180;
            }
        }
    }
}
