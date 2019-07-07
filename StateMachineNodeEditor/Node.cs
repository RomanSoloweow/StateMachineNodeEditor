using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
namespace StateMachineNodeEditor
{
    public class Node: Text
    {
        public bool inputVisible = true;
        public bool outputVisible = true;

        private double _width;
        private double _height;
        private string _text;

        public static readonly DependencyProperty BorderProperty;
        public static readonly DependencyProperty PenProperty;
        public static readonly DependencyProperty RadiusProperty;

        Manager management;

        private Rect body = new Rect();
        private Rect body2 = new Rect();
        private Rect header = new Rect();
        private Rect header2 = new Rect();
        private Rect input = new Rect();
        private Rect output = new Rect();

        Point point=new Point(0,0);
        public Node(string text, Style textStyle):base(text, textStyle)
        {
            management = new Manager(this);
            base.MinWidth = 60;
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
            Point position = e.GetPosition(this);
            if (header.Contains(position))
            {
                base.OnMouseDown(e);
                return;
            }
            if (input.Contains(position))
            {
                management.canMove = false;
                return;
            }
            if (output.Contains(position))
            {
                management.canMove = false;
                Point point = new Point(100, 100);
                DataObject data = new DataObject();
                data.SetData(DataFormats.StringFormat, point.ToString());
                data.SetData("Object", this);
                point = position;
                //DragDrop.DoDragDrop(this, point, DragDropEffects.Move);
                return;
            }
            if (body.Contains(position))
            {
                management.canMove = true;
                return;
            }                 
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);
            if (header.Contains(position))
            {
                base.OnMouseUp(e);               
            }
            management.canMove = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            if (input.Contains(position))
            {
                this.Cursor = Cursors.Arrow;              
                return;
            }
            if (output.Contains(position))
            {
                this.Cursor = Cursors.Arrow;            
                return;
            }
            if (body.Contains(position))
            {
              this.Cursor = Cursors.SizeAll;
                return;
            }

            if (header.Contains(position))
            {
               this.Cursor = Cursors.IBeam;          
               base.OnMouseMove(e);
                return;
            }
            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DrawingVisual visual = new DrawingVisual();
                DrawingContext dc = visual.RenderOpen();
                Constants.nodeLine.Brush = Brushes.Green;
                dc.DrawLine(Constants.nodeLine, point, position);
                AddVisualChild(visual);
                dc.Close();                        
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {         
            base.OnRender(drawingContext);
            this._width = Constants.nodeTextBorder.Left + base.ActualWidth + Constants.nodeTextBorder.Right;
            this._height = Constants.nodeTextBorder.Top + base.ActualHeight + Constants.nodeTextBorder.Bottom;
            Draw(ref drawingContext);
        }
        protected override void OnDrop(DragEventArgs e)
        {
            if (e.Data.GetData("Object") ==this)
                return;
            
            if ((int)e.Effects== 3)
            base.OnDrop(e);            
        }
      
        private void Draw(ref DrawingContext drawingContext)
        {
            double space = 10;

            #region Draw body node
            body.X = (-Constants.nodeTextBorder.Left); 
            body.Y = (_height - Constants.nodeTextBorder.Top);
            body.Width = _width;
            body.Height = space*3+10*2;
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
            #region Draw connector    
            if (inputVisible)
            {
                input.Width = 10;
                input.Height = 10;
                input.X = (body.X - input.Width / 2);
                input.Y = (body.Y + space);
                FormattedText formattedText = new FormattedText("Input", CultureInfo.CurrentCulture, this.FlowDirection, this.FontFamily.GetTypefaces().ElementAt(4), FontSize, Brushes.White, this.FontSize);
                Point point = new Point(input.X + input.Width + 2, input.Y - Math.Abs(formattedText.Height - input.Height));
          
                drawingContext.DrawRoundedRectangle(Brushes.DarkGray, Constants.nodePen, input, input.Width, input.Height);
                drawingContext.DrawText(formattedText, point);
            }

            if (outputVisible)
            {
                output.Width = 10;
                output.Height = 10;
                output.X = (body.X + body.Width - output.Width/2);
                output.Y = (body.Y + body.Height - space - output.Height);
            
                FormattedText formattedText2 = new FormattedText("Output", CultureInfo.CurrentCulture, this.FlowDirection, this.FontFamily.GetTypefaces().ElementAt(4), this.FontSize, Brushes.White, this.FontSize);
                Point point2 = new Point(output.X - formattedText2.Width - output .Width - 2, output.Y - Math.Abs(formattedText2.Height - output.Height));
            
                drawingContext.DrawRoundedRectangle(Brushes.DarkGray, Constants.nodePen, output, output.Width, output.Height);
                drawingContext.DrawText(formattedText2, point2);
            }
            #endregion
        }
    }
}
