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
        private SourceList<ViewModelConnect> ListConnects { get; set; } = new SourceList<ViewModelConnect>();
        public IObservableCollection<ViewModelConnect> Connects = new ObservableCollectionExtended<ViewModelConnect>();

        private readonly SourceList<ViewModelNode> ListNodes = new SourceList<ViewModelNode>();
        public IObservableCollection<ViewModelNode> Nodes = new ObservableCollectionExtended<ViewModelNode>();
        
        public IObservableList<ViewModelNode> NodesSelected { get; }
        [Reactive] public ViewModelSelector Selector { get; set; } = new ViewModelSelector();
        [Reactive] public ViewModelConnect CurrentConnect { get; set; }
        [Reactive] public ViewModelNode CurrentNode { get; set; }

        /// <summary>
        /// Масштаб 
        /// </summary>
        [Reactive] public Scale Scale { get; set; } = new Scale();

       

        public ViewModelNodesCanvas()
        {

            ListNodes.Connect().ObserveOnDispatcher().Bind(Nodes).Subscribe();
            NodesSelected = ListNodes.Connect().AutoRefresh(node => node.Selected).Filter(node => node.Selected).AsObservableList();
            ListConnects.Connect().ObserveOnDispatcher().Bind(Connects).Subscribe();
            this.WhenAnyValue(x => x.CurrentConnect).Where(x => x == null).Subscribe(x => AddEmptyConnect());

            //ListNodes.Add(new ViewModelNode(this)
            //{
            //    Name= "State 0"
            //});

            //AddEmptyConnect();
            AddEmptyConnect();
            SetupCommands();
         
        }
        #region Setup Commands
        public SimpleCommand CommandRedo { get; set; }
        public SimpleCommand CommandUndo { get; set; }
        public SimpleCommand CommandSelectAll { get; set; }
        public SimpleCommand CommandUnSelectAll { get; set; }
        public SimpleCommandWithParameter<MyPoint> CommandSelect { get; set; }
        public Command<MyPoint, ViewModelNode> CommandAddNode { get; set; }
        public Command<MyPoint, ViewModelNode> CommandDeleteNode { get; set; }

        //public Command CommandCopy { get; set; }
        //public Command CommandPaste { get; set; }
        //public Command CommandCut { get; set; }

        //public Command CommandMoveDown { get; set; }
        //public Command CommandMoveLeft { get; set; }
        //public Command CommandMoveRight { get; set; }
        //public Command CommandMoveUp { get; set; }


        public SimpleCommand CommandSelectorIntersect { get; set; }
        public Command<MyPoint, List<ViewModelNode>> CommandFullMoveAllNode { get; set; }
        public Command<MyPoint, List<ViewModelNode>> CommandFullMoveAllSelectedNode { get; set; }
        public SimpleCommandWithParameter<MyPoint> CommandPartMoveAllNode { get; set; }
        public SimpleCommandWithParameter<MyPoint> CommandPartMoveAllSelectedNode { get; set; }
        //public Command CommandDropOver { get; set; }

        private void SetupCommands()
        {
            CommandRedo = new SimpleCommand(this, CommandUndoRedo.Redo);
            CommandUndo = new SimpleCommand(this, CommandUndoRedo.Undo);
            CommandFullMoveAllNode = new Command<MyPoint, List<ViewModelNode>>(this, FullMoveAllNode);
            CommandPartMoveAllNode = new SimpleCommandWithParameter<MyPoint>(this, PartMoveAllNode);
            CommandFullMoveAllSelectedNode = new Command<MyPoint, List<ViewModelNode>>(this, FullMoveAllSelectedNode);
            CommandPartMoveAllSelectedNode = new SimpleCommandWithParameter<MyPoint>(this, PartMoveAllSelectedNode);
            CommandAddNode = new Command<MyPoint, ViewModelNode>(this, AddNode);
            CommandDeleteNode = new Command<MyPoint, ViewModelNode>(this, DeleteNode);
            CommandSelect = new SimpleCommandWithParameter<MyPoint>(this, StartSelect);
            CommandSelectorIntersect = new SimpleCommand(this, SelectorIntersect);
        }

        #endregion Setup Commands
        private void StartSelect(MyPoint point)
        {
            Selector.CommandStartSelect.Execute(point);
        }
        private List<ViewModelNode> FullMoveAllNode(MyPoint delta, List<ViewModelNode> nodes = null)
        {
            if (nodes == null)
                nodes = Nodes.ToList();
            nodes.ForEach(node => node.CommandMove.Execute(delta));
            return nodes;
        }
        private List<ViewModelNode> FullMoveAllSelectedNode(MyPoint delta, List<ViewModelNode> nodes = null)
        {
            if (nodes == null)
                nodes = NodesSelected.Items.ToList();
            nodes.ForEach(node => node.CommandMove.Execute(delta));
            return nodes;
        }
        private void PartMoveAllNode(MyPoint delta)
        {
            Nodes.ToList().ForEach(node => node.CommandMove.Execute(delta));
        }
        private void PartMoveAllSelectedNode(MyPoint delta)
        {
            NodesSelected.Items.ToList().ForEach(node => node.CommandMove.Execute(delta));
        }
        private ViewModelNode AddNode(MyPoint paramentr, ViewModelNode result)
        {
            ViewModelNode newNode = new ViewModelNode(this)
            {
                Name = "State "+ Nodes.Count.ToString(),
                Point1 = new MyPoint(paramentr)
            };

            ListNodes.Add(newNode);
            return newNode;
        }
        private ViewModelNode DeleteNode(MyPoint paramentr, ViewModelNode result)
        {
            return null;
        }
        private void SelectorIntersect()
        {
            MyPoint selectorPoint1 = Selector.Point1 / Scale.Value;
            MyPoint selectorPoint2 = Selector.Point2 / Scale.Value;

            foreach (ViewModelNode node in Nodes)
            {
                node.Selected = Utils.Intersect(node.Point1, node.Point2, selectorPoint1, selectorPoint2);
            }
        }
        private ViewModelConnect AddEmptyConnect()
        {
            CurrentConnect = new ViewModelConnect();
            Connects.Add(CurrentConnect);
            return CurrentConnect;
        }
    }
}
