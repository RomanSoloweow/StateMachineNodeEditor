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
        private Brush _fill;
        private Brush _stroke;
        private bool? _visible;

        public ModelConnector(ModelNode node)
        {
            _node = node;
            Text = "";
            Fill = Brushes.DarkGray;
            Stroke = Brushes.Black;
            TextIsEnable = true;
            Visible = true;
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
        public bool? Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
