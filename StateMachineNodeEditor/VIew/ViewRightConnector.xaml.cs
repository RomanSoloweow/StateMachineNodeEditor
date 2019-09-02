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
    /// Interaction logic for ViewConnector.xaml
    /// </summary>
    public partial class ViewRightConnector : UserControl
    {
        public ViewRightConnector()
        {
            InitializeComponent();
            this.DataContextChanged += DataContextChange;
            this.form.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }
        public ViewModelConnector ViewModelConnector { get; set; }
        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            object data = ViewModelConnector.CommandGetDataForDrop.Execute(null);
            DragDropEffects result = DragDrop.DoDragDrop(this, data, DragDropEffects.Link);
           object connect = ViewModelConnector.CommandGetResultDrop.Execute(null);
            if (connect != null)
                ViewModelConnector.CommandAddConnect.Execute(connect);
            //e.Handled = true;
        }
       
        public void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModelConnector = e.NewValue as ViewModelConnector;
        }
    }
}
