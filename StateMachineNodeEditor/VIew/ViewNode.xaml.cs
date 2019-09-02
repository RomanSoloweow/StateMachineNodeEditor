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
    public partial class ViewNode : UserControl
    {
        public ViewNode()
        {
            InitializeComponent();
            this.SizeChanged += SizeChange;
            this.DataContextChanged += DataContextChange;
            this.MouseDown += OnMouseDown;
            this.MouseUp += OnMouseUp;
        }
        public ViewModelNode ViewModelNode { get; set; }
        public void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModelNode = e.NewValue as ViewModelNode;
        }

        private void Select(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNode.CommandSelect.Execute(false);
        }
        
     
        private void SizeChange(object sender, EventArgs e)
        {
            ViewModelNode.Height = ActualHeight;
            ViewModelNode.Width = ActualWidth;
        }
        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                this.CaptureMouse();
                Keyboard.Focus(this);
                ViewModelNode.CommandSelect.Execute(true);
            }    
        }
        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
        }
    }
}
