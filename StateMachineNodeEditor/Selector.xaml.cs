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
            this.Visibility = Visibility.Hidden;
            this.IsVisibleChanged += VisualChange;
            //transforms.Origin = new Point(0.5, 0.5);
        }
        public void VisualChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.form.Width = 0;
            this.form.Height = 0;
        }
        public Point Position1
        {
            get { return ForPoint.GetPoint1WithScale(this.form, transforms); }
            set
            {
                ForPoint.Equality(transforms.translate, value);
                ForPoint.EqualityCenter(transforms.rotate, value);
                ForPoint.EqualityCenter(transforms.scale, value);
            }
        }
        public Point Position2
        {
            get
            {
                return ForPoint.GetPoint2WithScale(this.form, transforms);
            }
            set
            {
                ChangePosition2(value);

            }
        }
        public void StartSelect(Point position)
        {
            Position1 = position;
            Visibility = Visibility.Visible;
        }
        public void EndSelect()
        {
            Visibility = Visibility.Hidden;
        }
        private void ChangePosition2(Point point)
        {        
            Point position1 = ForPoint.GetValueAsPoint(transforms.translate);
            Point size = ForPoint.Subtraction(point, position1);
            form.Width = Math.Abs(size.X);
            form.Height = Math.Abs(size.Y);
            //Если нужно отражаем по X
            transforms.scale.ScaleX = (size.X>0) ? 1 : -1;
            //Если нужно отражаем по Y
            transforms.scale.ScaleY = (size.Y>0) ? 1 : -1;
        }
    }
}
