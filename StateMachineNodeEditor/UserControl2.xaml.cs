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
            AllowDrop = true;
            form.AllowDrop = true;
            form.MouseEnter += HeaderMouseEnter;
            this.Drop += OnThumbDrop;
            form.DragStarted += OnThumbDragStarted;
            form.DragDelta += OnThumbDragDelta;
            form.PreviewDrop += OnThumbPreviewDrop;
            this.PreviewDrop += OnThumbPreviewDrop;

            //this.PreviewDragEnter += DragEn;
            //this.DragEnter += DragEn;
            //this.form.DragOver += DragOvers;
            this.form.DragEnter += DragOvers;
            this.form.DragLeave += DragLeaves;
            this.form.Drop += Drops;

            //this.form.DragEnter += DragEn;
            //this.PreviewDragOver += DragEn;

            form.DragCompleted += OnThumbDragCompleted;
            form.Drop += OnThumbDrop;
            form.QueryContinueDrag += OnThumbQueryContinueDragEventArgs;
        }
        public void SetNode(UserControl1 userControl1)
        {
            node = userControl1;
            node.LocationChange += LocationChange;
        }
        private void OnThumbDragStarted(object sender, DragStartedEventArgs args)
        {
        }
        private void OnThumbQueryContinueDragEventArgs(object sender, QueryContinueDragEventArgs args)
        {

        }
        private void DragOvers(object sender, DragEventArgs args)
        {
            this.form.Background = Brushes.Green;
        }
        private void DragLeaves(object sender, DragEventArgs args)
        {
            this.form.Background = Brushes.White;
        }
        private void Drops(object sender, DragEventArgs args)
        {
            this.form.Background = Brushes.Pink;
        }
        private void OnThumbEnter(object sender, DragEventArgs args)
        {

        }
        protected override void OnPreviewDragEnter(DragEventArgs e)
        {
            Console.WriteLine("OnPreviewDragEnter");
            base.OnPreviewDragEnter(e);
        }
        protected override void OnPreviewDragOver(DragEventArgs e)
        {
            Console.WriteLine("OnPreviewDragOver");
           // this.form.Background = Brushes.Blue;
            //this.form.BorderBrush = Brushes.Blue;
            base.OnPreviewDragOver(e);
        }
        protected override void OnDragEnter(DragEventArgs e)
        {
            Console.WriteLine("OnDragEnter");
            base.OnDragEnter(e);
        }
        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);
        }
        protected override void OnDragOver(DragEventArgs e)
        {
           // this.form.Background = Brushes.Blue;
           Console.WriteLine("OnDragOver");
            base.OnDragOver(e);
        }
        protected override void OnDrop(DragEventArgs e)
        {
           Console.WriteLine("OnDrop");
          //  this.form.Background = Brushes.Green;
            base.OnDrop(e);
        }
        private void DragEn(object sender, DragEventArgs args)
        {
            //Console.WriteLine("DragEn");
            //this.form.Background = Brushes.Green;
        }
        private void OnThumbDrop(object sender, DragEventArgs args)
        {

        }
        public void HeaderMouseEnter(object sender, MouseEventArgs e)
        {
            var t = Mouse.DirectlyOver;
        }
        private void OnThumbPreviewDrop(object sender, DragEventArgs args)
        {

        }
        private void OnThumbDragDelta(object sender, DragDeltaEventArgs args)
        {

        }
        private void OnThumbDragCompleted(object sender, DragCompletedEventArgs args)
        {
            var t = Mouse.DirectlyOver;
    
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
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
          
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            Console.WriteLine("Connector GiveFeedBack");
            base.OnGiveFeedback(e);

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
