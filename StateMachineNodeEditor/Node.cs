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
    public class Node : Text, ManagedElement
    {
        private double _width;
        private double _height;
        private string _text;
        #region property
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(Node), new PropertyMetadata((double)5));
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        public static readonly DependencyProperty BorderProperty = DependencyProperty.Register("Border", typeof(Thickness), typeof(Node), new PropertyMetadata(new Thickness(10, 2, 10, 2)));
        public Thickness Border
        {
            get { return (Thickness)GetValue(BorderProperty); }
            set { SetValue(BorderProperty, value); }
        }

        public static readonly DependencyProperty HeaderBrushProperty = DependencyProperty.Register("HeaderBrush", typeof(Brush), typeof(Node), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(18, 61, 106))));
        public Brush HeaderBrush
        {
            get { return (Brush)GetValue(HeaderBrushProperty); }
            set { SetValue(HeaderBrushProperty, value); }
        }
        public static readonly DependencyProperty HeaderPenProperty = DependencyProperty.Register("HeaderPen", typeof(Pen), typeof(Node), new PropertyMetadata(new Pen()));
        public Pen HeaderPen
        {
            get { return (Pen)GetValue(HeaderPenProperty); }
            set { SetValue(HeaderPenProperty, value); }
        }

        public static readonly DependencyProperty BodyBrushProperty = DependencyProperty.Register("BodyBrush", typeof(Brush), typeof(Node), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(45, 45, 48))));
        public Brush BodyBrush
        {
            get { return (Brush)GetValue(BodyBrushProperty); }
            set { SetValue(BodyBrushProperty, value); }
        }
        public static readonly DependencyProperty BodyPenProperty = DependencyProperty.Register("BodyPen", typeof(Pen), typeof(Node), new PropertyMetadata(new Pen()));
        public Pen BodyPen
        {
            get { return (Pen)GetValue(BodyPenProperty); }
            set { SetValue(BodyPenProperty, value); }
        }


        public static readonly DependencyProperty InOutTextCultureProperty = DependencyProperty.Register("InOutTextCulture", typeof(CultureInfo), typeof(Node), new PropertyMetadata(new System.Globalization.CultureInfo("en-US")));
        public CultureInfo InOutTextCulture
        {
            get { return (CultureInfo)GetValue(InOutTextCultureProperty); }
            set { SetValue(InOutTextCultureProperty, value); }
        }
        public static readonly DependencyProperty InOutSpaceProperty = DependencyProperty.Register("InOutSpace", typeof(double), typeof(Node), new PropertyMetadata((double)10));
        public double InOutSpace
        {
            get { return (double)GetValue(InOutSpaceProperty); }
            set { SetValue(InOutSpaceProperty, value); }
        }


        public static readonly DependencyProperty InputVisibleProperty = DependencyProperty.Register("InputVisible", typeof(bool), typeof(Node), new PropertyMetadata(true));
        public bool InputVisible
        {
            get { return (bool)GetValue(InputVisibleProperty); }
            set { SetValue(InputVisibleProperty, value); }
        }
        public static readonly DependencyProperty InputSizeProperty = DependencyProperty.Register("InputSize", typeof(Size), typeof(Node), new PropertyMetadata(new Size(10, 10)));
        public Size InputSize
        {
            get { return (Size)GetValue(InputSizeProperty); }
            set { SetValue(InputSizeProperty, value); }
        }
        public static readonly DependencyProperty InputBrushProperty = DependencyProperty.Register("InputBrush", typeof(Brush), typeof(Node), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(92, 83, 83))));
        public Brush InputBrush
        {
            get { return (Brush)GetValue(InputBrushProperty); }
            set { SetValue(InputBrushProperty, value); }
        }
        public static readonly DependencyProperty InputPenProperty = DependencyProperty.Register("InputPen", typeof(Pen), typeof(Node), new PropertyMetadata(new Pen()));
        public Pen InputPen
        {
            get { return (Pen)GetValue(InputPenProperty); }
            set { SetValue(InputPenProperty, value); }
        }

        public static readonly DependencyProperty OutputVisibleProperty = DependencyProperty.Register("OutputVisible", typeof(bool), typeof(Node), new PropertyMetadata(true));
        public bool OutputVisible
        {
            get { return (bool)GetValue(OutputVisibleProperty); }
            set { SetValue(OutputVisibleProperty, value); }
        }
        public static readonly DependencyProperty OutputSizeProperty = DependencyProperty.Register("OutputSize", typeof(Size), typeof(Node), new PropertyMetadata(new Size(10, 10)));
        public Size OutputSize
        {
            get { return (Size)GetValue(OutputSizeProperty); }
            set { SetValue(OutputSizeProperty, value); }
        }
        public static readonly DependencyProperty OutputBrushProperty = DependencyProperty.Register("OutputBrush", typeof(Brush), typeof(Node), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(92, 83, 83))));
        public Brush OutputBrush
        {
            get { return (Brush)GetValue(OutputBrushProperty); }
            set { SetValue(OutputBrushProperty, value); }
        }
        public static readonly DependencyProperty OutputPenProperty = DependencyProperty.Register("OutputPen", typeof(Pen), typeof(Node), new PropertyMetadata(new Pen()));
        public Pen OutputPen
        {
            get { return (Pen)GetValue(OutputPenProperty); }
            set { SetValue(OutputPenProperty, value); }
        }
        #endregion 
        public Rect Body
        {
            get
            {
                return _body;
            }
        }
        public Rect Header
        {
            get
            {
                return _header;
            }
        }
        public Rect Input
        {
            get
            {
                return _input;
            }
        }
        public Rect Output
        {
            get
            {
                return _output;
            }
        }

        public Managers Manager { get; set; }
        private Rect _body = new Rect();
        private Rect _body2 = new Rect();
        private Rect _header = new Rect();
        private Rect _header2 = new Rect();
        private Rect _input = new Rect();
        private Rect _output = new Rect();

        public Node(string text, Style textStyle) : base(text, textStyle)
        {
            this.Style = textStyle;
            Manager = new Managers(this);
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Text))
            {
                base.OnTextChanged(e);
                this._text = base.Text;
            }
            else
                base.Text = this._text;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);
            if (_header.Contains(position))
            {
                base.OnMouseDown(e);
                return;
            }
            if (_input.Contains(position))
            {
                Manager.canMove = false;
                return;
            }
            if (_output.Contains(position))
            {
                Manager.canMove = false;
                //DataObject data = new DataObject();
                //data.SetData(DataFormats.StringFormat, position.ToString());
                //data.SetData("Object", this);
                //DragDrop.DoDragDrop(this, point, DragDropEffects.Move);
                return;
            }
            if (_body.Contains(position))
            {
                Manager.canMove = true;
                return;
            }
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);
            if (_header.Contains(position))
            {
                base.OnMouseUp(e);
            }
            Manager.canMove = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            if (_input.Contains(position))
            {
                this.Cursor = Cursors.Arrow;
                return;
            }
            if (_output.Contains(position))
            {
                this.Cursor = Cursors.Arrow;
                return;
            }
            if (_body.Contains(position))
            {
                this.Cursor = Cursors.SizeAll;
                return;
            }

            if (_header.Contains(position))
            {
                this.Cursor = Cursors.IBeam;
                base.OnMouseMove(e);
                return;
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this._width = Border.Left + base.ActualWidth + Border.Right;
            this._height = Border.Top + base.ActualHeight + Border.Bottom;
            Draw(ref drawingContext);
        }
        protected override void OnDrop(DragEventArgs e)
        {
            if (e.Data.GetData("Object") == this)
                return;

            if ((int)e.Effects == 3)
                base.OnDrop(e);
        }

        private void Draw(ref DrawingContext drawingContext)
        {
            #region Draw _body node
            _body.X = (-Border.Left);
            _body.Y = (_height - Border.Top);
            _body.Width = _width;
            _body.Height = InOutSpace * 3 + InputSize.Height + OutputSize.Height;
            drawingContext.DrawRoundedRectangle(BodyBrush, BodyPen, _body, Radius, Radius);
            #region _body2
            _body2.X = _body.X;
            _body2.Y = _body.Y;
            _body2.Width = _body.Width;
            _body2.Height = Radius;
            drawingContext.DrawRectangle(BodyBrush, BodyPen, _body2);
            #endregion
            #endregion     

            #region Draw _header node  
            _header.X = -Border.Left;
            _header.Y = -Border.Top;
            _header.Width = _width;
            _header.Height = _height;
            drawingContext.DrawRoundedRectangle(HeaderBrush, HeaderPen, _header, Radius, Radius);
            #region _header2
            _header2.X = _header.X;
            _header2.Y = _header.Y + _header.Height - Radius;
            _header2.Width = _header.Width;
            _header2.Height = Radius;
            drawingContext.DrawRectangle(HeaderBrush, HeaderPen, _header2);
            #endregion
            #endregion
            #region Draw connector    

            Typeface typeface = new Typeface("Normal");
            if (InputVisible)
            {
                _input.Width = InputSize.Width;
                _input.Height = InputSize.Height;
                _input.X = (_body.X - _input.Width / 2);
                _input.Y = (_body.Y + InOutSpace);

                FormattedText formattedText = new FormattedText("Input", InOutTextCulture, this.FlowDirection, typeface, this.FontSize, Brushes.White, this.FontSize);
                Point point = new Point((_input.X + _input.Width + 2), (_input.Y - Math.Abs(formattedText.Height - _input.Height)));

                drawingContext.DrawRoundedRectangle(InputBrush, InputPen, _input, _input.Width, _input.Height);
                drawingContext.DrawText(formattedText, point);
            }

            if (OutputVisible)
            {
                _output.Width = OutputSize.Width;
                _output.Height = OutputSize.Height;
                _output.X = (_body.X + _body.Width - _output.Width / 2);
                _output.Y = (_body.Y + _body.Height - InOutSpace - _output.Height);


                FormattedText formattedText2 = new FormattedText("Output", InOutTextCulture, this.FlowDirection, typeface, this.FontSize, Brushes.White, this.FontSize);
                Point point2 = new Point((_output.X - formattedText2.Width - 2), (_output.Y - Math.Abs(formattedText2.Height - _output.Height)));


                drawingContext.DrawRoundedRectangle(OutputBrush, OutputPen, _output, _output.Width, _output.Height);
                drawingContext.DrawText(formattedText2, point2);


                //FormattedText formattedText;
                //Point points;
                //double y = _output.Y + 30;
                //for (int i=0; i<this.FontFamily.GetTypefaces().Count; i++)
                //{
                //    formattedText = new FormattedText("State", Constants.culture, this.FlowDirection, this.FontFamily.GetTypefaces().ElementAt(i), this.FontSize, Brushes.White, this.FontSize);
                //    y += 30;
                //    points = new Point(_output.X - formattedText2.Width - _output.Width - 2, y);
                //    drawingContext.DrawText(formattedText, points);
                //}
            }
            #endregion
        }
    }
}
