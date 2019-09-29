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
using System.Windows.Controls.Primitives;

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
            SetupEvents();
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

                //Отображаются ли переходы
                this.Bind(this.ViewModel, x => x.TransitionsVisible, x => x.Transitions.Visibility);

                //Размеры
                this.WhenAnyValue(v => v.Border.ActualWidth, v => v.Border.ActualHeight, (width, height) => new Size(width, height))
                     .BindTo(this, v => v.ViewModel.Size);

                //Вход для соединения с этим узлом
                this.Bind(this.ViewModel, x => x.Input, x => x.Input.ViewModel);

                //Выход ( используется, когда список переходов свернут )
                this.Bind(this.ViewModel, x => x.Output, x => x.Output.ViewModel);

                //Переходы
                this.OneWayBind(this.ViewModel, x => x.Transitions, x => x.Transitions.ItemsSource);
            });
        }
        #region Events
        private void SetupEvents()
        {
            this.WhenActivated(disposable =>
            {
                this.Events().MouseLeftButtonDown.Subscribe(e => OnMouseLeftDown(e));
                this.Events().MouseLeftButtonUp.Subscribe(e => OnMouseLeftUp(e));
                this.Events().MouseRightButtonDown.Subscribe(e => OnMouseRightDown(e));
                this.Events().MouseRightButtonUp.Subscribe(e => OnMouseRightUp(e));
                this.Events().MouseDown.Subscribe(e => OnMouseDown(e));
                this.Events().MouseUp.Subscribe(e => OnMouseUp(e));
                this.Events().MouseMove.Subscribe(e => OnMouseMove(e));
                this.ButtonCollapse.Events().Click.Subscribe(_ => OnCollapse());
                //Collapse.Events().Click.
                //this.Collapse.Events().
                //this.Collapse.Events().MouseLeftButtonDown.Subscribe(_=> OnCollapse());

            });
        }
        private void OnMouseLeftDown(MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
        }
        private void OnMouseLeftUp(MouseButtonEventArgs e)
        {
        }
        private void OnMouseRightDown(MouseButtonEventArgs e)
        {

        }
        private void OnMouseRightUp(MouseButtonEventArgs e)
        {
        }
        private void OnMouseDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                this.CaptureMouse();
                Keyboard.Focus(this);
                //ViewModelNode.CommandSelect.Execute(true);
            }
        }
        private void OnMouseUp(MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
        }
        private void OnMouseMove(MouseButtonEventArgs e)
        {
        }

        private void OnCollapse()
        {
            bool visible = (this.Rotate.Angle != 0);
            this.Rotate.Angle = visible ? 0:180;
            ViewModel.CommandCollapse.Execute(visible);

        }
        #endregion Events
    }
}
