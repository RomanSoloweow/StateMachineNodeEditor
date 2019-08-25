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

            //this.Visibility = Visibility.Hidden;
            //translate.X = point.X;
            //translate.Y = point.Y;

            //MouseMove += mouseMove;
            //nodesCanvas.MouseMove += mouseMove;
        }
        public void VisualChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.form.Width = 0;
            this.form.Height = 0;
        }
        public Point Position1
        {
            get
            {
                return ForPoint.GetValueAsPoint(transforms.translate);
            }
            set
            {
                ForPoint.Equality(transforms.translate, value);
                //transforms.translate.X = value.X;
                //transforms.translate.Y = value.Y;
                ForPoint.EqualityCenter(transforms.rotate, value);
                //transforms.rotate.CenterX = value.X;
                //transforms.rotate.CenterY = value.Y;
            }
        }

        public void Change(Point point)
        {
            Point position1 = Position1;
            Point size = ForPoint.Subtraction(point, Position1);
            //size.X = point.X - position1.X;
            //size.Y = point.Y - position1.Y;
            form.Width = Math.Abs(size.X);
            form.Height = Math.Abs(size.Y);
            if ((size.X > 0) && (size.Y > 0))
            {
                transforms.rotate.Angle = 0;
            }
            else if ((size.X < 0) && (size.Y > 0))
            {
                double tmp = form.Width;
                form.Width = form.Height;
                form.Height = tmp;
                transforms.rotate.Angle = 90;             
            }
            else if ((size.X > 0) && (size.Y < 0))
            {
                transforms.rotate.Angle = 270;
                double tmp = form.Width;
                form.Width = form.Height;
                form.Height = tmp;
            }
            else if ((size.X < 0) && (size.Y < 0))
            {
                transforms.rotate.Angle = 180;
            }
        }
    }
}
