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

namespace StateMachineNodeEditor.View
{
    /// <summary>
    /// Interaction logic for ViewLeftConnector.xaml
    /// </summary>
    public partial class ViewLeftConnector : UserControl, IViewFor<ViewModelConnector>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ViewModelConnector), typeof(ViewLeftConnector), new PropertyMetadata(null));

        public ViewModelConnector ViewModel
        {
            get { return (ViewModelConnector)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelConnector)value; }
        }
        #endregion ViewModel
        public ViewLeftConnector()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                // Имя перехода ( вводится в узле)
                this.Bind(this.ViewModel, x => x.Name, x => x.Text.Text);

                // Доступно ли имя перехода для редактирования
                this.Bind(this.ViewModel, x => x.TextEnable, x => x.Text.IsEnabled);

                // Доступен ли переход для создания соединия
                this.Bind(this.ViewModel, x => x.FormEnable, x => x.Form.IsEnabled);

                // Цвет рамки, вокруг перехода
                this.Bind(this.ViewModel, x => x.FormStroke, x => x.Form.Stroke);

                // Цвет перехода
                this.Bind(this.ViewModel, x => x.FormFill, x => x.Form.Fill);

                // Отображается ли переход
                this.Bind(this.ViewModel, x => x.Visible, x => x.LeftConnector.Visibility);
            });
        }
    }
}
