using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
namespace StateMachineNodeEditor
{
    public partial class ViewModelConnector
    {
        private ModelConnector connector { get; set; }
        public ViewModelConnector(ModelConnector modelConnector)
        {
            connector = modelConnector;
            connector.PropertyChanged += ModelPropertyChange;
            AddCommands();
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
        public bool? Visible
        {
            get { return connector.Visible; }
            set
            {
                connector.Visible = value;
                OnPropertyChanged("Visible");
            }
        }
        public bool FormIsEnable
        {
            get { return connector.FormIsEnable; }
            set
            {
                connector.FormIsEnable = value;
                OnPropertyChanged("FormIsEnable");
            }
        }

        private event PropertyChangedEventHandler PropertyChanged;
        private void ModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName));
        }
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
