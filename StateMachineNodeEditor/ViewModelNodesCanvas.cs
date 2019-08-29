using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace StateMachineNodeEditor
{
    public class ViewModelNodesCanvas: INotifyPropertyChanged
    {
        private ModelNodesCanvas nodesCanvas;
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
