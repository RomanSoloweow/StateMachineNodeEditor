using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

using System.Collections.Specialized;
using System;

using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;

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
        public List<ModelNode> AddNodes(List<ModelNode> nodes)
        {
            foreach (ModelNode node in nodes)
            {
                AddNode(node);
            }
            return nodes;
        }
        public List<ModelNode> DeleteSelectedNodes(List<ModelNode> selectedNodes = null)
        {
            if (selectedNodes == null)
                selectedNodes = GetSelectedNodes();
            foreach (ModelNode selectedNode in selectedNodes)
            {
                DeleteNode(selectedNode);
            }
            return selectedNodes;
        }
        public ModelConnect DeleteConnect(ModelConnect modelConnect)
        {
            if (modelConnect != null)
            {
                _connects.Remove(modelConnect);
            }
            return modelConnect;
        }
        public object UnSelectedAllNodes()
        {
            foreach (ModelNode node in _nodes)
            {
                node.Selected = false;
            }
            return null;
        }
        public List<ModelNode> SelectedAllNodes()
        {
            foreach (ModelNode node in _nodes)
            {
                node.Selected = true;
            }
            return _nodes.ToList();
        }
        public List<ModelNode> MoveAllNode(Point delta, List<ModelNode> nodes = null)
        {
            if (nodes == null)
                nodes = _nodes.ToList();
            foreach (ModelNode node in nodes)
            {
                node.Move(delta);
            }
            return nodes;
        }
        public List<ModelNode> MoveAllSelectedNode(Point delta,List<ModelNode> selectedNodes=null)
        {
            if(selectedNodes==null)
             selectedNodes = GetSelectedNodes();
            foreach (ModelNode selectedNode in selectedNodes)
            {
                selectedNode.Move(delta);
            }
            return selectedNodes;
        }
        private List<ModelNode> GetSelectedNodes()
        {
            return _nodes.Where(x => x.Selected == true).ToList();
        }
        public ModelNode AddNewNode(Point position)
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
        public bool CheckConnect(ModelNode nodeFrom, ModelNode nodeTo)
        {
            if ((nodeFrom == nodeTo) || (nodeFrom == null) || (nodeTo == null))
                return false;

            return (_connects.Where(x => (x.FromConnector?.Node == nodeFrom) && (x.ToConnector?.Node == nodeTo)).Count() == 0);
        }
        public ModelNode AddNode(ModelNode modelNode)
        {
            string text = "State" + this._nodes.Count.ToString();
            modelNode.Text = text;
            _nodes.Add(modelNode);
            return modelNode;
        }
        public ModelConnect AddConnect(ModelConnect modelConnect)
        {
            _connects.Add(modelConnect);
            return modelConnect;
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
