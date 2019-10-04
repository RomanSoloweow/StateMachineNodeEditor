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
                this.Bind(this.ViewModel, x => x.Visible, x => x.RightConnector.Visibility);

                // При изменении размера, позиции или zoom узла
                this.WhenAnyValue(x => x.ViewModel.Node.Size, x => x.ViewModel.Node.Point1.Value, x => x.ViewModel.Node.NodesCanvas.Scale.Scales.Value).Subscribe(_ => UpdatePosition());
            });
        }
        #endregion SetupBinding

        #region SetupEvents
        private void SetupEvents()
        {
            this.WhenActivated(disposable =>
            {
                this.Form.Events().MouseLeftButtonDown.Subscribe(e => OnEventDrag(e));
            });
        }
        #endregion SetupEvents

        private void OnEventDrag(MouseButtonEventArgs e)
        {
            this.ViewModel.CommandDrag.Execute();
            DataObject data = new DataObject();
            data.SetData("Node", this.ViewModel.Node);        
            DragDropEffects result = DragDrop.DoDragDrop(this, data, DragDropEffects.Link);
            this.ViewModel.CommandCheckDrop.Execute();
            e.Handled = true;
        }
        void UpdatePosition()
        {
            Point Position;
            var name = this.Name;
            var t = this.Visibility;
            //Если отображается
            if (this.IsVisible)
            {
                // Координата центра
                Point InputCenter = Form.TranslatePoint(new Point(Form.Width / 2, Form.Height / 2), this);

                //Ищем Canvas
                ViewNodesCanvas NodesCanvas = Utils.FindParent<ViewNodesCanvas>(this);

                //Получаем позицию центру на канвасе
                Position = this.TransformToAncestor(NodesCanvas).Transform(InputCenter);
            }
            else
            {
                //Позиция выхода
                Position = this.ViewModel.Node.Output.Position.Value;
            }

            this.ViewModel.Position.Set(Position);
        }

    }
}
