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
        public SimpleCommand CommandGetDataForDrag;
        public SimpleCommand CommandAddConnectIfDrop;
        public SimpleCommand CommandOnDrop;
        public Point Position
        {
            get
            {
                return connector.Position;
            }
            set
            {
                connector.Position = value;
            }
        }
        public void AddCommands()
        {
            CommandGetDataForDrag = new SimpleCommand(this, GetDataForDrop);
            CommandAddConnectIfDrop = new SimpleCommand(this, AddConnectIfDrop, DeleteConnect);
            CommandOnDrop = new SimpleCommand(this, OnDrop);
        }
        private object GetDataForDrop(object parameters, object resultExecute)
        {           
            return connector.GetDataForDrag();
        }
        private object AddConnectIfDrop(object parameters, object resultExecute)
        {
            return connector.AddConnectIfDrop(resultExecute as ModelConnect);
        }
        private object DeleteConnect(object parameters, object resultExecute)
        {
            return connector.DeleteConnect(resultExecute as ModelConnect);
        }

        private object OnDrop(object parameters, object resultExecute)
        {
            return connector.OnDrop(parameters as DataObject);
        }

    }
}
