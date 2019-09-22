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
            this.WhenActivated(disposable =>
            {
                //BorderBrush (Рамка вокруг узла)
                this.Bind(this.ViewModel, x => x.BorderBrush, x => x.Border.BorderBrush);
                //Name (заголовок узла)
                this.Bind(this.ViewModel, x => x.Name, x => x.Header.Text);
                // Позиция X от левого верхнего угла
                this.Bind(this.ViewModel, x => x.Translate.X, x => x.Translate.X);
                // Позиция Y от левого верхнего угла
                this.Bind(this.ViewModel, x => x.Translate.Y, x => x.Translate.Y);
                // Масштаб по оси X
                this.Bind(this.ViewModel, x => x.Scale.ScaleX, x => x.Scale.ScaleX);
                // Масштаб по оси Y
                this.Bind(this.ViewModel, x => x.Scale.ScaleY, x => x.Scale.ScaleY);
                // Точка масштабирования, координата X
                this.Bind(this.ViewModel, x => x.Scale.CenterX, x => x.Scale.CenterX);
                // Точка масштабирования, координата Y
                this.Bind(this.ViewModel, x => x.Scale.CenterY, x => x.Scale.CenterY);
            });
        }
    }
}
