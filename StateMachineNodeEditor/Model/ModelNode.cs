using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections;
namespace StateMachineNodeEditor
{
    public class ModelNode : INotifyPropertyChanged, IEquatable<ModelNode>
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

        public bool Equals(ModelNode other)
        {
            if (other == null)
                return false;

            if (this.GetType() != other.GetType())
                return false;

            return Equals(this.Text, other.Text) && Equals(this.Translate, other.Translate) && Equals(this.Transitions, other.Transitions);
        }
        private ModelConnector AddEmptyConnector()
        {
            if (_currentConnector != null)
            {
                CurrentConnector.TextIsEnable = true;
                CurrentConnector.FormIsEnable = false;
                CurrentConnector.Text = "Transition_" + Transitions.Count.ToString();
            }
            _currentConnector = new ModelConnector(this);
            _currentConnector.TextIsEnable = false;
            //_transitions.Add(_currentConnector);
            _transitions.Insert(0, _currentConnector);
            return _currentConnector;
        }
       public void DropSuccessfull()
        {
            AddEmptyConnector();
        }
        public void DropUnSuccessfull()
        {
            NodesCanvas.DeleteConnect(CurrentConnector.Connect);
        }
        public bool CheckConnect(ModelNode nodeFrom)
        {

            return NodesCanvas.CheckConnect(nodeFrom, this);
        }
        public ModelNode(ModelNodesCanvas modelNodesCanvas, string text = null, Point? point = null)
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

            Text = text ;
            if (point != null)
                _translate.Value = point.Value;

            AddEmptyConnector();
        }
        public bool Select(bool selectOnlyOne)
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
            return Selected;
        }
        public ModelConnect GetNewConnect()
        {
            ModelConnect modelConnect = NodesCanvas.GetNewConnect(CurrentConnector.Position);
            modelConnect.FromConnector = CurrentConnector;
            return modelConnect;
        }
        public ModelConnect AddConnectIfDrop(ModelConnect modelConnect)
        {
            if (modelConnect != null)
            {
                NodesCanvas.AddConnect(modelConnect);
                CurrentConnector.Connect = modelConnect;
                DropSuccessfull();
            }
            if (CurrentConnector.Connect.ToConnector != null)
                DropSuccessfull();
            else
                DropUnSuccessfull();
            return CurrentConnector.Connect;
        }
        public ModelConnect DelereConnect(ModelConnect modelConnect)
        {
            return NodesCanvas.DeleteConnect(modelConnect);
        }
        #region Property
        public ModelNodesCanvas NodesCanvas
        {
             get { return _nodesCanvas; }
             set
             {
                _nodesCanvas = value;
                OnPropertyChanged("NodesCanvas");
             }
        }
        public ModelConnector CurrentConnector
        {
            get { return _currentConnector; }
            set
            {
                _currentConnector = value;
                OnPropertyChanged("CurrentConnector");
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
        public Scales Scale
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
                OnPropertyChanged("Selected");
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

        #endregion Property
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void Move(Point delta)
        {
            Translate.Value= ForPoint.Addition(Translate.Value, delta);
        }

      
    }
}
