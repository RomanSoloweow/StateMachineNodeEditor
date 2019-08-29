using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
namespace StateMachineNodeEditor
{
    public class ViewModelConnector : INotifyPropertyChanged
    {
        public ModelConnector Connector { get; set; }
        public ViewModelConnector()
        {
            Connector = new ModelConnector();
        }
        public Point Position
        {
            get { return Connector.Position; }
            set
            {
                Connector.Position = value;
                OnPropertyChanged("Position");
            }
        }
        public ModelNode Node
        {
            get { return Connector.Node; }
            set
            {
                Connector.Node = value;
                OnPropertyChanged("Node");
            }
        }
        public ModelConnect Connect
        {
            get { return Connector.Connect; }
            set
            {
                Connector.Connect = value;
                OnPropertyChanged("Connect");
            }
        }
        public String Text
        {
            get { return Connector.Text; }
            set
            {
                Connector.Text = value;
                OnPropertyChanged("Text");
            }
        }
        public bool TextIsEnable
        {
            get { return Connector.TextIsEnable; }
            set
            {
                Connector.TextIsEnable = value;
                OnPropertyChanged("TextIsEnable");
            }
        }
        public Brush Fill
        {
            get { return Connector.Fill; }
            set
            {
                Connector.Fill = value;
                OnPropertyChanged("Fill");
            }
        }
        public Brush Stroke
        {
            get { return Connector.Stroke; }
            set
            {
                Connector.Stroke = value;
                OnPropertyChanged("Stroke");
            }
        }
        public bool? Visible
        {
            get { return Connector.Visible; }
            set
            {
                Connector.Visible = value;
                OnPropertyChanged("Visible");
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
