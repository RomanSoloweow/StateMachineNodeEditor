using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace StateMachineNodeEditor
{
    public class ViewModelNode : INotifyPropertyChanged
    {
        private ModelNode node { get; set; }
        public ObservableCollection<ViewModelConnector> Transitions { get; set; } = new ObservableCollection<ViewModelConnector>();
        public ViewModelConnector Input { get; set; }
        public ViewModelConnector Output { get; set; }
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
        }
        public void TransitionsChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    ModelConnector connector = element as ModelConnector;
                    if (connector != null)
                        Transitions.Add(new ViewModelConnector(connector));
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
        public Point Point1
        {
            get { return node.Point1; }
            set
            {
                node.Point1 = value;
                OnPropertyChanged("Point1");
            }
        }
        public Point Point2
        {
            get { return node.Point2; }
            set
            {
                node.Point2 = value;
                OnPropertyChanged("Point2");
            }
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
        public Scales Sclale
        {
            get { return node.Sclale; }
            set
            {
                node.Sclale = value;
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
