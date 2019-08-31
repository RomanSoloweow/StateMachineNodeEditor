using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
namespace StateMachineNodeEditor
{
    
    public class ViewModelNodesCanvas: INotifyPropertyChanged
    {
        private ModelNodesCanvas nodesCanvas;
        public CommandBindingCollection CommandBindings { get; } = new CommandBindingCollection();
        public InputBindingCollection InputBindings { get; } = new InputBindingCollection();
        public List<MenuItem> Items { get; } = new List<MenuItem>();

        public ObservableCollection<ViewModelNode> Nodes { get; set; } = new ObservableCollection<ViewModelNode>();
        public void NodesChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    ModelNode node = element as ModelNode;
                    if (node != null)
                        Nodes.Add(new ViewModelNode(node));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var element in e.OldItems)
                {
                    ModelNode node = element as ModelNode;
                    if (node != null)
                        Nodes.Remove(new ViewModelNode(node));
                }
            }
            OnPropertyChanged("Nodes");
        }
        public ObservableCollection<ViewModelConnect> Connects { get; set; } = new ObservableCollection<ViewModelConnect>();
        public void ConnectsChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    ModelConnect connect = element as ModelConnect;
                    if (connect != null)
                        Connects.Add(new ViewModelConnect(connect));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var element in e.OldItems)
                {
                    ModelConnect connect = element as ModelConnect;
                    if (connect != null)
                        Connects.Remove(new ViewModelConnect(connect));
                }
            }
            OnPropertyChanged("Connects");
        }
        public ViewModelNodesCanvas(ModelNodesCanvas modelNodesCanvas)
        {
            nodesCanvas  = modelNodesCanvas;
            foreach(ModelNode modelNode in nodesCanvas.Nodes)
            {
                Nodes.Add(new ViewModelNode(modelNode));
            }
            foreach (ModelConnect modelConnect in nodesCanvas.Connects)
            {
                Connects.Add(new ViewModelConnect(modelConnect));
            }
            nodesCanvas.Nodes.CollectionChanged += NodesChange;
            nodesCanvas.Connects.CollectionChanged += ConnectsChange;
            nodesCanvas.PropertyChanged += ModelPropertyChange;
            AddCommand();
        }

        #region Commands
        public SimpleCommand CommandSelectAll { get; set; }
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

        public object Test(object parameters)
        {
            return null;
        }
        public void AddCommand()
        {
            CommandSelectAll    = new SimpleCommand(this, Test);
            CommandSelect       = new SimpleCommand(this, Test);
            CommandNew          = new SimpleCommand(this, New, UnNew);
            CommandRedo         = new SimpleCommand(this, Test);
            CommandUndo         = new SimpleCommand(this, Test);
            CommandCopy         = new SimpleCommand(this, Test);
            CommandPaste        = new SimpleCommand(this, Test);
            CommandDelete       = new SimpleCommand(this, Test);
            CommandCut          = new SimpleCommand(this, Test);

            CommandMoveDown     = new SimpleCommand(this, Test);
            CommandMoveLeft     = new SimpleCommand(this, Test);
            CommandMoveRight    = new SimpleCommand(this, Test);
            CommandMoveUp       = new SimpleCommand(this, Test);

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


        public ModelNode New(object parameters)
        {
            return nodesCanvas.GetNewNode((Point)parameters);
        }
        public ModelNode UnNew(object parameters)
        {
            ModelNode modelNode = parameters as ModelNode;
            return nodesCanvas.DeleteNode(modelNode);
        }
        #endregion Commands

        public event PropertyChangedEventHandler PropertyChanged;
        public void ModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName));
        }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
