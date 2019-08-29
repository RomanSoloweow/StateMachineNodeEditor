using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;

namespace StateMachineNodeEditor
{
    public class ViewModelNode : INotifyPropertyChanged
    {
        public ModelNode Node { get; set; }
        public ViewModelNode()
        {
            Node = new ModelNode();
        }

        public Point Point1
        {
            get { return Node.Point1; }
            set
            {
                Node.Point1 = value;
                OnPropertyChanged("Point1");
            }
        }
        public Point Point2
        {
            get { return Node.Point2; }
            set
            {
                Node.Point2 = value;
                OnPropertyChanged("Point2");
            }
        }
        public ModelConnector Input
        {
            get { return Node.Input; }
            set
            {
                Node.Input = value;
                OnPropertyChanged("Input");
            }
        }
        public ModelConnector Output
        {
            get { return Node.Output; }
            set
            {
                Node.Output = value;
                OnPropertyChanged("Output");
            }
        }
        public ObservableCollection<ModelConnector> Transitions
        {
            get { return Node.Transitions; }
            set
            {
                Node.Transitions = value;
                OnPropertyChanged("Transitions");
            }
        }
        public Translates Translate
        {
            get { return Node.Translate; }
            set
            {
                Node.Translate = value;
                OnPropertyChanged("Translate");
            }
        }
        public Scales Sclale
        {
            get { return Node.Sclale; }
            set
            {
                Node.Sclale = value;
                OnPropertyChanged("Sclale");
            }
        }
        public String Text
        {
            get { return Node.Text; }
            set
            {
                Node.Text = value;
                OnPropertyChanged("Text");
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
