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
       
        public Point CenterLocation { get; protected set; }
        public UserControl1 node;
        static UserControl2()
        {          
        }
        public UserControl2()
        {
            InitializeComponent();
        }
        public void SetNode(UserControl1 userControl1)
        {
            node = userControl1;
            node.LocationChange += LocationChange;
        }
        public UserControl2(string text,UserControl1 userControl1):this()
        {
            this.Text.Text = text;
            SetNode(userControl1);
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }
        private void LocationChange(object sender, RoutedEventArgs e)
        {
            UpdateCenterLocation();
        }
        public void UpdateCenterLocation()
        {
            Point InputCenter = Form.TranslatePoint(new Point(Form.Width / 2, Form.Height / 2), this);
            CenterLocation = node.TranslatePoint(this.TranslatePoint(InputCenter, node), node.nodesCanvas);
        }

    }
}
