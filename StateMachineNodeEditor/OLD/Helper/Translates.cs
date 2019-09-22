using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
namespace StateMachineNodeEditor
{
    public class Translates : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }
        public Point Value
        {
            get { return new Point(X, Y); }
            set { X = value.X;Y = value.Y; }
        }
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
