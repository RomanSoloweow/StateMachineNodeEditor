using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using StateMachineNodeEditor.ViewModel;

namespace StateMachineNodeEditor.View
{
    /// <summary>
    /// Interaction logic for ViewCutter.xaml
    /// </summary>
    public partial class ViewCutter : UserControl, IViewFor<ViewModelCutter>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ViewModelCutter), typeof(ViewCutter), new PropertyMetadata(null));

        public ViewModelCutter ViewModel
        {
            get { return (ViewModelCutter)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelCutter)value; }
        }
        #endregion ViewModel
        public ViewCutter()
        {
            InitializeComponent();
            SetupBinding();
        }
        #region Setup Binding 
        private void SetupBinding()
        {
            this.WhenActivated(disposable =>
            {
                // Отображается ли линия среза
                this.OneWayBind(this.ViewModel, x => x.Visible, x => x.Visibility);

                // Точка из которой выходит линия среза
                this.OneWayBind(this.ViewModel, x => x.EndPoint.Value.X, x => x.Line.X1);
                // Точка из которой выходит линия среза
                this.OneWayBind(this.ViewModel, x => x.EndPoint.Value.Y, x => x.Line.Y1);

                // Точка в которую приходит линия среза
                this.OneWayBind(this.ViewModel, x => x.EndPoint.Value.X, x => x.Line.X2);
                // Точка в которую приходит линия среза
                this.OneWayBind(this.ViewModel, x => x.EndPoint.Value.Y, x => x.Line.Y2);

            });
        }

        #endregion Setup Binding 
    }
}
