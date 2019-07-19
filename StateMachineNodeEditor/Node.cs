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
        public static readonly DependencyProperty InputSelectBrushProperty;
        public Brush InputSelectBrush
        {
            get { return (Brush)GetValue(InputSelectBrushProperty); }
            set { SetValue(InputSelectBrushProperty, value); }
        }
        private static readonly DependencyProperty InputIsSelectProperty;
        private bool InputIsSelect
        {
            get { return (bool)GetValue(InputIsSelectProperty); }
            set { SetValue(InputIsSelectProperty, value); }
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
        public static readonly DependencyProperty OutputSelectBrushProperty;
        public Brush OutputSelectBrush
        {
            get { return (Brush)GetValue(OutputSelectBrushProperty); }
            set { SetValue(OutputSelectBrushProperty, value); }
        }
        private static readonly DependencyProperty OutputIsSelectProperty;
        private bool OutputIsSelect
        {
            get { return (bool)GetValue(OutputIsSelectProperty); }
            set { SetValue(OutputIsSelectProperty, value); }
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

        public MouseButtonEventHandler HeaderMouseDownEvent;
        public MouseButtonEventHandler HeaderMouseUpEvent;
        public MouseEventHandler HeaderMouseMoveEvent;
        public MouseEventHandler HeaderMouseEnterEvent;
        public MouseEventHandler HeaderMouseLeaveEvent;
        public MouseEventHandler HeaderWithMouseMoveEvent;

        public MouseButtonEventHandler InputMouseDownEvent;
        public MouseButtonEventHandler InputMouseUpEvent;
        public MouseEventHandler InputMouseMoveEvent;
        public MouseEventHandler InputMouseEnterEvent;
        public MouseEventHandler InputMouseLeaveEvent;
        public MouseEventHandler InputWithMouseMoveEvent;

        public MouseButtonEventHandler OutputMouseDownEvent;
        public MouseButtonEventHandler OutputMouseUpEvent;
        public MouseEventHandler OutputMouseMoveEvent;
        public MouseEventHandler OutputMouseEnterEvent;
        public MouseEventHandler OutputMouseLeaveEvent;
        public MouseEventHandler OutputWithMouseMoveEvent;

        public MouseButtonEventHandler BodyMouseDownEvent;
        public MouseButtonEventHandler BodyMouseUpEvent;
        public MouseEventHandler BodyMouseMoveEvent;
        public MouseEventHandler BodyMouseEnterEvent;
        public MouseEventHandler BodyMouseLeaveEvent;
        public MouseEventHandler BodyWithMouseMoveEvent;

        public EventHandler LocationChangeEvent;
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
        private Rect? _currentFigure;
        private Rect? _pressedFigure;

        public Point? _moveStartPoint = null;
        public Point test = new Point();
        private double zoom = 1;
        public UIElement parent;
        public IInputElement per;
        public bool canMove = true;
        public bool canScale = true;
        public double scales = 0.05;
        public double ScaleMax = 5;
        public double ScaleMin = 0.1;
        public double TranslateXMax = 10000;
        public double TranslateXMin = -10000;
        public double TranslateYMax = 10000;
        public double TranslateYMin = -10000;
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
            InputSelectBrushProperty = DependencyProperty.Register("InputSelectBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(Brushes.Green, FrameworkPropertyMetadataOptions.AffectsRender));
            InputIsSelectProperty = DependencyProperty.Register(" InputIsSelect", typeof(bool), typeof(Node), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
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
            OutputSelectBrushProperty = DependencyProperty.Register("OutputSelectBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(Brushes.Green, FrameworkPropertyMetadataOptions.AffectsRender));
            OutputIsSelectProperty = DependencyProperty.Register("OutputIsSelect", typeof(bool), typeof(Node), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
            OutputPenProperty = DependencyProperty.Register("OutputPen", typeof(Pen), typeof(Node), new FrameworkPropertyMetadata(new Pen()));
            #endregion
            #region Text
            OutputTextProperty = DependencyProperty.Register("OutputText", typeof(string), typeof(Node), new FrameworkPropertyMetadata("Output", FrameworkPropertyMetadataOptions.AffectsRender));
            OutputTextBrushProperty = DependencyProperty.Register("OutputTextBrush", typeof(Brush), typeof(Node), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255)), FrameworkPropertyMetadataOptions.AffectsRender));
            #endregion
            #endregion
            #endregion
        }
        public Node() : base(false)
        {
            this.Style = Application.Current.FindResource(typeof(Node)) as Style;
            Manager = new Managers(this);
            parent = this;
            Manager.translate.Changed += TransformChange;
            HeaderMouseDownEvent += HeaderMouseDown;
            HeaderMouseUpEvent += HeaderMouseUp;
            HeaderMouseMoveEvent += HeaderMouseMove;
            HeaderMouseEnterEvent += HeaderMouseEnter;
            HeaderMouseLeaveEvent += HeaderMouseLeave;
            HeaderWithMouseMoveEvent += HeaderWithMouseMove;

            InputMouseDownEvent += InputMouseDown;
            InputMouseUpEvent += InputMouseUp;
            InputMouseMoveEvent += InputMouseMove;
            InputMouseEnterEvent += InputMouseEnter;
            InputMouseLeaveEvent += InputMouseLeave;
            InputWithMouseMoveEvent += InputWithMouseMove;

            OutputMouseDownEvent += OutputMouseDown;
            OutputMouseUpEvent += OutputMouseUp;
            OutputMouseMoveEvent += OutputMouseMove;
            OutputMouseEnterEvent += OutputMouseEnter;
            OutputMouseLeaveEvent += OutputMouseLeave;
            OutputWithMouseMoveEvent += OutputWithMouseMove;

            BodyMouseDownEvent += BodyMouseDown;
            BodyMouseUpEvent += BodyMouseUp;
            BodyMouseMoveEvent += BodyMouseMove;
            BodyMouseEnterEvent += BodyMouseEnter;
            BodyMouseLeaveEvent += BodyMouseLeave;
            BodyWithMouseMoveEvent += BodyWithMouseMove;

            parent.MouseDown += mouseDown;
            parent.MouseUp += mouseUp;
            per = (IInputElement)VisualParent;
            // parent.MouseWheel += _MouseWheel;

            // MouseDown += mouseDown;
            // MouseUp += mouseUp;
            //MouseMove += mouseMove;
        }
        public Node(string text) : this()
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

        public void HeaderMouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }
        public void HeaderMouseUp(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }
        public void HeaderMouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }
        public void HeaderMouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.IBeam;
            base.OnMouseEnter(e);
        }
        public void HeaderMouseLeave(object sender, MouseEventArgs e)
        {
            base.OnMouseLeave(e);
        }
        public void HeaderWithMouseMove(object sender, MouseEventArgs e)
        {

        }

        public void InputMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        public void InputMouseUp(object sender, MouseButtonEventArgs e)
        {
        }
        public void InputMouseMove(object sender, MouseEventArgs e)
        {

        }
        public void InputMouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            InputIsSelect = true;
        }
        public void InputMouseLeave(object sender, MouseEventArgs e)
        {
            InputIsSelect = false;
        }
        public void InputWithMouseMove(object sender, MouseEventArgs e)
        {

        }

        public void OutputMouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        public void OutputMouseUp(object sender, MouseButtonEventArgs e)
        {
        }
        public void OutputMouseMove(object sender, MouseEventArgs e)
        {

        }
        public void OutputMouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            OutputIsSelect = true;
        }
        public void OutputMouseLeave(object sender, MouseEventArgs e)
        {
            OutputIsSelect = false;
        }
        public void OutputWithMouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("OutputWithMouseMove");
        }

        public void BodyMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Manager.canMove = true;
            canMove = true;
            test = e.GetPosition(this);
        }
        public void BodyMouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        public void BodyMouseMove(object sender, MouseEventArgs e)
        {

        }
        public void BodyMouseLeave(object sender, MouseEventArgs e)
        {

        }
        public void BodyMouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.SizeAll;
        }
        public void BodyWithMouseMove(object sender, MouseEventArgs e)
        {
            if (_moveStartPoint != null)
            {
                ((FrameworkElement)sender).Cursor = Cursors.SizeAll;
                Point Position = e.GetPosition(parent);
                double deltaX = (Position.X - _moveStartPoint.Value.X);
                double deltaY = (Position.Y - _moveStartPoint.Value.Y);
                bool XMax = ((deltaX > 0) && (Manager.translate.X > TranslateXMax));
                bool XMin = ((deltaX < 0) && (Manager.translate.X < TranslateXMin));
                bool YMax = ((deltaY > 0) && (Manager.translate.Y > TranslateYMax));
                bool YMin = ((deltaY < 0) && (Manager.translate.Y < TranslateXMin));
                if (XMax || XMin || YMax || YMin)
                    return;
                Manager.translate.X += deltaX;
                Manager.translate.Y += deltaY;
            }
            _moveStartPoint = e.GetPosition(parent);
        }

        public void mouseDown(object sender, MouseButtonEventArgs e)
        {
            _moveStartPoint = null;
            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                parent.CaptureMouse();
            }
        }

        public void mouseUp(object sender, MouseButtonEventArgs e)
        {
            _moveStartPoint = null;

            ((UIElement)sender).ReleaseMouseCapture();
            ((FrameworkElement)sender).Cursor = Cursors.Arrow;
        }
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
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);

            if (_input.Contains(position))
            {
                _pressedFigure = _input;
                InputMouseDownEvent.Invoke(this, e);
            }
            else if (_output.Contains(position))
            {
                _pressedFigure = _output;
                OutputMouseDownEvent.Invoke(this, e);
            }
            else if (_header.Contains(position))
            {
                _pressedFigure = _header;
                HeaderMouseDownEvent.Invoke(this, e);
            }
            else if (_body.Contains(position))
            {
                _pressedFigure = _body;
                BodyMouseDownEvent.Invoke(this, e);
            }
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {      
            if (_header==_pressedFigure)
            {
                HeaderMouseUpEvent.Invoke(this, e);
            }
            else if (_input==_pressedFigure)
            {
                InputMouseUpEvent.Invoke(this, e);               
            }
            else if (_output==_pressedFigure)
            {
                OutputMouseUpEvent.Invoke(this, e);               
            }        
            else if (_body==_pressedFigure)
            {
                BodyMouseUpEvent.Invoke(this, e);
            }

            canMove = false;
            _pressedFigure = null;
        }        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            bool same = false;
            if (_pressedFigure == null)
            {
                if (_header.Contains(position))
                {
                    if (_currentFigure == _header)
                    {
                        HeaderMouseMoveEvent.Invoke(this, e);
                        same = true;
                    }
                }
                else if (_input.Contains(position))
                {
                    if (_currentFigure == _input)
                    {
                        InputMouseMoveEvent.Invoke(this, e);
                        same = true;
                    }
                }
                else if (_output.Contains(position))
                {
                    if (_currentFigure == _output)
                    {
                        OutputMouseMoveEvent.Invoke(this, e);
                        same = true;
                    }
                }
                else if (_body.Contains(position))
                {
                    if (_currentFigure == _body)
                    {
                        BodyMouseMoveEvent.Invoke(this, e);
                        same = true;
                    }
                }

                if(!same)
                {
                    OnMouseLeave(e);
                    OnMouseEnter(e);
                }
            }
            else
            {
                if (_pressedFigure == _header)
                {
                    HeaderWithMouseMoveEvent.Invoke(this, e);
                }
                else if (_pressedFigure == _input)
                {
                    InputWithMouseMoveEvent.Invoke(this, e);
                }
                else if (_pressedFigure == _output)
                {
                    OutputWithMouseMoveEvent.Invoke(this, e);
                }
                else if (_pressedFigure == _body)
                {
                    BodyWithMouseMoveEvent.Invoke(this, e);
                }
            }
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            if (_input.Contains(position))
            {
                _currentFigure = _input;
                 InputMouseEnterEvent.Invoke(this, e);
            }
            else if (_output.Contains(position))
            {
                _currentFigure = _output;
                OutputMouseEnterEvent.Invoke(this, e);
            }
            else if (_header.Contains(position))
            {
                _currentFigure = _header;
                HeaderMouseEnterEvent.Invoke(this, e);
            }
            else if (_body.Contains(position))
            {
                _currentFigure = _body;
                BodyMouseEnterEvent.Invoke(this, e);
            }
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (_currentFigure == _header) 
            {
                HeaderMouseLeaveEvent.Invoke(this, e);
            }
            else if (_currentFigure == _input)
            {
                InputMouseLeaveEvent.Invoke(this, e);
            }
            else if (_currentFigure == _output)
            {
                OutputMouseLeaveEvent.Invoke(this, e);
            }
            else if (_currentFigure == _body)
            {
                BodyMouseLeaveEvent.Invoke(this, e);
            }

            this.Cursor = Cursors.Arrow;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this._width = Border.Left + base.ActualWidth + Border.Right;
            this._height = Border.Top + base.ActualHeight + Border.Bottom;
            Draw(drawingContext);      
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
                drawingContext.DrawRoundedRectangle(InputIsSelect?InputSelectBrush:InputBrush, InputPen, _input, _input.Width, _input.Height);
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
                drawingContext.DrawRoundedRectangle(OutputIsSelect ? OutputSelectBrush : OutputBrush, OutputPen, _output, _output.Width, _output.Height);
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
        private void LocationChange(object sender, EventArgs e)
        {

        }
        private void TransformChange(object sender, EventArgs e)
        {
            LocationChangeEvent.Invoke(sender, e);
        }
        #endregion Private Methods
    }
}
