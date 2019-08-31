using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Collections.Generic;

namespace StateMachineNodeEditor
{
    public class ViewModelNode : INotifyPropertyChanged
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
            CommandSelect = new SimpleCommand(this, Select);
            node.PropertyChanged += ModelPropertyChange;
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
        public Brush BorderBrush
        {
            get { return node.BorderBrush; }
            set
            {
                node.BorderBrush = value;
                OnPropertyChanged("BorderBrush");
            }
        }
        #endregion Property
        public SimpleCommand CommandSelect { get; set; }
        public ModelNode Select(object parameters)
        {
            bool selectOnlyOne = false;
            bool.TryParse(parameters.ToString(),out selectOnlyOne);
           return node.Select(selectOnlyOne);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void ModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName));
        }       
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }

}
