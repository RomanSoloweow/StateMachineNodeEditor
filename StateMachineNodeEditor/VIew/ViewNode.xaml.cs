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
        }
        public ViewModelNode ViewModelNode { get; set; }
        public void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModelNode = e.NewValue as ViewModelNode;
        }

        private void Select(object sender, ExecutedRoutedEventArgs e)
        {
            
            ViewModelNode.CommandSelect.Execute(e.Parameter);
            e.Handled = false;
            //Border.BorderBrush = ViewModelNode.BorderBrush;
        }
        
     
        private void SizeChange(object sender, EventArgs e)
        {
                ViewModelNode.Height = ActualHeight;
                ViewModelNode.Width = ActualWidth;
        }
    }
}
