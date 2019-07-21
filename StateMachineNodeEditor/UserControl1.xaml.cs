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
        public UserControl1()
        {
            InitializeComponent();
            Managers manager = new Managers(this);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool visible = Rotate.Angle == 0;
            Rotate.Angle = visible?180:0;
            OutputForm.Visibility = visible? Visibility.Visible:Visibility.Hidden;
            OutputText.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            Transitions.Visibility= visible ? Visibility.Collapsed : Visibility.Visible;

        }
    }
}
