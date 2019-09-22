using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
    public class ModelConnect: INotifyPropertyChanged, IEquatable<ModelConnect>
    {
        private Point _startPoint;
        private Point _endPoint;
        private Point _point1;
        private Point _point2;
        private ModelConnector _fromConnector;
        private ModelConnector _toConnector;
        public bool Equals(ModelConnect other)
        {
            if (other == null)
                return false;

            if (this.GetType() != other.GetType())
                return false;
            
            return Equals(this.ToConnector, other.ToConnector) && Equals(this.FromConnector, other.FromConnector);
        }
        public ModelConnect(Point? point)
        {
            if (point != null)
                StartPoint = point.Value;
        }
        public Point StartPoint
        {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                Update();
                OnPropertyChanged("StartPoint");
               
            }
        }
        public Point EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                Update();
                OnPropertyChanged("EndPoint");
              
            }
        }
        public Point Point1
        {
            get { return _point1; }
            set
            {
                _point1 = value;
                OnPropertyChanged("Point1");
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
        public ModelConnector FromConnector
        {
            get { return _fromConnector; }
            set
            {
                _fromConnector = value;
                OnPropertyChanged("FromConnector");
            }
        }
        public ModelConnector ToConnector
        {
            get { return _toConnector; }
            set
            {
                _toConnector = value;
                OnPropertyChanged("ToConnector");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private void Update()
        {
            Vector different = EndPoint - StartPoint;
            Point1 = new Point(StartPoint.X + 3 * different.X / 8, StartPoint.Y + 1 * different.Y / 8);
            Point2 = new Point(StartPoint.X + 5 * different.X / 8, StartPoint.Y + 7 * different.Y / 8);
        }
    }
}
