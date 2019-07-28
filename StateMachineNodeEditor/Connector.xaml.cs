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
using System.Windows.Controls.Primitives;
namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class Connector : UserControl
    {
       
        public Point CenterLocation { get; protected set; }
        public Node node;
        static Connector()
        {          
        }
        public Connector()
        {
            InitializeComponent();
            AllowDrop = true;
            form.AllowDrop = true;

            this.form.DragEnter += DragOvers;
            this.form.DragLeave += DragLeaves;
            this.form.Drop += Drops;
        }
        public void SetNode(Node userControl1)
        {
            node = userControl1;
            node.LocationChange += LocationChange;
        }
        private void DragOvers(object sender, DragEventArgs args)
        {
            this.form.Stroke = Brushes.Green;
        }
        private void DragLeaves(object sender, DragEventArgs args)
        {
            this.form.Stroke = Brushes.White;
        }
        private void Drops(object sender, DragEventArgs args)
        {
            this.form.Stroke = Brushes.Pink;
        }

        public Connector(string text,Node userControl1):this()
        {
            this.text.Text = text;
            SetNode(userControl1);
        }
        private void LocationChange(object sender, RoutedEventArgs e)
        {
            UpdateCenterLocation();
        }
        public void UpdateCenterLocation()
        {
            Point InputCenter = form.TranslatePoint(new Point(form.Width / 2, form.Height / 2), this);
            CenterLocation = node.TranslatePoint(this.TranslatePoint(InputCenter, node), node.nodesCanvas);
        }
    }
}
