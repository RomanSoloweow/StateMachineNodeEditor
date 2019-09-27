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
    /// Interaction logic for ViewNode.xaml
    /// </summary>
    public partial class ViewNode : UserControl, IViewFor<ViewModelNode>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ViewModelNode), typeof(ViewNode), new PropertyMetadata(null));

        public ViewModelNode ViewModel
        {
            get { return (ViewModelNode)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelNode)value; }
        }
        #endregion ViewModel

        public ViewNode()
        {
            InitializeComponent();
            SetupProperties();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.ViewModel.Translate.Value.X += 20;
        }
        private void SetupProperties()
        {
            this.WhenActivated(disposable =>
            {
                //BorderBrush (Рамка вокруг узла)
                this.Bind(this.ViewModel, x => x.BorderBrush, x => x.Border.BorderBrush);

                //Name (заголовок узла)
                this.Bind(this.ViewModel, x => x.Name, x => x.Header.Text);

                //Позиция X от левого верхнего угла
                this.Bind(this.ViewModel, x => x.Translate.Value.X, x => x.Translate.X);

                //Позиция Y от левого верхнего угла
                this.Bind(this.ViewModel, x => x.Translate.Value.Y, x => x.Translate.Y);

                //Масштаб по оси X
                this.Bind(this.ViewModel, x => x.Scale.Scales.X, x => x.Scale.ScaleX);

                //Масштаб по оси Y
                this.Bind(this.ViewModel, x => x.Scale.Scales.Y, x => x.Scale.ScaleY);

                //Точка масштабирования, координата X
                this.Bind(this.ViewModel, x => x.Scale.Center.X, x => x.Scale.CenterX);

                //Точка масштабирования, координата Y
                this.Bind(this.ViewModel, x => x.Scale.Center.Y, x => x.Scale.CenterY);

                //Вход для соединения с этим узлом
                this.Bind(this.ViewModel, x => x.Input, x => x.Input.ViewModel);

                //Выход ( используется, когда список переходов свернут )
                this.Bind(this.ViewModel, x => x.Output, x => x.Output.ViewModel);

                //Переходы
                this.OneWayBind(this.ViewModel, x => x.Transitions, x => x.Transitions.ItemsSource);
            });
        }

    }
}
