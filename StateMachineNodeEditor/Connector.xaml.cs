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
using System.ComponentModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class Connector : UserControl
    {
       
        public static readonly DependencyProperty PositionProperty;
        public static RoutedEvent PositionChangeEvent;
        public static readonly DependencyProperty NodeProperty;
        public Node Node
        {
            get { return (Node)GetValue(NodeProperty); }
            set { SetValue(NodeProperty, value); }
        }
        public NodesCanvas nodesCanvas;
        public string Text
        {
            get
            {
                return text.Text;
            }
            set
            {
                text.Text = value;               
            }
        }
        public bool TextIsEnable
        {
            get
            {
                return text.IsEnabled;
            }
            set
            {
                text.IsEnabled = value;
            }
        }
        public event RoutedEventHandler PositionChange
        {
            add { base.AddHandler(PositionChangeEvent, value); }
            remove { base.RemoveHandler(PositionChangeEvent, value); }
        }
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            protected set { SetValue(PositionProperty, value); }
        }
        static Connector()
        {
            PositionProperty = DependencyProperty.Register("Position", typeof(Point), typeof(Connector), new FrameworkPropertyMetadata(new Point(0,0),  new PropertyChangedCallback(PositionShange)));
            PositionChangeEvent = EventManager.RegisterRoutedEvent("PositionChange", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(Connector));
            NodeProperty = DependencyProperty.Register("InputNode", typeof(Node), typeof(Connector), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(NodeChange)));
        }

        public Connector()
        {
            InitializeComponent();
            form.DragOver += DragOvers;
            form.DragLeave += DragLeaves;
            form.Drop += Drops;
        }
        public Connector(string text) : this()
        {
            this.text.Text = text;
        }
        public Connector(Node node):this()
        {
            Node = node;
        }
        private void DragOvers(object sender, DragEventArgs args)
        {
            this.form.Stroke = Brushes.Pink;
        }
        private void DragLeaves(object sender, DragEventArgs args)
        {
            this.form.Stroke = Brushes.Black;
        }
        private void Drops(object sender, DragEventArgs args)
        {
            this.form.Stroke = Brushes.Pink;
        }
        public static void NodeChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connector connector = (obj as Connector);
            Node oldNode = (e.OldValue as Node);
            Node newNode = (e.NewValue as Node);
            if (oldNode != null)
            {
                newNode.PositionChange -= connector.LocationChange;
            }
            if (newNode != null)
            {
                newNode.PositionChange += connector.LocationChange;
            }
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine(e.Property.ToString());
            base.OnPropertyChanged(e);
        }
        private static void PositionShange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connector connector = obj as Connector;
            connector.RaiseEvent(new RoutedEventArgs(PositionChangeEvent, connector));
        }
        public void Distribute(HorizontalAlignment horizontalAlignment)
        {
            this.HorizontalAlignment = horizontalAlignment;
            text.HorizontalAlignment = horizontalAlignment;
            form.HorizontalAlignment = horizontalAlignment;
            double radius = form.Width / 2;
            text.Margin = new Thickness(radius + 4, 0, radius + 4, 0);
            if (this.HorizontalAlignment == HorizontalAlignment.Right)
            {
                form.Margin = new Thickness(0, 0, -radius, 0);
               
            }
            else
            {
                form.Margin = new Thickness(-radius, 0, 0, 0);
            }
        }
       

        public Connector(string text,Node userControl1):this(userControl1)
        {
            this.text.Text = text;
        }
        private void LocationChange(object sender, RoutedEventArgs e)
        {
            UpdateCenterLocation();
        }
        public void UpdateCenterLocation()
        {
            Point InputCenter = form.TranslatePoint(new Point(form.Width / 2, form.Height / 2), this);
            Point InpuCenterOnNode = this.TranslatePoint(InputCenter, Node);
            Position = Node.TranslatePoint(InpuCenterOnNode, Node.nodesCanvas);
            this.RaiseEvent(new RoutedEventArgs(PositionChangeEvent, this));

        }
    }
}
