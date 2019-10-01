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

       public MyPoint positionRightClick { get; set ; } = new MyPoint();

        //public ObservableProperty<MyPoint> positionRightClick;
        public MyPoint positionLeftClick { get; set; } = new MyPoint();
        private MyPoint positionMove = new MyPoint();
        private MyPoint sumMove = new MyPoint();

        private MoveNodes move = MoveNodes.No;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Keyboard.Focus(this);
        }
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
           
            //positionRightClick.ObservableForProperty
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
                this.OneWayBind(this.ViewModel, x => x.CommandRedo, x => x.BindingRedo.Command);
                this.OneWayBind(this.ViewModel, x => x.CommandUndo, x => x.BindingUndo.Command);
                this.OneWayBind(this.ViewModel, x => x.CommandUndo, x => x.BindingUndo.Command);
                var positionLeftClickParam = this.ObservableForProperty(x => x.positionLeftClick).Select(x => x.Value);
                this.BindCommand(this.ViewModel, x => x.CommandSelect, x => x.BindingSelect, positionLeftClickParam);
                this.BindCommand(this.ViewModel, x => x.CommandAddNode, x => x.BindingAddNode, positionLeftClickParam);
                this.WhenAnyValue(x => x.ViewModel.Selector.Size).InvokeCommand(ViewModel.CommandSelectorIntersect);

            });
        }
        #endregion Setup Commands

        #region Setup Events
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

                //Эти события срабатывают раньше команд
                this.Events().PreviewMouseLeftButtonDown.Subscribe(e => OnMouseLeftDown(e));
            });
        }
        private void OnMouseLeftDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                this.CaptureMouse();
                Keyboard.Focus(this);
            }
            positionLeftClick.FromPoint(e.GetPosition(this.Grid));
            //if (this.IsMouseCaptured)
            //    ViewModelNodesCanvas.CommandUnSelectAll.Execute(null);
        }
            private void OnMouseLeftUp(MouseButtonEventArgs e)
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
        private void OnMouseRightDown(MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
            //positionRightClick.Value
            positionRightClick.FromPoint(e.GetPosition(this.Grid));
        }
        private void OnMouseRightUp(MouseButtonEventArgs e)
        {
        }
        private void OnMouseDown(MouseButtonEventArgs e)
        {
        }           
        private void OnMouseUp(MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            positionMove = null;
            Keyboard.Focus(this);
       
        }
        private void OnMouseMove(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
                return;
            MyPoint delta = GetDeltaMove();

            if (delta.IsClear)
                return;

            sumMove += delta;    
            
            if (this.IsMouseCaptured)
            {
                //ViewModelNodesCanvas.CommandSimpleMoveAllNode.Execute(delta);
                move = MoveNodes.MoveAll;
            }
            else
            {
                //ViewModelNodesCanvas.CommandSimpleMoveAllSelectedNode.Execute(delta);
                move = MoveNodes.MoveSelected;
            }
        }

        private MyPoint GetDeltaMove()
        {
            MyPoint CurrentPosition = MyPoint.MyPointFromPoint(Mouse.GetPosition(this.Grid));
            MyPoint result = new MyPoint();

            if (positionMove.IsClear)
            {
                result = CurrentPosition - positionMove;
            }
            positionMove = CurrentPosition;
            return result;
        }
        #endregion Setup Events

    }
}
