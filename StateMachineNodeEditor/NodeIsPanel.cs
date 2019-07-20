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
    public class NodeIsPanel:StackPanel
    {
       public NodeIsPanel()
        {
            this.Background = Brushes.Black;
            TextBox textBox = new TextBox();
            textBox.Text = "Text";
            Button button = new Button();
            this.Height = 500;
            button.Content = "Button";
            this.Children.Add(textBox);
            this.Children.Add(button);
            Ellipse ellipse = new Ellipse();
            ellipse.Height = 100;
            ellipse.Width = 100;
            ellipse.Margin = new Thickness(-50,0,0,0);
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.Stroke = Brushes.Green;
            this.Children.Add(ellipse);
            TextBox textBox2 = new TextBox();
            textBox2.Text = "Text ещё один";
            this.Children.Add(textBox2);
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
