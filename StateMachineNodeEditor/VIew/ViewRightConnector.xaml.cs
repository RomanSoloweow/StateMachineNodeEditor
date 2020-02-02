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
    public partial class ViewRightConnector : UserControl, IViewFor<ViewModelConnector>, CanBeMove
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
                this.OneWayBind(this.ViewModel, x => x.Position.X, x => x.Translate.X);

                //Позиция Y от левого верхнего угла
                this.OneWayBind(this.ViewModel, x => x.Position.Y, x => x.Translate.Y);

                //Размеры
                this.WhenAnyValue(v => v.Grid.ActualWidth, v => v.Grid.ActualHeight, (width, height) => new Size(width, height))
                     .BindTo(this, v => v.ViewModel.Size);

                // Цвет перехода
                this.OneWayBind(this.ViewModel, x => x.FormFill, x => x.Form.Fill);

                // Отображается ли переход
                this.OneWayBind(this.ViewModel, x => x.Visible, x => x.RightConnector.Visibility);

                // При изменении размера, позиции или zoom узла
                this.WhenAnyValue( x => x.ViewModel.Node.Size, x => x.ViewModel.Node.Point1.Value, x => x.ViewModel.Node.NodesCanvas.Scale.Scales.Value, x =>x.ViewModel.Position).Subscribe(_ => { UpdatePositionConnectPoin(); });
                this.WhenAnyValue(x => x.ViewModel.Node.Size).Subscribe(_ => { UpdatePosition(); });
            });
        }
        #endregion SetupBinding

        #region SetupEvents
        private void SetupEvents()
        {
            this.WhenActivated(disposable =>
            {
                this.Form.Events().MouseLeftButtonDown.Subscribe(e => ConnectDrag(e));

                this.Text.Events().LostFocus.Subscribe(e => Validate(e));
                
                this.Text.Events().PreviewMouseLeftButtonDown.Subscribe(e => TextDrag(e));
                this.Text.Events().PreviewDrop.Subscribe(e => TextDrop(e));
                this.Text.Events().PreviewDragOver.Subscribe(e => TextDragOver(e));
                this.Text.Events().PreviewDragEnter.Subscribe(e => TextDragEnter(e));
                this.Text.Events().PreviewDragLeave.Subscribe(e => TextDragLeave(e));

                this.Grid.Events().PreviewMouseLeftButtonDown.Subscribe(e => ConnectorDrag(e));
                this.Grid.Events().PreviewDragEnter.Subscribe(e => ConnectorDragEnter(e));
                this.Grid.Events().PreviewDragOver.Subscribe(e => ConnectorDragOver(e));              
                this.Grid.Events().PreviewDragLeave.Subscribe(e => ConnectorDragLeave(e));
                this.Grid.Events().PreviewDrop.Subscribe(e => ConnectorDrop(e));
               
            });
        }
        private void Test()
        {
            Console.WriteLine("Test " + this.ViewModel.Name);
        }
        private void Validate(RoutedEventArgs e)
        {
            ViewModel.CommandValidateName.Execute(Text.Text);
            if (Text.Text != ViewModel.Name)
                Text.Text = ViewModel.Name;
        }

        private void ConnectDrag(MouseButtonEventArgs e)
        {
            this.ViewModel.CommandConnectPointDrag.Execute();
            DataObject data = new DataObject();
            data.SetData("Node", this.ViewModel.Node);
            DragDrop.DoDragDrop(this, data, DragDropEffects.Link);
            this.ViewModel.CommandCheckConnectPointDrop.Execute();
            e.Handled = true;
        }
        private void TextDrag(MouseButtonEventArgs e)
        {
            ConnectorDrag(e);
            e.Handled = true;
        }
        private void TextDragOver(DragEventArgs e)
        {
            ConnectorDragOver(e);
            e.Handled = true;
        }
        private void TextDragEnter(DragEventArgs e)
        {
            ConnectorDragEnter(e);
            e.Handled = true;
        }
        private void TextDragLeave(DragEventArgs e)
        {
            ConnectorDragLeave(e);
            e.Handled = true;
        }
        private void TextDrop(DragEventArgs e)
        {
            ConnectorDrop(e);
            e.Handled = true;
        }

        private void ConnectorDrag(MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(this.ViewModel.Name))
                return;
          
            Console.WriteLine("RightConnector ConnectorDrag");

            this.UpdatePosition();

            if (this.ViewModel.NodesCanvas.DraggedConnector==null)
            {        
                this.ViewModel.NodesCanvas.ConnectorPreviewForDrop = this.ViewModel;
            }

           

            //this.ViewModel.CommandConnectorDrag.Execute();
            DataObject data = new DataObject();
            data.SetData("Connector", this.ViewModel);
            DragDrop.DoDragDrop(this, data, DragDropEffects.Link);


            //this.ViewModel.NodesCanvas.ConnectorPreviewForDrop = null;
            //this.ViewModel.CommandConnectorDrop.Execute();
            e.Handled = true;
        }
        private void ConnectorDragOver(DragEventArgs e)
        {
            if (this.ViewModel.NodesCanvas.ConnectorPreviewForDrop == null)
                return;

            if (string.IsNullOrEmpty(this.ViewModel.Name))
                return;

            if (this.ViewModel.NodesCanvas.DraggedConnector == this.ViewModel)
                return;

            if((this.ViewModel.NodesCanvas.ConnectorPreviewForDrop == this.ViewModel)&&(this.ViewModel.Node!=null))
            {
                this.ViewModel.Node.Transitions.Remove(this.ViewModel.NodesCanvas.ConnectorPreviewForDrop);
                //this.ViewModel.Node = null;
                this.ViewModel.NodesCanvas.DraggedConnector = this.ViewModel.NodesCanvas.ConnectorPreviewForDrop;
                this.ViewModel.NodesCanvas.ConnectorPreviewForDrop = null;
                //this.UpdatePosition();

                return;
            }

            e.Handled = true;
            Console.WriteLine("RightConnector ConnectorDragOver");
            return;

       




            this.ViewModel.Node.Transitions.Remove(this.ViewModel.NodesCanvas.ConnectorPreviewForDrop);
        

            int index = this.ViewModel.Node.Transitions.IndexOf(this.ViewModel);

            if ((this.ViewModel.NodesCanvas.ConnectorPreviewForDrop.PositionConnectPoint.Y - 6) > (this.ViewModel.PositionConnectPoint.Y - 6))
            {
                this.ViewModel.Node.Transitions.Insert(index + 1, this.ViewModel.NodesCanvas.ConnectorPreviewForDrop);
            }
            else
            {
                this.ViewModel.Node.Transitions.Insert(index, this.ViewModel.NodesCanvas.ConnectorPreviewForDrop);
            }
            this.ViewModel.NodesCanvas.ConnectorPreviewForDrop.Position.Clear();
            //this.ViewModel.NodesCanvas.DraggedConnector.Position.Clear();
            //this.ViewModel.NodesCanvas.DraggedConnector = null;

         

            //ViewNode viewNode = MyUtils.FindParent<ViewNode>(this);
            //if(viewNode!=null)
            //    viewNode.OnEventTransitionsDragOver(e);

           
        }
        private void ConnectorDragEnter(DragEventArgs e)
        {
            if (this.ViewModel.NodesCanvas.DraggedConnector == null)
                return;

            if (this.ViewModel.NodesCanvas.DraggedConnector == this.ViewModel)
                return;

            if (this.ViewModel.NodesCanvas.ConnectorPreviewForDrop == this.ViewModel)
                return;


            Console.WriteLine("RightConnector ConnectorDragEnter");

            int index = this.ViewModel.Node.Transitions.IndexOf(this.ViewModel);

            this.ViewModel.Node.Transitions.Insert(index + 1, this.ViewModel.NodesCanvas.DraggedConnector);

            //if (this.ViewModel.Node.Transitions.Count > 1)
            //{

            //    int index = this.ViewModel.Node.Transitions.IndexOf(this.ViewModel);

            //    if ((this.ViewModel.NodesCanvas.DraggedConnector.Position.Y) > (this.ViewModel.Position.Y))
            //    {
            //        this.ViewModel.Node.Transitions.Insert(index + 1, this.ViewModel.NodesCanvas.DraggedConnector);
            //    }
            //    else
            //    {
            //        this.ViewModel.Node.Transitions.Insert(index, this.ViewModel.NodesCanvas.DraggedConnector);
            //    }
            //}
            //else
            //{
            //    this.ViewModel.Node.Transitions.Insert(1, this.ViewModel.NodesCanvas.DraggedConnector);
            //}
            this.ViewModel.NodesCanvas.DraggedConnector.Node = this.ViewModel.Node;
            this.ViewModel.NodesCanvas.DraggedConnector.Position.Clear();
            this.ViewModel.NodesCanvas.ConnectorPreviewForDrop = this.ViewModel.NodesCanvas.DraggedConnector;
            this.ViewModel.NodesCanvas.DraggedConnector = null;




            e.Handled = true;
        }
        private void ConnectorDragLeave(DragEventArgs e)
        {
            if (this.ViewModel.NodesCanvas.ConnectorPreviewForDrop == null)
                return;
            ////if (this.ViewModel.NodesCanvas.DraggedConnector == null)
            ////    return;
            //if (string.IsNullOrEmpty(this.ViewModel.Name))
            //    return;

            //if (this.ViewModel.NodesCanvas.DraggedConnector == this.ViewModel)
            //    return;

            if (this.ViewModel.NodesCanvas.ConnectorPreviewForDrop == this.ViewModel)
                return;

            ////if ((this.ViewModel.NodesCanvas.ConnectorPreviewForDrop == this.ViewModel) && (this.ViewModel.Node != null))
            ////{
            this.ViewModel.Node.Transitions.Remove(this.ViewModel.NodesCanvas.ConnectorPreviewForDrop);
            //this.ViewModel.Node = null;
            this.ViewModel.NodesCanvas.DraggedConnector = this.ViewModel.NodesCanvas.ConnectorPreviewForDrop;
            this.ViewModel.NodesCanvas.ConnectorPreviewForDrop = null;
            //    //this.UpdatePosition();


            ////}

            e.Handled = true;
            Console.WriteLine("RightConnector ConnectorDragLeave");
            return;
        }
        private void ConnectorDrop(DragEventArgs e)
        {
            //if (!this.ViewModel.NodesCanvas.HasConnectorDrag())
            //    return;

            //ViewNode viewNode = MyUtils.FindParent<ViewNode>(this);
            //if (viewNode != null)
            //    viewNode.OnEventTransitionsDrop(e);
        }

        #endregion SetupEvents

        /// <summary>
        /// Обновить координату центра круга
        /// </summary>
        void UpdatePositionConnectPoin()
        {
            if (this.ViewModel.Node == null)
                return;
            //Console.WriteLine("UpdatePositionConnectPoin "+this.ViewModel.Name);
            Point positionConnectPoint;
            //Если отображается
            if (this.IsVisible)
            {
                // Координата центра
                positionConnectPoint = Form.TranslatePoint(new Point(Form.Width / 2, Form.Height / 2), this);

                //Ищем Canvas
                ViewNodesCanvas NodesCanvas = MyUtils.FindParent<ViewNodesCanvas>(this);

                //Получаем позицию центру на канвасе
                positionConnectPoint = this.TransformToAncestor(NodesCanvas).Transform(positionConnectPoint);

            }
            else
            {
                //Позиция выхода
                positionConnectPoint = this.ViewModel.Node.Output.PositionConnectPoint.Value;
            }

            this.ViewModel.PositionConnectPoint.Set(positionConnectPoint);
        }

        void UpdatePosition()
        {

            Point position = new Point();

            //Если отображается
            if (this.IsVisible)
            {
                //Ищем Canvas
                ViewNodesCanvas NodesCanvas = MyUtils.FindParent<ViewNodesCanvas>(this);

                position = this.TransformToAncestor(NodesCanvas).Transform(position);

                this.ViewModel.Position.Set(position);

            }
        }
    }
}
