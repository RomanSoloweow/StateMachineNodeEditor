using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
    public partial class ViewModelConnector
    {
        private Brush _fill = Brushes.DarkGray;
        private Brush _stroke = Brushes.Black;
        public Brush Fill
        {
            get { return _fill; }
            set
            {
                _fill = value;
                OnPropertyChanged("Fill");
            }
        }
        public Brush Stroke
        {
            get { return _stroke; }
            set
            {
                _stroke = value;
                OnPropertyChanged("Stroke");
            }
        }
        public SimpleCommand CommandGetDataForDrop;
        public SimpleCommand CommandGetResultDrop;
        public SimpleCommand CommandAddConnect;
        public void AddCommands()
        {
            CommandGetDataForDrop = new SimpleCommand(this, GetDataForDrop);
            CommandGetResultDrop = new SimpleCommand(this, GetResultDrop);
            CommandAddConnect = new SimpleCommand(this, AddConnect);
        }
        private object GetDataForDrop(object parameters, object resultExecute)
        {           
            return connector.GetDataForDrop();
        }
        private object GetResultDrop(object parameters, object resultExecute)
        {
            return connector.CheckResultDrop();
        }
        private object AddConnect(object parameters, object resultExecute)
        {
            return null;
        }

    }
}
