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
        private ModelSelector _selector = new ModelSelector();
        public ModelNodesCanvas()
        {
            //ModelNode node = new ModelNode();
            //node.Text = "TestNode";
            //node.Translate.X = 100;
            //node.Translate.Y = 100;
            //_nodes.Add(node);

            //ModelNode node2 = new ModelNode();
            //node2.Text = "TestNode2";
            //node2.Translate.X = 10;
            //node2.Translate.Y = 10;
            //_nodes.Add(node2);
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
        public ModelSelector Selector
        {
            get { return _selector; }
            set
            {
                _selector = value;
                OnPropertyChanged("Selector");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ModelConnect GetNewConnect(Point position)
        {
            ModelConnect modelConnect = new ModelConnect(position);
            _connects.Add(modelConnect);
            return modelConnect;
        }
        public ModelConnect DeleteConnect(ModelConnect modelConnect)
        {
            if (modelConnect != null)
            {
                _connects.Remove(modelConnect);
            }
            return modelConnect;
        }
        public void UnSelectedAllNodes()
        {
            foreach (ModelNode node in _nodes)
            {
                node.Selected = false;
            }
        }
        public void SelectedAllNodes()
        {
            foreach (ModelNode node in _nodes)
            {
                node.Selected = true;
            }
        }
        public ModelNode GetNewNode(Point position)
        {
            string text = "State " + this._nodes.Count.ToString();
            ModelNode modelNode = new ModelNode(this,text, position);
            //if (nodes.Count > 0)
            //{
            //    Node firstNode = nodes.First();
            //}
            //ForPoint.Equality(node.Manager.translate, ForPoint.Division(position, node.Manager.zoom));
            _nodes.Add(modelNode);
            //Panel.SetZIndex(node, nodes.Count());
            return modelNode;
        }
        public ModelNode AddNode(ModelNode modelNode)
        {
            string text = "State" + this._nodes.Count.ToString();
            modelNode.Text = text;
            _nodes.Add(modelNode);
            return modelNode;
        }
        public ModelNode DeleteNode(ModelNode node)
        {
            if (node != null)
            {
                _nodes.Remove(node);
            }
            return node;
        }
    }
}
