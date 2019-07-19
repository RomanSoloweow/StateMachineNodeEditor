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
using System.Globalization;
namespace StateMachineNodeEditor
{
   public class Connect: UIElement
    {
      
        public static readonly DependencyProperty TextProperty;
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty InputNodeProperty;
        public Node InputNode
        {
            get { return (Node)GetValue(InputNodeProperty); }
            set { SetValue(InputNodeProperty, value); }
        }
        public static readonly DependencyProperty OutputNodeProperty;
        public Node OutputNode
        {
            get { return (Node)GetValue(OutputNodeProperty); }
            set { SetValue(OutputNodeProperty, value); }
        }

        static Connect()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Connect), new FrameworkPropertyMetadata("Connect", FrameworkPropertyMetadataOptions.AffectsRender));
            InputNodeProperty = DependencyProperty.Register("InputNode", typeof(Node), typeof(Connect), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(InputShange)));
            OutputNodeProperty = DependencyProperty.Register("OutputNode", typeof(Node), typeof(Connect), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OutputShange)));
        }
        public static void InputShange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Node oldNode = (e.OldValue as Node);
            Node newNode = (e.NewValue as Node);
        }
        private static void OutputShange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Connect connect = (obj as Connect);
            Node oldNode = (e.OldValue as Node);
            Node newNode = (e.NewValue as Node);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }
}
