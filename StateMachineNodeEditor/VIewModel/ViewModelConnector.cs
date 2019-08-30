using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
namespace StateMachineNodeEditor
{
    public class ViewModelConnector : INotifyPropertyChanged
    {
        private ModelConnector connector { get; set; }
        public ViewModelConnector(ModelConnector modelConnector)
        {
            connector = modelConnector;
        }
        public String Text
        {
            get { return connector.Text; }
            set
            {
                connector.Text = value;
                OnPropertyChanged("Text");
            }
        }
        public bool TextIsEnable
        {
            get { return connector.TextIsEnable; }
            set
            {
                connector.TextIsEnable = value;
                OnPropertyChanged("TextIsEnable");
            }
        }
        public Brush Fill
        {
            get { return connector.Fill; }
            set
            {
                connector.Fill = value;
                OnPropertyChanged("Fill");
            }
        }
        public Brush Stroke
        {
            get { return connector.Stroke; }
            set
            {
                connector.Stroke = value;
                OnPropertyChanged("Stroke");
            }
        }
        public bool? Visible
        {
            get { return connector.Visible; }
            set
            {
                connector.Visible = value;
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
