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
using System.Windows.Media.Effects;

namespace StateMachineNodeEditor
{
    public class Node: Text
    {
        private double _width;
        private double _height;
        private string _text;

        public static readonly DependencyProperty BorderProperty;
        public static readonly DependencyProperty PenProperty;
        public static readonly DependencyProperty RadiusProperty;

        Management management;
        private Rect body   =  new Rect();
        private Rect body2 = new Rect();
        private Rect header = new Rect();
        private Rect header2 = new Rect();
        public Node(string text, Style textStyle):base(text, textStyle)
        {
            management = new Management(this);
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Text))
            {
                base.OnTextChanged(e);
                this._text = base.Text;
            }
            else
              base.Text= this._text;
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (header.Contains(e.GetPosition(this)))
            {
                management.canMove = false;
                base.OnMouseDown(e);
            }            
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (header.Contains(e.GetPosition(this)))
            {
                base.OnMouseUp(e);               
            }
            management.canMove = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (body.Contains(e.GetPosition(this)))
            {
              this.Cursor = Cursors.SizeAll;
            }

            if (header.Contains(e.GetPosition(this)))
            {           
               this.Cursor = Cursors.IBeam;          
                base.OnMouseMove(e);
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {         
            base.OnRender(drawingContext);      
            this._width = Constants.nodeTextBorder.Left + base.ActualWidth + Constants.nodeTextBorder.Right;
            this._height = Constants.nodeTextBorder.Top + base.ActualHeight + Constants.nodeTextBorder.Bottom;
            Draw(ref drawingContext);
        }

        private void Draw(ref DrawingContext drawingContext)
        {
            #region Draw body node
            body.X = (-Constants.nodeTextBorder.Left); 
            body.Y = (_height - Constants.nodeTextBorder.Top);
            body.Width = _width;
            body.Height = _height + 50;
            drawingContext.DrawRoundedRectangle(Constants.nodeBrushBody, Constants.nodePen, body, Constants.nodeRadius, Constants.nodeRadius);
            #region body2
            body2.X = body.X;
            body2.Y = body.Y;
            body2.Width = body.Width;
            body2.Height = Constants.nodeRadius;
            drawingContext.DrawRectangle(Constants.nodeBrushBody, Constants.nodePen, body2);
            #endregion
            #endregion     

            #region Draw header node  
            header.X = -Constants.nodeTextBorder.Left;
            header.Y = -Constants.nodeTextBorder.Top;
            header.Width = _width;
            header.Height = _height;
            drawingContext.DrawRoundedRectangle(Constants.nodeBrushHeader, Constants.nodePen, header, Constants.nodeRadius, Constants.nodeRadius);
            #region header2
            header2.X = header.X;
            header2.Y = header.Y + header.Height - Constants.nodeRadius;
            header2.Width = header.Width;
            header2.Height = Constants.nodeRadius;
            drawingContext.DrawRectangle(Constants.nodeBrushHeader, Constants.nodePen, header2);
            #endregion
            #endregion
        }
    }
}
