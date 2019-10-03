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
    /// Interaction logic for ViewNodesCanvas.xaml
    /// </summary>
    public partial class ViewNodesCanvas : UserControl,IViewFor<ViewModelNodesCanvas>
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

        private MyPoint positionRightClick { get; set ; } = new MyPoint();
        private MyPoint positionLeftClick { get; set; } = new MyPoint();
        private MyPoint positionMove { get; set; } = new MyPoint();
        private MyPoint sumMove { get; set; } = new MyPoint();
        private MoveNodes move { get; set; } = MoveNodes.No;

        public ViewNodesCanvas()
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
                    this.OneWayBind(this.ViewModel, x => x.Nodes, x => x.Nodes.ItemsSource);
                    this.OneWayBind(this.ViewModel, x => x.Connects, x => x.Connects.ItemsSource);
                    this.OneWayBind(this.ViewModel, x => x.Selector, x => x.Selector.ViewModel);
                });
            }
        #endregion Setup Binding

        #region Setup Commands
        private void SetupCommands()
        {
            this.WhenActivated(disposable =>
            {
                var positionLeftClickObservable = this.ObservableForProperty(x => x.positionLeftClick).Select(x => x.Value);
                var positionRightClickObservable = this.ObservableForProperty(x => x.positionRightClick).Select(x => x.Value);
                this.OneWayBind(this.ViewModel, x => x.CommandRedo, x => x.BindingRedo.Command);
                this.OneWayBind(this.ViewModel, x => x.CommandUndo, x => x.BindingUndo.Command);
                this.OneWayBind(this.ViewModel, x => x.CommandUndo, x => x.BindingUndo.Command);

                this.BindCommand(this.ViewModel, x => x.CommandSelect, x => x.BindingSelect, positionLeftClickObservable);
                this.BindCommand(this.ViewModel, x => x.CommandAddNode, x => x.BindingAddNode, positionLeftClickObservable);
                this.BindCommand(this.ViewModel, x => x.CommandAddNode, x => x.ItemAddNode, positionRightClickObservable);
                this.WhenAnyValue(x => x.ViewModel.Selector.Size).InvokeCommand(ViewModel.CommandSelectorIntersect);

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
                    this.Events().DragOver.Subscribe(e => OnEventDragOver(e));

                    //Эти события срабатывают раньше команд
                    this.Events().PreviewMouseLeftButtonDown.Subscribe(e => OnEventPreviewMouseLeftButtonDown(e));
                    this.Events().PreviewMouseRightButtonDown.Subscribe(e => OnEventPreviewMouseRightButtonDown(e));
                });
            }
        private void OnEventMouseLeftDown(MouseButtonEventArgs e)
        {
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
        
        private void OnEventMouseLeftUp(MouseButtonEventArgs e)
        {
            if (move == MoveNodes.No)
                return;

            //if (move == MoveNodes.MoveAll)
                //ViewModelNodesCanvas.CommandMoveAllNode.Execute(sumMove);
            //else
                //ViewModelNodesCanvas.CommandMoveAllSelectedNode.Execute(sumMove);
            move = MoveNodes.No;
            sumMove.Clear();
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
        private void OnEventMouseUp(MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            positionMove.Clear();
            Keyboard.Focus(this);
       
        }
        private void OnEventMouseMove(MouseEventArgs e)
        {
            if (Mouse.Captured == null)
                return;
            MyPoint delta = GetDeltaMove();

            if (delta.IsClear)
                return;

            sumMove += delta;
            if (this.IsMouseCaptured)
            {
                ViewModel.CommandPartMoveAllNode.Execute(delta);
                move = MoveNodes.MoveAll;
            }
            else
            {
                ViewModel.CommandPartMoveAllSelectedNode.Execute(delta);
                move = MoveNodes.MoveSelected;
            }
        }
        private void OnEventDragOver(DragEventArgs e)
        {
            MyPoint point = new MyPoint(e.GetPosition(Grid));
            point -= 2;
            this.ViewModel.CurrentConnect.EndPoint.Set(point);
        }

        private void OnEventPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            positionLeftClick.Set(e.GetPosition(this.Grid));
        }
        private void OnEventPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        {
            positionRightClick.Set(e.GetPosition(this.Grid));
        }

        #endregion Setup Events
        private MyPoint GetDeltaMove()
        {
            MyPoint CurrentPosition = new MyPoint(Mouse.GetPosition(this.Grid));
            MyPoint result = new MyPoint();

            if (!positionMove.IsClear)
            {
                result = CurrentPosition - positionMove;
            }
            positionMove = CurrentPosition;
            return result;
        }

    }
}
