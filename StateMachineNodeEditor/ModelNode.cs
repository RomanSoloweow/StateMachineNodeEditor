using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;


namespace StateMachineNodeEditor
{
    public class ModelNode : INotifyPropertyChanged
    {
        private Point _point1;
        private Point _point2;
        private ModelConnector _input;
        private ModelConnector _output;
        private ObservableCollection<ModelConnector> _transitions;
        private Translates _translate;
        private static Scales _sclale;
        private string _text;
        //private Brush 
        public  ModelNode()
        {
            _transitions = new ObservableCollection<ModelConnector>();
            _input = new ModelConnector()
            {
                Text = "Input",
                TextIsEnable = false
            };
            _output = new ModelConnector()
            {
                Text = "Output",
                TextIsEnable = false,
                Visible = false
            };
            _translate = new Translates();
        }   
        public Point Point1
        {
            get { return new Point(Translate.X, Translate.Y); }
            set
            {
                Translate.X = value.X;
                Translate.Y = value.Y;
                OnPropertyChanged("Translate");
            }
        }
        public Point Point2
        {
            get { return _point2; }
            set
            {
                _point2 = value;
                OnPropertyChanged("Point2");
            }
        }
        public ModelConnector Input
        {
            get { return _input; }
            set
            {
                _input = value;
                OnPropertyChanged("Input");
            }
        }
        public ModelConnector Output
        {
            get { return _output; }
            set
            {
                _output = value;
                OnPropertyChanged("Output");
            }
        }
        public ObservableCollection<ModelConnector> Transitions
        {
            get { return _transitions; }
            set
            {
                _transitions = value;
                OnPropertyChanged("Transitions");
            }
        }
        public Translates Translate
        {
            get { return _translate; }
            set
            {
                _translate = value;
                OnPropertyChanged("Translate");
     
            }
        }
        public Scales Sclale
        {
            get { return _sclale; }
            set
            {
                _sclale = value;
                OnPropertyChanged("Sclale");
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
