using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace StateMachineNodeEditor
{
    public partial class ViewModelNode : INotifyPropertyChanged, IEquatable<ViewModelNode>
    {
        private ModelNode node { get; set; }
        public ViewModelNode(ModelNode modelNode)
        {
            node = modelNode;
            Input = new ViewModelConnector(node.Input);
            Output = new ViewModelConnector(node.Output);
            foreach (ModelConnector modelConnector in node.Transitions)
            {
                Transitions.Add(new ViewModelConnector(modelConnector));
            }
            node.Transitions.CollectionChanged += TransitionsChange;    
            node.PropertyChanged += ModelPropertyChange;
            AddCommands();
        }
        public bool Equals(ViewModelNode other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this.node, other.node))
                return true;

            if (this.GetType() != other.GetType())
                return false;

            return Equals(this.node,other.node);

        }
        #region Property
        public ObservableCollection<ViewModelConnector> Transitions { get; set; } = new ObservableCollection<ViewModelConnector>();
        public ViewModelConnector Input { get; set; }
        public ViewModelConnector Output { get; set; }    
        public void TransitionsChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    ModelConnector connector = element as ModelConnector;
                    if (connector != null)
                        if(e.NewStartingIndex== (Transitions.Count))
                            Transitions.Add(new ViewModelConnector(connector));
                        else
                            Transitions.Insert(e.NewStartingIndex, new ViewModelConnector(connector));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var element in e.OldItems)
                {
                    ModelConnector connector = element as ModelConnector;
                    if (connector != null)
                        Transitions.Remove(new ViewModelConnector(connector));
                }
            }
            OnPropertyChanged("Transitions");
        }
        public Translates Translate
        {
            get { return node.Translate; }
            set
            {
                node.Translate = value;
                OnPropertyChanged("Translate");
            }
        }
        public Scales Scale
        {
            get { return node.Scale; }
            set
            {
                node.Scale = value;
                OnPropertyChanged("Sclale");
            }
        }
        public String Text
        {
            get { return node.Text; }
            set
            {
                node.Text = value;
                OnPropertyChanged("Text");
            }
        }
        public double Width
        {
            get { return node.Width; }
            set
            {
                node.Width = value;
                OnPropertyChanged("Width");
            }
        }
        public double Height
        {
            get { return node.Height; }
            set
            {
                node.Height = value;
                OnPropertyChanged("Height");
            }
        }  
        public bool Selected
        {
            get { return node.Selected; }
            set
            {
                node.Selected = value;
                UpdateBorderBrush();
                OnPropertyChanged("Selected");
            }
        }
        #endregion Property

        public event PropertyChangedEventHandler PropertyChanged;
        public void ModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null)
                return;
            
         
            PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName));
            if (e.PropertyName == "Selected")
                UpdateBorderBrush();
        }       
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged == null)
                return;

                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }

}
