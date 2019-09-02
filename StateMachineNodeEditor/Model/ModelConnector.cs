using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
namespace StateMachineNodeEditor
{
   public class ModelConnector: INotifyPropertyChanged
    {
        private Point _position;
        private ModelNode _node;
        private ModelConnect _connect;
        private String _text;
        private bool _textIsEnable;
        private bool? _visible;
        private bool _formIsEnable;

        public ModelConnector(ModelNode node)
        {
            _node = node;
            Text = "";
            TextIsEnable = true;
            Visible = true;
            FormIsEnable = true;
        }


        public object GetDataForDrop()
        {
            Connect = this.Node.GetNewConnect();
            DataObject data = new DataObject();
            data.SetData("Node", this.Node);
            data.SetData("Connector", this);
            data.SetData("Connect", Connect);
            return data;
        }
        public object CheckResultDrop()
        {
            if (Connect.ToConnector!=null)
            {
                Node.DropSuccessfull();            
            }
            else
            {
                Node.DropUnSuccessfull();
            }
            return Connect;
        }

        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }
        public ModelNode Node
        {
            get { return _node; }
            set
            {
                _node = value;
                OnPropertyChanged("Node");
            }
        }
        public ModelConnect Connect
        {
            get { return _connect; }
            set
            {
                _connect = value;
                OnPropertyChanged("Connect");
            }
        }
        public String Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        public bool TextIsEnable
        {
            get { return _textIsEnable; }
            set
            {
                _textIsEnable = value;
                OnPropertyChanged("TextIsEnable");
            }
        }
        public bool? Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }
        public bool FormIsEnable
        {
            get { return _formIsEnable; }
            set
            {
                _formIsEnable = value;
                OnPropertyChanged("FormIsEnable");
            }
        }
   
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
