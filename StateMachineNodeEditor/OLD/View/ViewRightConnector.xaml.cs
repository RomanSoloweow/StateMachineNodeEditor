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
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if(e.Property!=null)
            Console.WriteLine(e.Property.Name.ToString());
        }
        public ViewModelConnector ViewModelConnector { get; set; }
        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            object data = ViewModelConnector.CommandGetDataForDrag.Execute(null);
            DragDropEffects result = DragDrop.DoDragDrop(this, data, DragDropEffects.Link);
            ViewModelConnector.CommandAddConnectIfDrop.Execute(null);
            e.Handled = true;
        }
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
        }
        public void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModelConnector = e.NewValue as ViewModelConnector;
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (ViewModelConnector != null)
            {
               // ViewModelConnector.Position = (this.VisualParent as UIElement).TranslatePoint(form.TranslatePoint(new Point(form.Width, form.Height / 2), this), this);

                Point InputCenter = form.TranslatePoint(new Point(form.Width, form.Height / 2), this);
                UIElement visualParent = (this.VisualParent as UIElement);
                Point InpuCenterOnNode = this.TranslatePoint(InputCenter, visualParent);
                ViewModelConnector.Position = InpuCenterOnNode;
                //var t = visualParent.FindCommonVisualAncestor(this);
                //(visualParent as Visual).FindCommonVisualAncestor()
                //ViewModelConnector.Position = InpuCenterOnNode;
            }

        }
    }
}
