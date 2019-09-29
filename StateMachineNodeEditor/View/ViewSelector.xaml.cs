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
    /// Interaction logic for ViewSelector.xaml
    /// </summary>
    public partial class ViewSelector : UserControl, IViewFor<ViewModelSelector>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(ViewModelSelector), typeof(ViewSelector), new PropertyMetadata(null));

        public ViewModelSelector ViewModel
        {
            get { return (ViewModelSelector)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelSelector)value; }
        }
        #endregion ViewModel
        public ViewSelector()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                // Отображается ли выделение
                this.Bind(this.ViewModel, x => x.Visible, x => x.Visibility);

                //Ширина
                this.Bind(this.ViewModel, x => x.Width, x => x.Width);

                //Высота
                this.Bind(this.ViewModel, x => x.Height, x => x.Height);

                //Позиция X от левого верхнего угла
                this.Bind(this.ViewModel, x => x.Translate.Translates.Value.X, x => x.Translate.X);

                //Позиция Y от левого верхнего угла
                this.Bind(this.ViewModel, x => x.Translate.Translates.Value.Y, x => x.Translate.Y);

                //Масштаб по оси X
                this.Bind(this.ViewModel, x => x.Scale.Scales.Value.X, x => x.Scale.ScaleX);

                //Масштаб по оси Y
                this.Bind(this.ViewModel, x => x.Scale.Scales.Value.Y, x => x.Scale.ScaleY);

                //Точка масштабирования, координата X
                this.Bind(this.ViewModel, x => x.Scale.Center.Value.X, x => x.Scale.CenterX);

                //Точка масштабирования, координата Y
                this.Bind(this.ViewModel, x => x.Scale.Center.Value.Y, x => x.Scale.CenterY);

            });
        }
    }
}
