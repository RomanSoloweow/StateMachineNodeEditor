using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;


namespace StateMachineNodeEditor
{
    public class ModelNodesCanvas: INotifyPropertyChanged
    {
        private ObservableCollection<ModelNode> _nodes = new ObservableCollection<ModelNode>();
        private ObservableCollection<ModelConnect> _connects = new ObservableCollection<ModelConnect>();

        public ModelNodesCanvas()
        {
            ModelNode node = new ModelNode();
            node.Text = "TestNode";
            node.Translate.X = 100;
            node.Translate.Y = 100;
            _nodes.Add(node);

            ModelNode node2 = new ModelNode();
            node2.Text = "TestNode2";
            node2.Translate.X = 10;
            node2.Translate.Y = 10;
            _nodes.Add(node2);
        }
        public ObservableCollection<ModelNode> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                OnPropertyChanged("Nodes");
            }
        }
        public ObservableCollection<ModelConnect> Connects
        {
            get { return _connects; }
            set
            {
                _connects = value;
                OnPropertyChanged("Connects");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
