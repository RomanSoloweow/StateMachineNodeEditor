using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
    public class ModelNode : INotifyPropertyChanged
    {
        private ModelConnector _input;
        private ModelConnector _output;
        private ObservableCollection<ModelConnector> _transitions = new ObservableCollection<ModelConnector>();
        private ModelConnector _currentConnector;
        private ModelNodesCanvas _nodesCanvas;
        private Translates _translate = new Translates();
        private static Scales _sclale = new Scales();
        private string _text;
        private double _width;
        private double _height;
        private bool _selected;
        private Brush _borderBrush = Brushes.DarkGray;

        private ModelConnector AddEmptyConnector()
        {
            if (_currentConnector != null)
            {
                _currentConnector.TextIsEnable = true;
                _currentConnector.Text = "Transition_" + Transitions.Count.ToString();
            }
            _currentConnector = new ModelConnector(this);
            _currentConnector.TextIsEnable  = false;
            _transitions.Insert(0, _currentConnector);
            return _currentConnector;
        }
        //private Brush 
        public  ModelNode(ModelNodesCanvas modelNodesCanvas,string text=null, Point? point=null )
        {
            _nodesCanvas = modelNodesCanvas;
            _input = new ModelConnector(this)
            {
                Text = "Input",
                TextIsEnable = false
            };
            _output = new ModelConnector(this)
            {
                Text = "Output",
                TextIsEnable = false,
                Visible = false
            };

            Text = text??"Test";
            if (point != null)
                _translate.Value = point.Value;

            AddEmptyConnector();
        }
        public ModelNode Select(bool selectOnlyOne)
        {
            //ЛКМ
            if (selectOnlyOne)
            {
                if (!Selected)
                {
                    _nodesCanvas.UnSelectedAllNodes();
                    Selected = true;
                }
            }
            else //ctrl + ЛКМ
            {
                Selected = !Selected;
            }
            return this;
        }



        #region Property
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
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;

                if (value)
                    SetColorOnSelect();
                else
                    SetColorOnUnSelect();

                OnPropertyChanged("Select");
            }
        }
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                _borderBrush = value;
                OnPropertyChanged("BorderBrush");
            }
        }
        #endregion Property
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SetColorOnSelect()
        {
           this.BorderBrush = Brushes.Red;
        }
        private void SetColorOnUnSelect()
        {
            this.BorderBrush = Brushes.DarkGray;
        }
    }
}
