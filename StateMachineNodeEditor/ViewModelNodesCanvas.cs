using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace StateMachineNodeEditor
{
    public class ViewModelNodesCanvas: INotifyPropertyChanged
    {
       public ModelNodesCanvas NodesCanvas { get; set; } 
        public ViewModelNodesCanvas()
        {
            NodesCanvas = new ModelNodesCanvas();
        }

        public ObservableCollection<ModelNode> Nodes
        {
            get { return NodesCanvas.Nodes; }
            set
            {
                NodesCanvas.Nodes = value;
                OnPropertyChanged("Nodes");
            }
        }
        public ObservableCollection<ModelConnect> Connects
        {
            get { return NodesCanvas.Connects; }
            set
            {
                NodesCanvas.Connects = value;
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
