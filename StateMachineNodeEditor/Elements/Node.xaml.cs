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
using System.Windows.Controls.Primitives;
namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Node : UserControl, ICloneable
    {
        public static RoutedEvent PositionChangeEvent;
        public event RoutedEventHandler PositionChange
        {       
            add { base.AddHandler(PositionChangeEvent, value);}
            remove { base.RemoveHandler(PositionChangeEvent, value); }
        }
        public static RoutedEvent ZoomChangeEvent;
        public event RoutedEventHandler ZoomChange
        {
            add { base.AddHandler(ZoomChangeEvent, value); }
            remove { base.RemoveHandler(ZoomChangeEvent, value); }
        }
        public static RoutedEvent BeforeDeleteEvent;
        public event RoutedEventHandler BeforeDelete
        {
            add { base.AddHandler(BeforeDeleteEvent, value); }
            remove { base.RemoveHandler(BeforeDeleteEvent, value); }
        }
        public Point Point1
        {
            get { return ForPoint.GetValueAsPoint(Manager.translate); }
        }
        public Point Point2
        {
            get {
                Point point1 = Point1;
                return new Point(point1.X + Border.ActualWidth, point1.Y + Border.ActualHeight); 
               }
        }
        public Managers Manager { get;  set; }
        public Connector Input;
        public Connector Output;
        //public NodesCanvas nodesCanvas;
        public NodesCanvas nodesCanvas;
        public string Text
        {
            get { return Header.Text; }
            set { Header.Text = value; }
        }
        public Point InputCenterLocation { get; protected set; }
        public static readonly DependencyProperty CurrentConnectorProperty;
        public Connector CurrentConnector
        {
            get { return (Connector)GetValue(CurrentConnectorProperty); }
            set { SetValue(CurrentConnectorProperty, value); }
        }
        public Point OutputCenterLocation { get; protected set; }
        public static readonly DependencyProperty SelectedProperty;
        public object Clone()
        {
            Node node = new Node();
            //node.Manager._transformGroup = (TransformGroup)this.Manager._transformGroup.Clone();
            //node.Name = this.Name + "_";
            //node.Text = this.Text + "_";
            return node;
        }
        public bool? Selected
        {
            get { return (bool?)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }
        static Node()
        {           
            PositionChangeEvent = EventManager.RegisterRoutedEvent("PositionChange", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(Node));
            ZoomChangeEvent = EventManager.RegisterRoutedEvent("ZoomChange", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(Node));
            BeforeDeleteEvent = EventManager.RegisterRoutedEvent("BeforeDelete", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(Node));
            CurrentConnectorProperty = DependencyProperty.Register("currentConnector", typeof(Connector), typeof(Node), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            SelectedProperty = DependencyProperty.Register("Selected", typeof(bool?), typeof(Node), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(Select)));
        }
        public static void Select(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Node node = (obj as Node);
            bool? selected = (e.NewValue as bool?);
            if (selected == true)
            {
                node.SetColorOnSelect();
            }
            else
            {
                node.SetColorOnUnSelect();
            }
        }
        private void SetColorOnSelect()
        {
            Border.BorderBrush = Brushes.Red;
        }
        private void SetColorOnUnSelect()
        {
            Border.BorderBrush = Brushes.DarkGray;
        }
        public void AddInputOutput()
        {
            Input = new Connector(this)
            {
                Name = "Input",
                Text = "Input",
                TextIsEnable = false,
                CanDelete = false

             };

            MainPanel.Children.Add(Input);
            Grid.SetRow(Input, 0);
            Grid.SetColumn(Input, 0);
            Input.Distribute(HorizontalAlignment.Left);

            Output = new Connector(this)
            {
                Name = "Output",
                Text = "Output",
                TextIsEnable = false,
                CanDelete = false,
                Visibility =Visibility.Hidden
            };

            MainPanel.Children.Add(Output);
            Grid.SetRow(Output, 1);
            Grid.SetColumn(Output, 1);
            Output.Distribute(HorizontalAlignment.Right);
        }
        public Node()
        {
            InitializeComponent();
            AddInputOutput();
            Manager = new Managers(this);
            Manager.scale.Changed += Zoom;
            this.Output.form.MouseDown += NewConnect;
            this.Input.Drop += DropEnter;
            PositionChange += PositionChanges;
            this.MouseDown += mouseDown;
            this.MouseEnter += mouseEnter;
            this.MouseLeave += mouseLeave;
            //this.Transitions.MouseEnter += mouseEnter2;
            this.Border.SizeChanged += SizeChange;
            Manager.translate.Changed += TransformChange;            
            this.Header.TextChanged += TextBox_TextChanged;
            AddEmptyConnector();
        }
        
        public void mouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Selected!=true)
            {
                nodesCanvas.UnSelectedAllNodes();
                Selected = true;
            }
            e.Handled = true;
        }
        private void Select(object sender, ExecutedRoutedEventArgs e)
        {
            Selected = !Selected;
        }
        public void mouseEnter(object sender, MouseEventArgs e)
        {
            if (Selected!=true)
                SetColorOnSelect();
        }
        public void mouseEnter2(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }
        public void mouseLeave(object sender, MouseEventArgs e)
        {
            if (Selected != true)
                SetColorOnUnSelect();
        }
        public void Zoom(object sender, EventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PositionChangeEvent, this));
            RaiseEvent(new RoutedEventArgs(ZoomChangeEvent, this));
        }
        public void DropEnter(object sender, DragEventArgs e)
        {
            var obj = e.Data.GetData("Node");
            Node node = obj as Node;
            if((node!=null)&&(node!=this))
            {
                if(nodesCanvas.Check(node,this))
              ((Connect)e.Data.GetData("Connect")).OutputConnector = this.Input;
            }
        }
        public Node(string text, NodesCanvas _canvasNode) : this()
        {
            Header.Text = text;
            nodesCanvas = _canvasNode;
        }
        private void SizeChange(object sender, EventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PositionChangeEvent, this));
        }
        private void TransformChange(object sender, EventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PositionChangeEvent, this));
        }
        private void PositionChanges(object sender, RoutedEventArgs e)
        {
            //if (Input.IsVisible)
            //   UpdateInputCenterLocation();
            //if (Output.IsVisible)
            //    UpdateOutputCenterLocation();
        }
        public void UpdateOutputCenterLocation()
        {
           Point OutputCenter = Output.form.TranslatePoint(new Point(Output.form.Width / 2, Output.form.Height / 2), this);
            OutputCenterLocation = this.TranslatePoint(OutputCenter, nodesCanvas);
        }
        public void UpdateInputCenterLocation()
        {
             Point InputCenter = Input.form.TranslatePoint(new Point(Input.form.Width / 2, Input.form.Height / 2), this);
            InputCenterLocation = this.TranslatePoint(InputCenter, nodesCanvas);
        }
        private Connector AddEmptyConnector()
        {
            if (CurrentConnector != null)
            {
                CurrentConnector.text.IsEnabled = true;
                CurrentConnector.text.Text = CurrentConnector.Name;
                CurrentConnector.form.MouseDown -= NewConnect;
            }
            CurrentConnector = new Connector(this);
            CurrentConnector.text.IsEnabled = false;
            CurrentConnector.Name = "Transition_" + Transitions.Children.Count.ToString();
            CurrentConnector.form.MouseDown += NewConnect;
            this.Transitions.Children.Insert(0, CurrentConnector);
            return null;
        }      
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }
        public void NewConnect(object sender, MouseEventArgs e)
        {
            CurrentConnector.UpdateCenterLocation();
            Connector oldconnector = CurrentConnector;
            Connect connect = nodesCanvas.GetNewConnect(CurrentConnector.Position);
            connect.InputConnector = CurrentConnector;
            CurrentConnector.Connect = connect;


            DataObject data = new DataObject();
            data.SetData("Node", this);
            data.SetData("Control", CurrentConnector);
            data.SetData("Connect", connect);
            DragDropEffects result = DragDrop.DoDragDrop(connect, data, DragDropEffects.Link);

            if(connect.OutputConnector!=null)
            {
                AddEmptyConnector();
            }
            else
            {
                nodesCanvas.connects.Remove(connect);
            }
            e.Handled = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool visible = (this.Rotate.Angle == 0);
            this.Rotate.Angle = visible?180:0;
            this.Output.Visibility = visible? Visibility.Visible:Visibility.Hidden;
            this.Transitions.Visibility= visible ? Visibility.Collapsed : Visibility.Visible;
            UpdateOutputCenterLocation();
        }
        public Connector DeleteConnector(Connector connector)
        {
            if (connector != null)
                Transitions.Children.Remove(connector);
            return connector;
        }
        public Node Delete()
        {
            RaiseEvent(new RoutedEventArgs(BeforeDeleteEvent, this));
            nodesCanvas.DeleteNode(this);
            return this;
        }
    }
}
