using System;
using System.Linq;
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
using StateMachineNodeEditor.ViewModel;
using System.Reactive.Linq;

namespace StateMachineNodeEditor.View
{
    /// <summary>
    /// Interaction logic for ViewNodesCanvas.xaml
    /// </summary>
    public partial class ViewNodesCanvas : UserControl, IViewFor<ViewModelNodesCanvas>, CanBeMove
    {
        enum MoveNodes
        {
            No = 0,
            MoveAll,
            MoveSelected
        }

        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(ViewModelNodesCanvas), typeof(ViewNodesCanvas), new PropertyMetadata(null));

        public ViewModelNodesCanvas ViewModel
        {
            get { return (ViewModelNodesCanvas)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelNodesCanvas)value; }
        }
        #endregion ViewModel

        private MyPoint PositionDragOver { get; set; } = new MyPoint();
        private MyPoint PositionRightClick { get; set; } = new MyPoint();
        private MyPoint PositionLeftClick { get; set; } = new MyPoint();
        private MyPoint PositionMove { get; set; } = new MyPoint();

        private MyPoint SumMove { get; set; } = new MyPoint();
        private MoveNodes Move { get; set; } = MoveNodes.No;

        public ViewNodesCanvas()
        {
            InitializeComponent();
            ViewModel = new ViewModelNodesCanvas();
            SetupBinding();
            SetupEvents();
            BindingCommands();
        }
        #region Setup Binding
        private void SetupBinding()
        {
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(this.ViewModel, x => x.Nodes, x => x.Nodes.ItemsSource);
                this.OneWayBind(this.ViewModel, x => x.Connects, x => x.Connects.ItemsSource);
                this.OneWayBind(this.ViewModel, x => x.DraggedConnector, x => x.Connector.ViewModel);

                //Масштаб по оси X
                this.OneWayBind(this.ViewModel, x => x.Scale.Scales.Value.X, x => x.Scale.ScaleX);

                //Масштаб по оси Y
                this.OneWayBind(this.ViewModel, x => x.Scale.Scales.Value.Y, x => x.Scale.ScaleY);

                this.OneWayBind(this.ViewModel, x => x.Selector, x => x.Selector.ViewModel);

                this.OneWayBind(this.ViewModel, x => x.Cutter, x => x.Cutter.ViewModel);
            });
        }
        #endregion Setup Binding

        #region Setup Commands
        private void BindingCommands()
        {
            this.WhenActivated(disposable =>
            {
                var positionLeftClickObservable = this.ObservableForProperty(x => x.PositionLeftClick).Select(x => x.Value);
                var positionRightClickObservable = this.ObservableForProperty(x => x.PositionRightClick).Select(x => x.Value);

                this.BindCommand(this.ViewModel, x => x.CommandRedo, x => x.BindingRedo);
                this.BindCommand(this.ViewModel, x => x.CommandUndo, x => x.BindingUndo);
                this.BindCommand(this.ViewModel, x => x.CommandSelectAll, x => x.BindingSelectAll);
                this.BindCommand(this.ViewModel, x => x.CommandDeleteSelectedNodes, x => x.BindingDeleteNode);

                this.BindCommand(this.ViewModel, x => x.CommandSelect, x => x.BindingSelect, positionLeftClickObservable);
                this.BindCommand(this.ViewModel, x => x.CommandCut, x => x.BindingCut, positionLeftClickObservable);


                this.BindCommand(this.ViewModel, x => x.CommandAddNode, x => x.BindingAddNode, positionLeftClickObservable);
                this.BindCommand(this.ViewModel, x => x.CommandAddNode, x => x.ItemAddNode, positionRightClickObservable);
                this.WhenAnyValue(x => x.ViewModel.Selector.Size).InvokeCommand(ViewModel.CommandSelectorIntersect);
                this.WhenAnyValue(x => x.ViewModel.Cutter.EndPoint.Value).InvokeCommand(ViewModel.CommandCutterIntersect);
                this.WhenAnyValue(x => x.ViewModel.DraggedConnector).Subscribe(_ => UpdateConnector());

            });
        }
        #endregion Setup Commands

        #region Setup Events
        private void SetupEvents()
        {
            this.WhenActivated(disposable =>
            {
                this.Events().MouseLeftButtonDown.Subscribe(e => OnEventMouseLeftDown(e));
                this.Events().MouseLeftButtonUp.Subscribe(e => OnEventMouseLeftUp(e));
                this.Events().MouseRightButtonDown.Subscribe(e => OnEventMouseRightDown(e));
                this.Events().MouseRightButtonUp.Subscribe(e => OnEventMouseRightUp(e));
                this.Events().MouseDown.Subscribe(e => OnEventMouseDown(e));
                this.Events().MouseUp.Subscribe(e => OnEventMouseUp(e));
                this.Events().MouseMove.Subscribe(e => OnEventMouseMove(e));
                this.Events().MouseWheel.Subscribe(e => OnEventMouseWheel(e));
                this.Events().DragOver.Subscribe(e => OnEventDragOver(e));
                this.Events().DragEnter.Subscribe(e => OnEventDragEnter(e));

                //Эти события срабатывают раньше команд
                this.Events().PreviewMouseLeftButtonDown.Subscribe(e => OnEventPreviewMouseLeftButtonDown(e));
                this.Events().PreviewMouseRightButtonDown.Subscribe(e => OnEventPreviewMouseRightButtonDown(e));

                this.WhenAnyValue(x => x.ViewModel.Scale.Value).Subscribe(value => { this.Grid.Height /= value; this.Grid.Width /= value; });
            });
        }
        private void OnEventMouseLeftDown(MouseButtonEventArgs e)
        {
            PositionMove = new MyPoint(Mouse.GetPosition(this.Grid));

            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                this.CaptureMouse();
                Keyboard.Focus(this);

                this.ViewModel.CommandUnSelectAll.Execute();
            }

            //if (this.IsMouseCaptured)
            //    ViewModelNodesCanvas.CommandUnSelectAll.Execute(null);
        }
        private void UpdateConnector()
        {
            this.Connector.Visibility = (this.ViewModel.DraggedConnector == null) ? Visibility.Collapsed : Visibility.Visible;
        }
        private void OnEventMouseLeftUp(MouseButtonEventArgs e)
        {
            if (Move == MoveNodes.No)
                return;

            if (Move == MoveNodes.MoveAll)
                this.ViewModel.CommandFullMoveAllNode.Execute(SumMove);
            else if (Move == MoveNodes.MoveSelected)
                this.ViewModel.CommandFullMoveAllSelectedNode.Execute(SumMove);

            Move = MoveNodes.No;
            SumMove = new MyPoint();
        }
        private void OnEventMouseRightDown(MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
        }
        private void OnEventMouseRightUp(MouseButtonEventArgs e)
        {
        }
        private void OnEventMouseDown(MouseButtonEventArgs e)
        {
        }
        private void OnEventMouseWheel(MouseWheelEventArgs e)
        {
            this.ViewModel.CommandZoom.Execute(e.Delta);
        }
        private void OnEventMouseUp(MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            PositionMove.Clear();
            Keyboard.Focus(this);
        }
        private void OnEventMouseMove(MouseEventArgs e)
        {
            //if ((Mouse.Captured == null)||(!(Mouse.Captured is CanBeMove)))
            if (!(Mouse.Captured is CanBeMove))
                return;
           

            MyPoint delta = GetDeltaMove();

            if (delta.IsClear)
                return;

            SumMove += delta;
            if (this.IsMouseCaptured)
            {
                ViewModel.CommandPartMoveAllNode.Execute(delta);
                Move = MoveNodes.MoveAll;
            }
            else
            {
                ViewModel.CommandPartMoveAllSelectedNode.Execute(delta);
                Move = MoveNodes.MoveSelected;
            }
        }
        private void OnEventDragEnter(DragEventArgs e)
        {

        }
        private void OnEventDragOver(DragEventArgs e)
        {
            MyPoint point = new MyPoint(e.GetPosition(this));
            if (this.ViewModel.DraggedConnect != null)
            {
                point -= 2;
                this.ViewModel.DraggedConnect.EndPoint.Set(point);
            }
            else if (this.ViewModel.DraggedConnector != null)
            {
                this.ViewModel.DraggedConnector.Position = point;
            }
        }

        private void OnEventPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            PositionLeftClick.Set(e.GetPosition(this.Grid));
        }
        private void OnEventPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        {
            PositionRightClick.Set(e.GetPosition(this.Grid));
        }

        #endregion Setup Events
        private MyPoint GetDeltaMove()
        {
            MyPoint CurrentPosition = new MyPoint(Mouse.GetPosition(this.Grid));
            MyPoint result = new MyPoint();

            if (!PositionMove.IsClear)
            {
                result = CurrentPosition - PositionMove;
            }

            PositionMove = CurrentPosition;
            return result;
        }
        private MyPoint GetDeltaDragOver(DragEventArgs e)
        {
            MyPoint CurrentPosition = new MyPoint(e.GetPosition(this));

            MyPoint result = new MyPoint();

            if (!PositionDragOver.IsClear)
            {
                result = CurrentPosition - PositionDragOver;
            }
            PositionDragOver = CurrentPosition;

            return result;
        }
    }
}
