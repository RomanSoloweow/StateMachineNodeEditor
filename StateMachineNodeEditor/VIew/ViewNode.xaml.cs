using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ReactiveUI;
using DynamicData;
using StateMachineNodeEditor.ViewModel;
using System.Reactive.Linq;
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
            SetupBinding();
            SetupEvents();
            SetupCommands();
        }
        #region Setup Binding
        private void SetupBinding()
        {
            this.WhenActivated(disposable =>
            {
                //BorderBrush (Рамка вокруг узла)
                this.OneWayBind(this.ViewModel, x => x.BorderBrush, x => x.Border.BorderBrush);

                //Name (заголовок узла)
                this.OneWayBind(this.ViewModel, x => x.Name, x => x.Header.Text);

                //Можно ли менять заголовок
                this.Bind(this.ViewModel, x => x.NameEnable, x => x.Header.IsEnabled);

                //Позиция X от левого верхнего угла
                this.OneWayBind(this.ViewModel, x => x.Point1.X, x => x.Translate.X);

                //Позиция Y от левого верхнего угла
                this.OneWayBind(this.ViewModel, x => x.Point1.Y, x => x.Translate.Y);

                //Отображаются ли переходы
                this.OneWayBind(this.ViewModel, x => x.TransitionsVisible, x => x.Transitions.Visibility);

                //Отображается ли кнопка свернуть
                this.OneWayBind(this.ViewModel, x => x.RollUpVisible, x => x.ButtonCollapse.Visibility);

                //Размеры
                this.WhenAnyValue(v => v.Border.ActualWidth, v => v.Border.ActualHeight, (width, height) => new Size(width, height))
                     .BindTo(this, v => v.ViewModel.Size);

                //Вход для соединения с этим узлом
                 this.OneWayBind(this.ViewModel, x => x.Input, x => x.Input.ViewModel);

                //Выход ( используется, когда список переходов свернут )
                this.OneWayBind(this.ViewModel, x => x.Output, x => x.Output.ViewModel);

                //Переходы
                this.OneWayBind(this.ViewModel, x => x.Transitions, x => x.Transitions.ItemsSource);
            });
        }
        #endregion Setup Binding
        #region Setup Events
        private void SetupEvents()
        {
            this.WhenActivated(disposable =>
            {
                this.Events().MouseLeftButtonDown.Subscribe(e => OnEventMouseLeftDowns(e));
                this.Events().MouseLeftButtonUp.Subscribe(e => OnEventMouseLeftUp(e));
                this.Events().MouseRightButtonDown.Subscribe(e => OnEventMouseRightDown(e));
                this.Events().MouseRightButtonUp.Subscribe(e => OnEventMouseRightUp(e));
                this.Events().MouseDown.Subscribe(e => OnEventMouseDowns(e));
                this.Events().MouseUp.Subscribe(e => OnEventMouseUps(e));
                this.Events().MouseMove.Subscribe(e => OnMouseMove(e));
                this.Events().MouseEnter.Subscribe(e => OnEventMouseEnter(e));
                this.Events().MouseLeave.Subscribe(e => OnEventMouseMouseLeave(e));
                this.ButtonCollapse.Events().Click.Subscribe(_ => OnEventCollapse());
                this.Header.Events().TextChanged.Subscribe(e => Validate(e));
               
            });
        }
        private void OnEventMouseLeftDowns(MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
            this.ViewModel.CommandSelect.Execute(true);
        }
        private void Validate(TextChangedEventArgs e)
        {
            ViewModel.CommandValidateName.Execute(Header.Text);
        }
        private void OnEventMouseLeftUp(MouseButtonEventArgs e)
        {
        }
        private void OnEventMouseRightDown(MouseButtonEventArgs e)
        {

        }
        private void OnEventMouseRightUp(MouseButtonEventArgs e)
        {
        }
        private void OnEventMouseDowns(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                this.CaptureMouse();
                Keyboard.Focus(this);
            }
            e.Handled = true;
        }
        private void OnEventMouseUps(MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
        }
        //private void OnEventMouseMove(MouseButtonEventArgs e)
        //{
        //}
        private void OnEventMouseEnter(MouseEventArgs e)
        {
            if (this.ViewModel.Selected != true)
                this.ViewModel.BorderBrush = Application.Current.Resources["ColorNodeSelectedBorder"] as SolidColorBrush;
        }
        private void OnEventMouseMouseLeave(MouseEventArgs e)
        {
            if (this.ViewModel.Selected != true)
                this.ViewModel.BorderBrush = Application.Current.Resources["ColorNodeBorder"] as SolidColorBrush;
        }
        

        private void OnEventCollapse()
        {
            bool visible = (this.Rotate.Angle != 0);
            this.Rotate.Angle = visible ? 0:180;
            ViewModel.CommandCollapse.Execute(visible);
        }
        #endregion Setup Events
        #region Setup Commands
        private void SetupCommands()
        {
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.CommandSelect, x => x.BindingSelect);
            });
        }
        #endregion Setup Commands
    }
}
