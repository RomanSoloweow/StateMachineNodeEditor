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

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public static readonly DependencyProperty ReraProperty;
        public string Rera
        {
            get { return (string)GetValue(ReraProperty); }
            set { SetValue(ReraProperty, value); }
        }
        public string tt;
        static UserControl2()
        {
            
            ReraProperty = DependencyProperty.Register("Rera", typeof(string), typeof(Nodess), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        }
        public UserControl2()
        {
           
            InitializeComponent();

        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            Rera = "Privet";
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            Rera = "Poka";
        }



    }
}
