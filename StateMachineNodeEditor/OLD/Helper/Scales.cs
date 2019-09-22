using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
namespace StateMachineNodeEditor
{
    public class Scales : INotifyPropertyChanged
    {
        private double _scaleX;
        private double _scaleY;
        private double _centerX;
        private double _centerY;
        public Scales()
        {
            _scaleX = 1;
            _scaleY = 1;
        }
        public Point ValueScale
        {
            get { return new Point(ScaleX, ScaleY); }
            set { ScaleX = value.X; ScaleY = value.Y; }
        }
        public Point ValueCenter
        {
            get { return new Point(CenterX, CenterY); }
            set { CenterX = value.X; CenterY = value.Y; }
        }
        public double ScaleX
        {
            get { return _scaleX; }
            set
            {
                _scaleX = value;
                OnPropertyChanged("ScaleX");
            }
        }
        public double ScaleY
        {
            get { return _scaleY; }
            set
            {
                _scaleY = value;
                OnPropertyChanged("ScaleY");
            }
        }
        public double CenterX
        {
            get { return _centerX; }
            set
            {
                _centerX = value;
                OnPropertyChanged("CenterX");
            }
        }
        public double CenterY
        {
            get { return _centerY; }
            set
            {
                _centerY = value;
                OnPropertyChanged("CenterY");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
