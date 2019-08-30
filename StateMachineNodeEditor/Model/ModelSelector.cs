using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StateMachineNodeEditor
{
    public class ModelSelector : INotifyPropertyChanged
    {
        private Translates _translate;
        private Scales _sclale;
        private bool? _visible;
        private double _width;
        private double _height;
        public ModelSelector()
        {
            _translate = new Translates();
            _sclale = new Scales();
            _visible = true;
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
        public bool? Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged("Visible");
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
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
