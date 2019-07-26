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
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
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
        static UserControl1()
        {
            LocationChangeEvent = EventManager.RegisterRoutedEvent("LocationChange", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(UserControl1));
        }
        public UserControl1()
        {
            InitializeComponent();
            Manager = new Managers(this);
            Manager.translate.Changed += TransformChange;
            LocationChange += LocationChanges;
             
          
            this.OutputForm.MouseEnter += OutputMouseEnter;
            this.OutputForm.MouseDown += NewConnect;        
            this.Header.TextChanged+= TextBox_TextChanged;
            this.MainTransitions.Form.MouseDown += NewConnect;
            this.MainTransitions.SetNode(this);
            this.MainTransitions.Text.IsEnabled = false;
        }
        public UserControl1(string text,NodesCanvas _nodesCanvas) :this()
        {
            Header.Text = text;
            nodesCanvas = _nodesCanvas;
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

        public void NewConnect(object sender, MouseButtonEventArgs e)
        {
           
            UserControl2 control = new UserControl2("Transition "+Transitions.Children.Count.ToString(), this);
            this.Transitions.Children.Insert(1, control);
            nodesCanvas.AddConnect(control, MainTransitions.CenterLocation);
            //Transitions.Children.Add(control);

            // control.Rera = "Stop";
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
