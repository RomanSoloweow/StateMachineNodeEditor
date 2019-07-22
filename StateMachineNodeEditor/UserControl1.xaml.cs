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
        public Managers Manager { get; protected set; }
        public UserControl1()
        {
            InitializeComponent();
            Manager = new Managers(this);
            this.OutputForm.MouseEnter += OutputMouseEnter;
            this.OutputForm.MouseDown += OutputMouseUp;
            UserControl2 control = new UserControl2();
            control.Form.MouseDown += OutputMouseUp;
            this.Transitions.Children.Add(control);
            //  this.InputForm.MouseUp += OutputMouseUp;
        }
        public UserControl1(string text):this()
        {
            Header.Text = text;
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        public void OutputMouseUp(object sender, MouseButtonEventArgs e)
        {
            UserControl2 control = new UserControl2();
            // control.Text.Text = Transitions.Children.Count.ToString();
         
            this.Transitions.Children.Add(control);
           // control.Rera = "Stop";
        }
        public void OutputMouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool visible = (this.Rotate.Angle == 0);

            this.Rotate.Angle = visible?180:0;
            this.OutputForm.Visibility = visible? Visibility.Visible:Visibility.Hidden;
            this.OutputText.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            this.Transitions.Visibility= visible ? Visibility.Collapsed : Visibility.Visible;

        }
    }
}
