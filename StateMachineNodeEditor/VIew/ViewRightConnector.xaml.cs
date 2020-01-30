using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using StateMachineNodeEditor.Helpers;
using ReactiveUI;
using DynamicData;
using StateMachineNodeEditor.ViewModel;
using System.Reactive.Linq;
using System.Windows.Controls.Primitives;

namespace StateMachineNodeEditor.View
{
    /// <summary>
    /// Interaction logic for ViewConnector.xaml
    /// </summary>
    public partial class ViewRightConnector : UserControl, IViewFor<ViewModelConnector>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ViewModelConnector), typeof(ViewRightConnector), new PropertyMetadata(null));

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

        public ViewRightConnector()
        {
            InitializeComponent();
            SetupBinding();
            SetupEvents();
        }


        #region SetupBinding
        private void SetupBinding()
        {
            this.WhenActivated(disposable =>
            {
                // Имя перехода ( вводится в узле)
                this.OneWayBind(this.ViewModel, x => x.Name, x => x.Text.Text);

                // Доступно ли имя перехода для редактирования
                this.OneWayBind(this.ViewModel, x => x.TextEnable, x => x.Text.IsEnabled);

                // Доступен ли переход для создания соединия
                this.OneWayBind(this.ViewModel, x => x.FormEnable, x => x.Form.IsEnabled);

                // Цвет рамки, вокруг перехода
                this.OneWayBind(this.ViewModel, x => x.FormStroke, x => x.Form.Stroke);

                //Позиция X от левого верхнего угла
                this.Bind(this.ViewModel, x => x.Position.X, x => x.Translate.X);

                //Позиция Y от левого верхнего угла
                this.Bind(this.ViewModel, x => x.Position.Y, x => x.Translate.Y);

                // Цвет перехода
                this.OneWayBind(this.ViewModel, x => x.FormFill, x => x.Form.Fill);

                // Отображается ли переход
                this.OneWayBind(this.ViewModel, x => x.Visible, x => x.RightConnector.Visibility);

                // При изменении размера, позиции или zoom узла
                this.WhenAnyValue(x => x.ViewModel.Node.Size, x => x.ViewModel.Node.Point1.Value, x => x.ViewModel.Node.NodesCanvas.Scale.Scales.Value, x => x.ViewModel.Position.Value).Subscribe(_ => UpdatePosition());
            });
        }
        #endregion SetupBinding

        #region SetupEvents
        private void SetupEvents()
        {
            this.WhenActivated(disposable =>
            {
                this.Form.Events().MouseLeftButtonDown.Subscribe(e => OnEventDrag(e));
                this.Text.Events().PreviewMouseLeftButtonDown.Subscribe(e => OnEventTextPreviewMouseLeftButtonDown(e));
                this.Text.Events().LostFocus.Subscribe(e => Validate(e));
            });
        }
        private void Validate(RoutedEventArgs e)
        {
            ViewModel.CommandValidateName.Execute(Text.Text);
            if (Text.Text != ViewModel.Name)
                Text.Text = ViewModel.Name;
        }
        /// <summary>
        /// Событие перетаскивания соединения на круг
        /// </summary>
        /// <param name="e"></param>
        private void OnEventDrag(MouseButtonEventArgs e)
        {
            this.ViewModel.CommandConnectPointDrag.Execute();
            DataObject data = new DataObject();
            data.SetData("Node", this.ViewModel.Node);
            DragDrop.DoDragDrop(this, data, DragDropEffects.Link);
            this.ViewModel.CommandCheckConnectPointDrop.Execute();
            e.Handled = true;
        }
        private void OnEventTextPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.ViewModel.CommandConnectorDrag.Execute();
            DataObject data = new DataObject();
            data.SetData("Connector", this.ViewModel);
            DragDrop.DoDragDrop(this, data, DragDropEffects.Link);
            //this.ViewModel.CommandConnectorDrop.Execute();
            //e.Handled = true;
        }
        #endregion SetupEvents

        /// <summary>
        /// Обновить координату центра круга
        /// </summary>
        void UpdatePosition()
        {
            Point Position;
            //Если отображается
            if (this.IsVisible)
            {
                // Координата центра
                Point InputCenter = Form.TranslatePoint(new Point(Form.Width / 2, Form.Height / 2), this);

                //Ищем Canvas
                ViewNodesCanvas NodesCanvas = MyUtils.FindParent<ViewNodesCanvas>(this);

                //Получаем позицию центру на канвасе
                Position = this.TransformToAncestor(NodesCanvas).Transform(InputCenter);
            }
            else
            {
                //Позиция выхода
                Position = this.ViewModel.Node.Output.PositionConnectPoint.Value;
            }

            this.ViewModel.PositionConnectPoint.Set(Position);
        }

    }
}
