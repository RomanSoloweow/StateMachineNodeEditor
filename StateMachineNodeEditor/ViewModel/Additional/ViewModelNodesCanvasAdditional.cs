
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace StateMachineNodeEditor
{
    public partial class ViewModelNodesCanvas
    {
        #region Commands
        public SimpleCommand CommandSelectAll { get; set; }
        public SimpleCommand CommandUnSelectAll { get; set; }
        public SimpleCommand CommandSelect { get; set; }
        public SimpleCommand CommandNew { get; set; }
        public SimpleCommand CommandRedo { get; set; }
        public SimpleCommand CommandUndo { get; set; }
        public SimpleCommand CommandCopy { get; set; }
        public SimpleCommand CommandPaste { get; set; }
        public SimpleCommand CommandDelete { get; set; }
        public SimpleCommand CommandCut { get; set; }
        public SimpleCommand CommandMoveDown { get; set; }
        public SimpleCommand CommandMoveLeft { get; set; }
        public SimpleCommand CommandMoveRight { get; set; }
        public SimpleCommand CommandMoveUp { get; set; }

        public SimpleCommand CommandMoveAllNode { get; set; }
        public SimpleCommand CommandMoveAllSelectedNode { get; set; }
        public SimpleCommand CommandDropOver { get; set; }

        public void AddCommands()
        {
            CommandSelectAll = new SimpleCommand(this, SelectAll);
            CommandUnSelectAll = new SimpleCommand(this, UnSelectAll);
            CommandSelect = new SimpleCommand(this, Select);
            CommandNew = new SimpleCommand(this, New, UnNew);
            CommandRedo = new SimpleCommand(this, Redo);
            CommandUndo = new SimpleCommand(this, Undo);
            CommandCopy = new SimpleCommand(this, SimpleCommand.Test);
            CommandPaste = new SimpleCommand(this, SimpleCommand.Test);
            CommandDelete = new SimpleCommand(this, Delete, UnDelete);
            CommandCut = new SimpleCommand(this, SimpleCommand.Test);

            CommandMoveDown = new SimpleCommand(this, SimpleCommand.Test);
            CommandMoveLeft = new SimpleCommand(this, SimpleCommand.Test);
            CommandMoveRight = new SimpleCommand(this, SimpleCommand.Test);
            CommandMoveUp = new SimpleCommand(this, SimpleCommand.Test);


            CommandMoveAllNode = new SimpleCommand(this, MoveAllNode, UnMoveAllNode, CombinePoint, EqualsList);
            CommandMoveAllSelectedNode = new SimpleCommand(this, MoveAllSelectedNode, UnMoveAllSelectedNode, CombinePoint, EqualsList);
            CommandDropOver = new SimpleCommand(this, DropOver);
            //MenuItem ItemFromCommand(Command command)
            //{
            //    MenuItem menuItem = new MenuItem();
            //    //menuItem.Header = command.Text;
            //    menuItem.Name = command.Text;
            //    menuItem.Command = command;
            //    return menuItem;
            //}
            //CommandBinding CommandBindingFromCommand(Command command)
            //{
            //    return new CommandBinding(command, command.Execute);
            //}

            //Command newCommnad = new Command(ApplicationCommands.New, New, UnNew);
            //InputBinding inputBinding = new InputBinding(newCommnad, newCommnad.InputGestures[0]);
            //InputBindings.Add(inputBinding);
            //CommandBindings.Add(CommandBindingFromCommand(newCommnad));
            //Items.Add(ItemFromCommand(newCommnad));
        }


        public object New(object parameters, object resultExecute)
        {
            if (resultExecute == null)
                return nodesCanvas.AddNewNode((Point)parameters);
            else
                return nodesCanvas.AddNode(resultExecute as ModelNode);
        }
        public object UnNew(object parameters, object resultExecute)
        {
            ModelNode modelNode = resultExecute as ModelNode;
            return nodesCanvas.DeleteNode(modelNode);
        }
        public object Delete(object parameters, object resultExecute)
        {
            return nodesCanvas.DeleteSelectedNodes(resultExecute as List<ModelNode>);
        }
        public object UnDelete(object parameters, object resultExecute)
        {
            return nodesCanvas.AddNodes(resultExecute as List<ModelNode>);
        }
        public object Redo(object parameters, object resultExecute)
        {
            if (SimpleCommand.Redo.Count > 0)
                SimpleCommand.Redo.Pop().Execute();
            return null;
        }
        public object Undo(object parameters, object resultExecute)
        {
            if (SimpleCommand.Undo.Count > 0)
                SimpleCommand.Undo.Pop().UnExecute();
            return null;
        }
        public object SelectAll(object parameters, object resultExecute)
        {
            return nodesCanvas.SelectedAllNodes();
        }
        public object UnSelectAll(object parameters, object resultExecute)
        {
            return nodesCanvas.UnSelectedAllNodes();
        }
        public object MoveAllNode(object parameters, object resultExecute)
        {
            return nodesCanvas.MoveAllNode((Point)parameters, resultExecute as List<ModelNode>);
        }
        public object CombinePoint(object parameters1, object parameters2)
        {
            Point point1 = (Point)parameters1;
            Point point2 = (Point)parameters2;

            return ForPoint.Addition(point1, point2);
        }
        public object UnMoveAllNode(object parameters, object resultExecute)
        {
            return nodesCanvas.MoveAllNode(ForPoint.Mirror((Point)parameters), resultExecute as List<ModelNode>);
        }
        public object MoveAllSelectedNode(object parameters, object resultExecute)
        {
            return nodesCanvas.MoveAllSelectedNode((Point)parameters, resultExecute as List<ModelNode>);
        }
        public bool EqualsList(object list1, object list2)
        {
            return (list1 as List<ModelNode>).SequenceEqual(list2 as List<ModelNode>);
        }
        public object UnMoveAllSelectedNode(object parameters, object resultExecute)
        {
            return nodesCanvas.MoveAllSelectedNode(ForPoint.Mirror((Point)parameters), resultExecute as List<ModelNode>);
        }
        public object Select(object parameters, object resultExecute)
        {
            //nodesCanvas.Selector.StartSelect((Point)parameters);
            return null;
        }
        public object DropOver(object parameters, object resultExecute)
        {
            return nodesCanvas.DropOver(parameters as DataObject);
        }
        #endregion Commands
    }
}
