using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace StateMachineNodeEditor
{
    
    public class ViewModelNodesCanvas: INotifyPropertyChanged
    {
        private ModelNodesCanvas nodesCanvas;
         public void News(object sender, ExecutedRoutedEventArgs e)
        {

        }
        public CommandBindingCollection CommandBindings { get; } = new CommandBindingCollection();
        public InputBindingCollection InputBindings { get; } = new InputBindingCollection();
        public List<MenuItem> Items { get; } = new List<MenuItem>();

        public void New(object sender)
        {

        }

        public ObservableCollection<ViewModelNode> Nodes { get; set; } = new ObservableCollection<ViewModelNode>();
        public ObservableCollection<ViewModelConnect> Connects { get; set; } = new ObservableCollection<ViewModelConnect>();
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

            AddCommand();
        }
        public void AddCommand()
        {
            MenuItem ItemFromCommand(Command command)
            {
                MenuItem menuItem = new MenuItem();
                //menuItem.Header = command.Text;
                menuItem.Name = command.Text;
                menuItem.Command = command;
                return menuItem;
            }
            Command newCommnad = new Command(ApplicationCommands.New, New);
            CommandBindings.Add(newCommnad.GetCommandBinding());
            Items.Add(ItemFromCommand(newCommnad));
        }
        public void NodesChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    ModelNode node = element as ModelNode;
                    if (node!=null)
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
