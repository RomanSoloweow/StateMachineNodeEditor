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
        public static RoutedEvent PositionChangeEvent;
        public event RoutedEventHandler PositionChange
        {       
            add { base.AddHandler(PositionChangeEvent, value); Console.WriteLine("PositionChange"); }
            remove { base.RemoveHandler(PositionChangeEvent, value); }
        }
        public Managers Manager { get; protected set; }
        public Connector Input;
        public Connector Output;
        public NodesCanvas nodesCanvas;
        public Point InputCenterLocation { get; protected set; }
        public static readonly DependencyProperty currentConnectorProperty;
        public Connector currentConnector
        {
            get { return (Connector)GetValue(currentConnectorProperty); }
            set { SetValue(currentConnectorProperty, value); }
        }
        public Point OutputCenterLocation { get; protected set; }
        static Node()
        {
            PositionChangeEvent = EventManager.RegisterRoutedEvent("PositionChange", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(Node));
            currentConnectorProperty = DependencyProperty.Register("currentConnector", typeof(Connector), typeof(Node), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        }
        public void AddInputOutput()
        {
            Input = new Connector(this)
            {
                Name = "Input",
                Text = "Input",
                TextIsEnable=false
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
                Visibility=Visibility.Hidden
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
            PositionChange += PositionChanges;
            this.Border.SizeChanged += SizeChange;
            Manager.translate.Changed += TransformChange;            
            this.Header.TextChanged += TextBox_TextChanged;
            AddEmptyConnector();
        }

        public Node(string text, NodesCanvas _nodesCanvas) : this()
        {
            Header.Text = text;
            nodesCanvas = _nodesCanvas;
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
            if (Input.IsVisible)
               UpdateInputCenterLocation();
            if (Output.IsVisible)
                UpdateOutputCenterLocation();
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
            if (currentConnector != null)
            {
                 currentConnector.text.IsEnabled = true;
                 currentConnector.text.Text = currentConnector.Name;
                currentConnector.MouseDown -= NewConnect;  
            }
            currentConnector = new Connector(this);
            currentConnector.Name = "Transition_" + Transitions.Children.Count.ToString();
            currentConnector.MouseDown += NewConnect;
            this.Transitions.Children.Insert(0, currentConnector);
            this.Transitions.UpdateLayout();           
            return null;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }
        public void NewConnect(object sender, MouseEventArgs e)
        {
            currentConnector.UpdateCenterLocation();
            Connect connect = new Connect(currentConnector);
            connect.StartPoint = currentConnector.Position;
            nodesCanvas.AddConnect(connect);
            DataObject data = new DataObject();
            data.SetData("control", currentConnector);
            data.SetData("object", connect);
            DragDropEffects result = DragDrop.DoDragDrop(connect, data, DragDropEffects.Link);

            if (result == DragDropEffects.Link)
            {
                AddEmptyConnector();
            }
            else
            {
                nodesCanvas.connects.Remove(connect);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool visible = (this.Rotate.Angle == 0);

            this.Rotate.Angle = visible?180:0;
            this.Output.Visibility = visible? Visibility.Visible:Visibility.Hidden;
            this.Transitions.Visibility= visible ? Visibility.Collapsed : Visibility.Visible;
            UpdateOutputCenterLocation();
        }
    }
}
