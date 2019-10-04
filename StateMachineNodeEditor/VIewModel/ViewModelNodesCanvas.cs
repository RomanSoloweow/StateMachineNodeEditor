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
        public IObservableCollection<ViewModelConnect> Connects = new ObservableCollectionExtended<ViewModelConnect>();
        public IObservableCollection<ViewModelNode> Nodes = new ObservableCollectionExtended<ViewModelNode>();

        [Reactive] public ViewModelSelector Selector { get; set; } = new ViewModelSelector();
        [Reactive] public ViewModelConnect CurrentConnect { get; set; }
        [Reactive] public ViewModelNode CurrentNode { get; set; }

        /// <summary>
        /// Масштаб 
        /// </summary>
        [Reactive] public Scale Scale { get; set; } = new Scale();

       

        public ViewModelNodesCanvas()
        {
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

        //public SimpleCommandWithParameter<ViewModelConnector> CommandAddFreeConnect { get; set; }
        public Command<ViewModelConnector, ViewModelConnect> CommandAddConnect { get; set; }
        public SimpleCommand CommandDeleteFreeConnect { get; set; }
        //public Command CommandDropOver { get; set; }

        private void SetupCommands()
        {
            CommandRedo = new SimpleCommand(this, CommandUndoRedo.Redo);
            CommandUndo = new SimpleCommand(this, CommandUndoRedo.Undo);
            CommandFullMoveAllNode = new Command<MyPoint, List<ViewModelNode>>(this, FullMoveAllNode, UnFullMoveAllNode);
            CommandPartMoveAllNode = new SimpleCommandWithParameter<MyPoint>(this, PartMoveAllNode);
            CommandFullMoveAllSelectedNode = new Command<MyPoint, List<ViewModelNode>>(this, FullMoveAllSelectedNode, UnFullMoveAllSelectedNode);
            CommandPartMoveAllSelectedNode = new SimpleCommandWithParameter<MyPoint>(this, PartMoveAllSelectedNode);
            CommandAddNode = new Command<MyPoint, ViewModelNode>(this, AddNode, DeleteNode);
            //CommandAddConnect = new Command<ViewModelConnect, ViewModelConnect>(this, AddConnect, DeleteConnect);
            //CommandDeleteNode = new Command<MyPoint, ViewModelNode>(this, DeleteNode,);
            CommandSelect = new SimpleCommandWithParameter<MyPoint>(this, StartSelect);
            CommandSelectorIntersect = new SimpleCommand(this, SelectorIntersect);

            CommandAddConnect = new Command<ViewModelConnector, ViewModelConnect>(this, AddConnect, DeleteConnect);
            CommandDeleteFreeConnect = new SimpleCommand(this, DeleteFreeConnect);

            CommandSelectAll = new SimpleCommand(this, SelectedAll);
            CommandUnSelectAll = new SimpleCommand(this, UnSelectedAll);
        }

        #endregion Setup Commands
        private void StartSelect(MyPoint point)
        {
            Selector.CommandStartSelect.Execute(point);
        }
        private void SelectedAll()
        {
            Nodes.ToList().ForEach(x => x.Selected = true);
        }
        private void UnSelectedAll()
        {
            Nodes.ToList().ForEach(x => x.Selected = false);
        }
        private List<ViewModelNode> FullMoveAllNode(MyPoint delta, List<ViewModelNode> nodes = null)
        {
            MyPoint myPoint = delta.Copy();
            if (nodes == null)
            {
                nodes = Nodes.ToList();
                myPoint.Clear();
            }
            nodes.ForEach(node => node.CommandMove.Execute(myPoint));
            return nodes;
        }
        private List<ViewModelNode> UnFullMoveAllNode(MyPoint delta, List<ViewModelNode> nodes = null)
        {
            MyPoint myPoint = delta.Copy();
            myPoint.Mirror();
            nodes.ForEach(node => node.CommandMove.Execute(myPoint));
            return nodes;
        }
        private List<ViewModelNode> FullMoveAllSelectedNode(MyPoint delta, List<ViewModelNode> nodes = null)
        {
            MyPoint myPoint = delta.Copy();
            if (nodes == null)
            {
                nodes = Nodes.Where(x => x.Selected).ToList();
                myPoint.Clear();
            }
            nodes.ForEach(node => node.CommandMove.Execute(myPoint));
            return nodes;
        }
        private List<ViewModelNode> UnFullMoveAllSelectedNode(MyPoint delta, List<ViewModelNode> nodes = null)
        {
            MyPoint myPoint = delta.Copy();
            myPoint.Mirror();
            nodes.ForEach(node => node.CommandMove.Execute(myPoint));
            return nodes;
        }
        private void PartMoveAllNode(MyPoint delta)
        {
            Nodes.ToList().ForEach(node => node.CommandMove.Execute(delta));
        }
        private void PartMoveAllSelectedNode(MyPoint delta)
        {
            Nodes.Where(x => x.Selected).ToList().ForEach(node => node.CommandMove.Execute(delta));
        }
        private ViewModelNode AddNode(MyPoint parameter, ViewModelNode result)
        {
            ViewModelNode newNode = result;
            if (result == null)
            {
                newNode = new ViewModelNode(this)
                {
                    Name = "State " + Nodes.Count.ToString(),
                    Point1 = new MyPoint(parameter)
                };
            }
            Nodes.Add(newNode);
            return newNode;
        }
        private ViewModelNode DeleteNode(MyPoint parameter, ViewModelNode result)
        {
            Nodes.Remove(result);
            return result;
        }

        private void SelectorIntersect()
        {
            MyPoint selectorPoint1 = Selector.Point1WithScale / Scale.Value;
            MyPoint selectorPoint2 = Selector.Point2WithScale / Scale.Value;
            foreach (ViewModelNode node in Nodes)
            {
                node.Selected = Utils.Intersect(node.Point1, node.Point2, selectorPoint1, selectorPoint2);
            }
        }
        private ViewModelConnect AddConnect(ViewModelConnector parameter, ViewModelConnect result)
        {
            CurrentConnect = result?? new ViewModelConnect(parameter);
            Connects.Add(CurrentConnect);
            return CurrentConnect;
        }
        private ViewModelConnect DeleteConnect(ViewModelConnector parameter, ViewModelConnect result)
        {
            bool t = Connects.Remove(result);
            return result;
        }
        private void DeleteFreeConnect()
        {
            Connects.Remove(CurrentConnect);
        }
    }
}
