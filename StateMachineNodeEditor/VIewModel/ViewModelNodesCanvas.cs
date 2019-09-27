using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI.Fody.Helpers;
using StateMachineNodeEditor.Helpers;
using ReactiveUI;
using ReactiveUI.Wpf;
using DynamicData;
using DynamicData.Binding;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows;

namespace StateMachineNodeEditor.ViewModel
{
    public class ViewModelNodesCanvas : ReactiveObject
    {
        //public IObservableCollection<ViewModelNode> Nodes { get; set; } = new ObservableCollectionExtended<ViewModelNode>();
        //public IObservableCollection<ViewModelConnect> Connects { get; set; } = new ObservableCollectionExtended<ViewModelConnect>();

        private SourceList<ViewModelConnect> ListConnects { get; set; } = new SourceList<ViewModelConnect>();
        public IObservableCollection<ViewModelConnect> Connects = new ObservableCollectionExtended<ViewModelConnect>();


        private SourceList<ViewModelNode> ListNodes = new SourceList<ViewModelNode>();

        public IObservableCollection<ViewModelNode> Nodes = new ObservableCollectionExtended<ViewModelNode>();
        public IObservableList<ViewModelNode> NodesSelected { get; }
        public Point deltda = new Point();
        public ViewModelSelector Selector { get; set; } = new ViewModelSelector();
        public ViewModelConnect CurrentConnect { get; set; }
        private ViewModelConnect AddEmptyConnect()
        {
            CurrentConnect = new ViewModelConnect();
            Connects.Add(CurrentConnect);
            return CurrentConnect;
        }
        public ViewModelNodesCanvas()
        {
            ListNodes.Connect().ObserveOnDispatcher().Bind(Nodes).Subscribe();
            NodesSelected = ListNodes.Connect().AutoRefresh(node => node.Selected).Filter(node => node.Selected).AsObservableList();
            ListConnects.Connect().ObserveOnDispatcher().Bind(Connects).Subscribe();

            ListNodes.Add(new ViewModelNode(this));

            AddEmptyConnect();
            SetupCommands();
         
        }      
        #region Commands
        public SimpleCommand<object> CommandRedo { get; set; }
        public SimpleCommand<object> CommandUndo { get; set; }
        //public Command CommandSelectAll { get; set; }
        //public Command CommandUnSelectAll { get; set; }
        //public Command CommandSelect { get; set; }
        //public Command CommandNew { get; set; }
        //public Command CommandDelete { get; set; }
        //public Command CommandCopy { get; set; }
        //public Command CommandPaste { get; set; }
        //public Command CommandCut { get; set; }
        //public Command CommandMoveDown { get; set; }
        //public Command CommandMoveLeft { get; set; }
        //public Command CommandMoveRight { get; set; }
        //public Command CommandMoveUp { get; set; }
        //public Command CommandSimpleMoveAllNode { get; set; }
        //public Command CommandSimpleMoveAllSelectedNode { get; set; }
        public Command<MyPoint, List<ViewModelNode>> CommandMoveAllNode { get; set; }
        public Command<MyPoint, List<ViewModelNode>> CommandMoveAllSelectedNode { get; set; }
        //public Command CommandDropOver { get; set; }

        public void SetupCommands()
        {
            CommandRedo = new SimpleCommand<object>(this, Command<object, object>.Redo);
            CommandUndo = new SimpleCommand<object>(this, Command<object, object>.Undo);
            CommandMoveAllNode = new Command<MyPoint, List<ViewModelNode>>(this, MoveAllNode);
            CommandMoveAllNode = new Command<MyPoint, List<ViewModelNode>>(this, MoveAllSelectedNode);
        }

        #endregion Commands

        public List<ViewModelNode> MoveAllNode(MyPoint delta, List<ViewModelNode> nodes = null)
        {
            if (nodes == null)
                nodes = Nodes.ToList();
            nodes.ForEach(node => node.Move(delta));
            return nodes;
        }
        public List<ViewModelNode> MoveAllSelectedNode(MyPoint delta, List<ViewModelNode> nodes = null)
        {
            if (nodes == null)
                nodes = NodesSelected.Items.ToList();
            nodes.ForEach(node => node.Move(delta));
            return nodes;
        }

    }
}
