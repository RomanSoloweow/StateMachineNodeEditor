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
        //public NodesCanvas nodesCanvas;
        public NodesCanvas canvasNode;
        public Selector(NodesCanvas _canvasNode)
        {
            InitializeComponent();
            transforms = new Transforms(this);
            canvasNode = _canvasNode;
            this.Visibility = Visibility.Hidden;
            this.MouseMove += mouseMove;
            this.MouseUp += mouseUp;
            this.IsVisibleChanged += VisualChange;
            this.KeyUp += keyUp;
        }
        public void VisualChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.form.Width = 0;
            this.form.Height = 0;
        }
        public Point Position1
        {
            get { return ForPoint.GetPoint1WithScale(this.form, transforms); }
            set{ ChangePosition1(value); }
        }
        public Point Position2
        {
            get{ return ForPoint.GetPoint2WithScale(this.form, transforms); }
            set{ ChangePosition2(value);}
        }
        public void StartSelect(Point position)
        {
            Position1 = position;
            Visibility = Visibility.Visible;
            Mouse.Capture(this);
            Keyboard.Focus(this);
        }
        public void EndSelect()
        {
            Visibility = Visibility.Hidden;
        }
        public void mouseMove(object sender, MouseEventArgs e)
        {
            Position2 = Mouse.GetPosition(canvasNode);
            canvasNode.UpdateSeletedNodes();
            e.Handled = true;
        }
        public void mouseUp(object sender, MouseEventArgs e)
        {
            EndSelect();
        }
        public void keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                EndSelect();
            }
        }
        private void ChangePosition1(Point point)
        {
            //изменяем текущее место положения
            ForPoint.Equality(transforms.translate, point);
            ForPoint.EqualityCenter(transforms.scale, point);
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
