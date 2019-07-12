using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
namespace StateMachineNodeEditor
{
    public class Node : Text, ManagedElement
    {
        #region DependencyProperties
        #region Form
        public static readonly DependencyProperty BorderProperty;  
        public Thickness Border
        {
            get { return (Thickness)GetValue(BorderProperty); }
            set { SetValue(BorderProperty, value); }
        }
        #endregion Form
        #region Header
        public static readonly DependencyProperty HeaderRadiusProperty;
        public double HeaderRadius
        {
            get { return (double)GetValue(HeaderRadiusProperty); }
            set { SetValue(HeaderRadiusProperty, value); }
        }
        public static readonly DependencyProperty HeaderBrushProperty;
        public Brush HeaderBrush
        {
            get { return (Brush)GetValue(HeaderBrushProperty); }
            set { SetValue(HeaderBrushProperty, value); }
        }
        public static readonly DependencyProperty HeaderPenProperty;
        public Pen HeaderPen
        {
            get { return (Pen)GetValue(HeaderPenProperty); }
            set { SetValue(HeaderPenProperty, value); }
    }
        #endregion Header
        #region Body
        public static readonly DependencyProperty BodyRadiusProperty;
        public double BodyRadius
        {
            get { return (double)GetValue(BodyRadiusProperty); }
            set { SetValue(BodyRadiusProperty, value); }
        }
        public static readonly DependencyProperty BodyBrushProperty;
        public Brush BodyBrush
        {
            get { return (Brush)GetValue(BodyBrushProperty); }
            set { SetValue(BodyBrushProperty, value); }
        }
        public static readonly DependencyProperty BodyPenProperty;
        public Pen BodyPen
        {
            get { return (Pen)GetValue(BodyPenProperty); }
            set { SetValue(BodyPenProperty, value); }
        }
        #endregion Body
        public static readonly DependencyProperty InOutTextCultureProperty;
        public CultureInfo InOutTextCulture
        {
            get { return (CultureInfo)GetValue(InOutTextCultureProperty); }
            set { SetValue(InOutTextCultureProperty, value); }
        }
        public static readonly DependencyProperty InOutSpaceProperty;
        public double InOutSpace
        {
            get { return (double)GetValue(InOutSpaceProperty); }
            set { SetValue(InOutSpaceProperty, value); }
        }
        #region Input
        #region Figure
        public static readonly DependencyProperty InputVisibleProperty;
        public bool InputVisible
        {
            get { return (bool)GetValue(InputVisibleProperty); }
            set { SetValue(InputVisibleProperty, value); }
        }
        public static readonly DependencyProperty InputSizeProperty;
        public Size InputSize
        {
            get { return (Size)GetValue(InputSizeProperty); }
            set { SetValue(InputSizeProperty, value); }
        }
        public static readonly DependencyProperty InputBrushProperty;
        public Brush InputBrush
        {
            get { return (Brush)GetValue(InputBrushProperty); }
            set { SetValue(InputBrushProperty, value); }
        }
        public static readonly DependencyProperty InputPenProperty;
        public Pen InputPen
        {
            get { return (Pen)GetValue(InputPenProperty); }
            set { SetValue(InputPenProperty, value); }
        }
        #endregion Figure
        #region Text
        public static readonly DependencyProperty InputTextProperty;
        public string InputText
        {
            get { return (string)GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }
        public static readonly DependencyProperty InputTextBrushProperty;
        public Brush InputTextBrush
        {
            get { return (Brush)GetValue(InputTextBrushProperty); }
            set { SetValue(InputTextBrushProperty, value); }
        }
        #endregion
        #endregion Input
        #region Output
        #region Figure
        public static readonly DependencyProperty OutputVisibleProperty;
        public bool OutputVisible
        {
            get { return (bool)GetValue(OutputVisibleProperty); }
            set { SetValue(OutputVisibleProperty, value); }
        }
        public static readonly DependencyProperty OutputSizeProperty;
        public Size OutputSize
        {
            get { return (Size)GetValue(OutputSizeProperty); }
            set { SetValue(OutputSizeProperty, value); }
        }
        public static readonly DependencyProperty OutputBrushProperty;
        public Brush OutputBrush
        {
            get { return (Brush)GetValue(OutputBrushProperty); }
            set { SetValue(OutputBrushProperty, value); }
        }
        public static readonly DependencyProperty OutputPenProperty;
        public Pen OutputPen
        {
            get { return (Pen)GetValue(OutputPenProperty); }
            set { SetValue(OutputPenProperty, value); }
        }
        #endregion Figure
        #region Text
        public static readonly DependencyProperty OutputTextProperty;
        public string OutputText
        {
            get { return (string)GetValue(OutputTextProperty); }
            set { SetValue(OutputTextProperty, value); }
        }
        public static readonly DependencyProperty OutputTextBrushProperty;
        public Brush OutputTextBrush
        {
            get { return (Brush)GetValue(OutputTextBrushProperty); }
            set { SetValue(OutputTextBrushProperty, value); }
        }
        #endregion Text
        #endregion Output
        #endregion DependencyProperties
        #region Public properties
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
        #endregion Public properties   
        #region Private  Fields
        private Rect _body = new Rect();
        private Rect _body2 = new Rect();
        private Rect _header = new Rect();
        private Rect _header2 = new Rect();
        private Rect _input = new Rect();
        private Rect _output = new Rect();
        private double _width;
        private double _height;
        private string _text;
        #endregion Private  Fields
        #region Constructors
        static Node()
        {
            #region Inicial properties           
            #region Header
            HeaderRadiusProperty = DependencyProperty.Register("HeaderCornerRadius", typeof(double), typeof(Node), new FrameworkPropertyMetadata((double)5));
            HeaderBrushProperty = DependencyProperty.Register("HeaderBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(18, 61, 106)), FrameworkPropertyMetadataOptions.AffectsRender));
            HeaderPenProperty = DependencyProperty.Register("HeaderPen", typeof(Pen), typeof(Node), new FrameworkPropertyMetadata(new Pen(), FrameworkPropertyMetadataOptions.AffectsRender));
            #endregion
            #region Body
            BodyRadiusProperty = DependencyProperty.Register("BodyCornerRadius", typeof(double), typeof(Node), new FrameworkPropertyMetadata((double)5, FrameworkPropertyMetadataOptions.AffectsRender));
            BodyBrushProperty = DependencyProperty.Register("BodyBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(45, 45, 48)), FrameworkPropertyMetadataOptions.AffectsRender));
            BodyPenProperty = DependencyProperty.Register("BodyPen", typeof(Pen), typeof(Node), new FrameworkPropertyMetadata(new Pen(), FrameworkPropertyMetadataOptions.AffectsRender));
            #endregion
            InOutTextCultureProperty = DependencyProperty.Register("InOutTextCulture", typeof(CultureInfo), typeof(Node), new FrameworkPropertyMetadata(new System.Globalization.CultureInfo("en-US"), FrameworkPropertyMetadataOptions.AffectsRender));
            InOutSpaceProperty = DependencyProperty.Register("InOutSpace", typeof(double), typeof(Node), new FrameworkPropertyMetadata((double)10, FrameworkPropertyMetadataOptions.AffectsRender));
            BorderProperty = DependencyProperty.Register("Border", typeof(Thickness), typeof(Node), new FrameworkPropertyMetadata(new Thickness(10, 2, 10, 2), FrameworkPropertyMetadataOptions.AffectsRender));
            #region Input
            #region Figure
            InputVisibleProperty = DependencyProperty.Register("InputVisible", typeof(bool), typeof(Node), new FrameworkPropertyMetadata(true));
            InputSizeProperty = DependencyProperty.Register("InputSize", typeof(Size), typeof(Node), new FrameworkPropertyMetadata(new Size(10, 10), FrameworkPropertyMetadataOptions.AffectsRender));
            InputBrushProperty = DependencyProperty.Register("InputBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(92, 83, 83)), FrameworkPropertyMetadataOptions.AffectsRender));
            InputPenProperty = DependencyProperty.Register("InputPen", typeof(Pen), typeof(Node), new FrameworkPropertyMetadata(new Pen()));
            #endregion
            #region Text
            InputTextProperty = DependencyProperty.Register("InputText", typeof(string), typeof(Node), new FrameworkPropertyMetadata("Input", FrameworkPropertyMetadataOptions.AffectsRender));
            InputTextBrushProperty = DependencyProperty.Register("InputTextBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255)), FrameworkPropertyMetadataOptions.AffectsRender));
            #endregion
            #endregion
            #region Output
            #region Figure
            OutputVisibleProperty = DependencyProperty.Register("OutputVisible", typeof(bool), typeof(Node), new FrameworkPropertyMetadata(true));
            OutputSizeProperty = DependencyProperty.Register("OutputSize", typeof(Size), typeof(Node), new FrameworkPropertyMetadata(new Size(10, 10)));
            OutputBrushProperty = DependencyProperty.Register("OutputBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(92, 83, 83)), FrameworkPropertyMetadataOptions.AffectsRender));
            OutputPenProperty = DependencyProperty.Register("OutputPen", typeof(Pen), typeof(Node), new FrameworkPropertyMetadata(new Pen()));
            #endregion
            #region Text
            OutputTextProperty = DependencyProperty.Register("OutputText", typeof(string), typeof(Node), new FrameworkPropertyMetadata("Output", FrameworkPropertyMetadataOptions.AffectsRender));
            OutputTextBrushProperty = DependencyProperty.Register("OutputTextBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255)), FrameworkPropertyMetadataOptions.AffectsRender));
            #endregion
            #endregion
            #endregion
        }
        public Node():base(false)
        {
            this.Style = Application.Current.FindResource(typeof(Node)) as Style;
            Manager = new Managers(this);
        }
        public Node(string text):this()
        {
            this.Text = text;
        }
        #endregion Constructors
        #region Public Methods

        //public void mouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    _movePoint = null;
        //    if (Mouse.Captured == null)
        //    {
        //        Keyboard.ClearFocus();
        //        parent.CaptureMouse();
        //    }
        //}
        //public void mouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    _movePoint = null;

        //    ((UIElement)sender).ReleaseMouseCapture();
        //    ((FrameworkElement)sender).Cursor = Cursors.Arrow;
        //}

        //public void mouseMove(object sender, MouseEventArgs e)
        //{
        //    if ((Mouse.LeftButton != MouseButtonState.Pressed) || (!canMove))
        //        return;
        //    if (Mouse.Captured == parent)
        //    {
        //        if (_movePoint != null)
        //        {
        //            ((FrameworkElement)sender).Cursor = Cursors.SizeAll;
        //            Point Position = e.GetPosition(parent);
        //            double deltaX = (e.GetPosition(parent).X - _movePoint.Value.X);
        //            double deltaY = (e.GetPosition(parent).Y - _movePoint.Value.Y);
        //            bool XMax = ((deltaX > 0) && (translate.X > TranslateXMax));
        //            bool XMin = ((deltaX < 0) && (translate.X < TranslateXMin));
        //            bool YMax = ((deltaY > 0) && (translate.Y > TranslateYMax));
        //            bool YMin = ((deltaY < 0) && (translate.Y < TranslateXMin));
        //            if (XMax || XMin || YMax || YMin)
        //                return;

        //            //foreach (var children in childrens)
        //            //{
        //            //    children.Manager.translate.X += deltaX / children.Manager.scale.ScaleX;
        //            //    children.Manager.translate.Y += deltaY / children.Manager.scale.ScaleY;
        //            //}
        //            //if (test)
        //            //{
        //            translate.X += deltaX;
        //            translate.Y += deltaY;
        //            // }
        //        }
        //        _movePoint = e.GetPosition(parent);
        //    }
        //}
        //private void _MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (Mouse.Captured != null)
        //        return;
        //    bool Delta0 = (e.Delta == 0);
        //    bool DeltaMax = ((e.Delta > 0) && (zoom > ScaleMax));
        //    bool DeltaMin = ((e.Delta < 0) && (zoom < ScaleMin));
        //    if (Delta0 || DeltaMax || DeltaMin)
        //        return;

        //    zoom += (e.Delta > 0) ? scales : -scales;
        //    //foreach (var children in childrens)
        //    //{
        //    //    children.Manager.scale.ScaleX = zoom;
        //    //    children.Manager.scale.ScaleY = zoom;
        //    //}
        //    scale.ScaleX = zoom;
        //    scale.ScaleY = zoom;
        //}
        #endregion Public Methods
        #region Protected Methods
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Text))
            {
                base.OnTextChanged(e);
                this._text = base.Text;
            }
            else
            {
                base.Text = this._text;
            }
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
           // this.OutputBrush = Brushes.DarkGray;
            base.OnMouseLeave(e);
        }
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
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
                return;
            }
            if (_output.Contains(position))
            {          
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
            Manager.canMove = false;
            if (_header.Contains(position))
            {
                base.OnMouseUp(e);
                return;
            }
            //if (_body.Contains(position))
            //{
            //    Manager.canMove = true;
            //    return;
            //}
     
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            #region test visual
            //DrawingVisual visual = new DrawingVisual();
            //using (DrawingContext dc = visual.RenderOpen())
            //{
            //    Typeface typeface = new Typeface("Normal");
            //    FormattedText ft = new FormattedText("Input", InOutTextCulture, this.FlowDirection, typeface, this.FontSize, Brushes.Blue, 100);
            //    dc.DrawText(ft, position);
            //    Geometry geometry = ft.BuildGeometry(position);
            //    dc.DrawGeometry(Brushes.Blue, new Pen(BorderBrush, 30), geometry);
            //}
            //this.AddVisualChild(visual);
            #endregion
            if (_input.Contains(position))
            {
                this.Cursor = Cursors.Arrow;
                return;
            }
            if (_output.Contains(position))
            {
                this.Cursor = Cursors.Arrow;
                 this.OutputBrush = Brushes.Green;
                    return;
            }
            this.OutputBrush = Brushes.DarkGray;
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
            Draw(drawingContext);      
        }
        protected override void OnDrop(DragEventArgs e)
        {
            if (e.Data.GetData("Object") == this)
                return;

            if ((int)e.Effects == 3)
                base.OnDrop(e);
        }
        #endregion Protected Methods
        #region Private Methods
        private void Draw(DrawingContext drawingContext)
        {        
            #region Draw _body node
            _body.X = (-Border.Left);
            _body.Y = (_height - Border.Top);
            _body.Width = _width;
            _body.Height = InOutSpace * 3 + InputSize.Height + OutputSize.Height;
            drawingContext.DrawRoundedRectangle(BodyBrush, BodyPen, _body, BodyRadius, BodyRadius);
            #region  _body2
            if (BodyRadius > 0)
            {
                _body2.X = _body.X;
                _body2.Y = _body.Y;
                _body2.Width = _body.Width;
                _body2.Height = BodyRadius;
                drawingContext.DrawRectangle(BodyBrush, BodyPen, _body2);
            }
            #endregion
            #endregion
            #region Draw _header node            
            _header.X = -Border.Left;
            _header.Y = -Border.Top;
            _header.Width = _width;
            _header.Height = _height;
            drawingContext.DrawRoundedRectangle(HeaderBrush, HeaderPen, _header, HeaderRadius, HeaderRadius);
            #region _header2
            if (HeaderRadius > 0)
            {
            _header2.X = _header.X;
            _header2.Y = _header.Y + _header.Height - HeaderRadius;
            _header2.Width = _header.Width;
            _header2.Height = HeaderRadius;
            drawingContext.DrawRectangle(HeaderBrush, HeaderPen, _header2);
            }
            #endregion
            #endregion
            #region Draw connector    
            Typeface typeface = new Typeface("Normal");
            if (InputVisible)
            {
                #region Figure
                _input.Width = InputSize.Width;
                _input.Height = InputSize.Height;
                _input.X = (_body.X - _input.Width / 2);
                _input.Y = (_body.Y + InOutSpace);
                drawingContext.DrawRoundedRectangle(InputBrush, InputPen, _input, _input.Width, _input.Height);
                #endregion
                #region Text
                FormattedText inputText = new FormattedText(InputText, InOutTextCulture, this.FlowDirection, typeface, this.FontSize, InputTextBrush, this.FontSize);
                Point inputTextPoint = new Point((_input.X + _input.Width + 2), (_input.Y - Math.Abs(inputText.Height - _input.Height)));             
                drawingContext.DrawText(inputText, inputTextPoint);
                #endregion
            }
            if (OutputVisible)
            {
                #region Figure
                _output.Width = OutputSize.Width;
                _output.Height = OutputSize.Height;
                _output.X = (_body.X + _body.Width - _output.Width / 2);
                _output.Y = (_body.Y + _body.Height - InOutSpace - _output.Height);
                drawingContext.DrawRoundedRectangle(OutputBrush, OutputPen, _output, _output.Width, _output.Height);
                #endregion
                #region Text
                FormattedText outputText = new FormattedText(OutputText, InOutTextCulture, this.FlowDirection, typeface, this.FontSize, OutputTextBrush, this.FontSize);
                Point outputTextPoint = new Point((_output.X - outputText.Width - 2), (_output.Y - Math.Abs(outputText.Height - _output.Height)));            
                drawingContext.DrawText(outputText, outputTextPoint);
                #endregion
                #region Test Typefaces
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
                #endregion
            }
            #endregion
        }
        #endregion Private Methods
    }
}
