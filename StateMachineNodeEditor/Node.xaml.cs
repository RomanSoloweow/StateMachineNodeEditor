﻿using System;
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
using System.Windows.Controls.Primitives;
namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public static RoutedEvent LocationChangeEvent;
        public event RoutedEventHandler LocationChange
        {
            add { base.AddHandler(LocationChangeEvent, value);}
            remove{ base.RemoveHandler(LocationChangeEvent, value);}
        }

        public Managers Manager { get; protected set; }
        public NodesCanvas nodesCanvas;
        public Point InputCenterLocation { get; protected set; }

        public Point OutputCenterLocation { get; protected set; }
        static Node()
        {
            LocationChangeEvent = EventManager.RegisterRoutedEvent("LocationChange", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(Node));
        }
        public Node()
        {
            InitializeComponent();
            Manager = new Managers(this);
            Manager.translate.Changed += TransformChange;
            LocationChange += LocationChanges;

            this.OutputForm.MouseEnter += OutputMouseEnter;
            //this.OutputForm.MouseDown += NewConnect;        
            this.Header.TextChanged+= TextBox_TextChanged;
            this.MainTransitions.SetNode(this);
            this.MainTransitions.text.IsEnabled = false;
            this.MainTransitions.MouseDown += NewConnect;
            this.InputForm.MouseDown += InputsMouseDown;
        }
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
        }
        public Node(string text,NodesCanvas _nodesCanvas) :this()
        {
            Header.Text = text;
            nodesCanvas = _nodesCanvas;
        }
        private void txtTarget_Drop(object sender, DragEventArgs e)
        {

        }
        private void LocationChanges(object sender, RoutedEventArgs e)
        {
            if (InputForm.IsVisible)
                UpdateInputCenterLocation();
            if (OutputForm.IsVisible)
                UpdateOutputCenterLocation();
        }
        private void TransformChange(object sender, EventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(LocationChangeEvent, this));
            //LocationChange.
            // LocationChangeEvent.Invoke(sender, );
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            RaiseEvent(new RoutedEventArgs(LocationChangeEvent, this));
            //LocationChange.Invoke(this, new EventArgs());
        }
        public void UpdateOutputCenterLocation()
        {
            Point OutputCenter = OutputForm.TranslatePoint(new Point(OutputForm.Width / 2, OutputForm.Height / 2), this);
            OutputCenterLocation = this.TranslatePoint(OutputCenter, nodesCanvas);
        }
        public void UpdateInputCenterLocation()
        {
            Point InputCenter = OutputForm.TranslatePoint(new Point(InputForm.Width / 2, InputForm.Height / 2), this);
            OutputCenterLocation = this.TranslatePoint(InputCenter, nodesCanvas);
        }


        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }
        public void Moves(object sender, EventArgs e)
        {
            //Console.WriteLine("Двигаем мышь");
        }

        public void NewConnect(object sender, MouseEventArgs e)
        {
          
            e.Handled = true;
            Connector control = new Connector("Transition "+Transitions.Children.Count.ToString(), this);
            Connect connect = nodesCanvas.AddConnect(control, MainTransitions.CenterLocation);
            //MainTransitions.form.DragDelta += connect.OnThumbDragDelta;



            //Mouse.Capture(nodesCanvas, CaptureMode.SubTree);
            //this.nodesCanvas.MouseMove += connect.HeaderMouseMove;

            //connect.CaptureMouse();

            DataObject data = new DataObject();
            //data.SetData("control", control);
            data.SetData("object", connect);
            DragDrop.DoDragDrop(connect, data, DragDropEffects.Link);
            this.Transitions.Children.Insert(1, control);

        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            Console.WriteLine("Node GiveFeedBack");
            base.OnGiveFeedback(e);
           
        }
        public void InputsMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("go");
        }
        public void OutputMouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool visible = (this.Rotate.Angle == 0);

            this.Rotate.Angle = visible?180:0;
            this.OutputForm.Visibility = visible? Visibility.Visible:Visibility.Hidden;
            this.OutputText.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            this.Transitions.Visibility= visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}