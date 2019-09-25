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
using ReactiveUI.Fody.Helpers;
using StateMachineNodeEditor.Helpers;
using ReactiveUI;
using ReactiveUI.Wpf;
using DynamicData;
using StateMachineNodeEditor.ViewModel;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData.Binding;

namespace StateMachineNodeEditor.View
{
    /// <summary>
    /// Interaction logic for ViewNodesCanvas.xaml
    /// </summary>
    public partial class ViewNodesCanvas : UserControl,IViewFor<ViewModelNodesCanvas>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(ViewModelNodesCanvas), typeof(ViewNodesCanvas), new PropertyMetadata(null));

        public ViewModelNodesCanvas ViewModel
        {
            get { return (ViewModelNodesCanvas)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelNodesCanvas)value; }
        }
        #endregion ViewModel
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Keyboard.Focus(this);
        }
        public ViewNodesCanvas()
        {
            InitializeComponent();
            

            this.WhenActivated(disposable =>
            {
                
                this.OneWayBind(this.ViewModel, x => x.Nodes, x => x.Nodes.ItemsSource);
                this.OneWayBind(this.ViewModel, x => x.commandi, x => x.deleteBinding.Command);
                this.OneWayBind(this.ViewModel, x => x.commandi, x => x.MenuItemAdd.Command);
            });
        }
    }
}
