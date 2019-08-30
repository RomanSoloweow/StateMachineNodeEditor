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
    /// Interaction logic for ViewNode.xaml
    /// </summary>
    public partial class ViewNode : UserControl
    {
        public ViewNode()
        {
            InitializeComponent();
            //DataContext = new ViewModelNode(new ModelNode());
            this.SizeChanged += SizeChange;
        }
        private void SizeChange(object sender, EventArgs e)
        {
            ModelNode modelNode = DataContext as ModelNode;
            if (modelNode != null)
            {
                modelNode.Height = ActualHeight;
                modelNode.Width = ActualWidth;
            }
        }
    }
}
