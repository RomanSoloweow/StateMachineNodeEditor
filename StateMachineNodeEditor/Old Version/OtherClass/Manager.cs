using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
namespace StateMachineNodeEditor
{
    public class Managers:Transforms
    {
        public Point? MovePoint { get; private set; } = null;
 
        public bool canMovie;
        public bool canScale;
        public double ScaleMax = 5;
        public double ScaleMin = 0.1;
        public double TranslateXMax = 10000;
        public double TranslateXMin = -10000;
        public double TranslateYMax = 10000;
        public double TranslateYMin = -10000;

        public double Zoom { get; set; } = 1;
        public double Scales { get;  set; } = 0.05;
        //public Point Position1
        //{
        //    get { return GetPosition1(translate);}
        //}
        //public Point Position2
        //{
        //    get { return GetPosition2(parent, translate); }
        //}       
        public Managers(FrameworkElement _parent):base(_parent)
        {
            parent.MouseDown += OnMouseDown;
            parent.MouseUp += OnMouseUp;
        }
        public void Down()
        {
            MovePoint = null;
            MovePoint = Mouse.GetPosition(parent);
                Keyboard.ClearFocus();
                parent.CaptureMouse();
                Keyboard.Focus(parent);
        }
        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Down();
        }
        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            MovePoint = null;
            parent.ReleaseMouseCapture();
        }
        public Point GetDeltaMove(Point? CurrentPosition=null)
        {
            Point result = new Point();

            if (CurrentPosition == null)
                CurrentPosition = Mouse.GetPosition(parent);
        
            if (MovePoint != null)
            {
                result = ForPoint.Subtraction(CurrentPosition.Value, MovePoint.Value);
            }
            MovePoint = CurrentPosition;
            return result;
        }
        public void Move(Point? CurrentPosition = null)
        {
            if (MovePoint != CurrentPosition)
            {
                Point delta = GetDeltaMove();
                Move(delta);
            }
        }
        public void Move(Point delta)
        {
            delta = ForPoint.DivisionOnScale(delta, scale);
            bool XMax = ((delta.X > 0) && (translate.X > TranslateXMax));
            bool XMin = ((delta.X < 0) && (translate.X < TranslateXMin));
            bool YMax = ((delta.Y > 0) && (translate.Y > TranslateYMax));
            bool YMin = ((delta.Y < 0) && (translate.Y < TranslateXMin));
            if (XMax || XMin || YMax || YMin)
                return;
            ForPoint.Addition(translate, delta);
        }
        public void Scale(int Delta)
        {
            bool Delta0 = (Delta == 0);
            bool DeltaMax = ((Delta > 0) && (Zoom > ScaleMax));
            bool DeltaMin = ((Delta < 0) && (Zoom < ScaleMin));
            if (Delta0 || DeltaMax || DeltaMin)
                return;

            Zoom += (Delta > 0) ? Scales : -Scales;
            ForPoint.EqualityScale(scale, Zoom);
        }
    }
}
