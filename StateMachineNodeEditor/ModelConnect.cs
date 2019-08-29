using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
    public class ModelConnect: INotifyPropertyChanged
    {
        private Point _startPoint;
        private Point _endPoint;
        private Point _point1;
        private Point _point2;
        private ModelConnect _inputConnect;
        private ModelConnect _outputConnect;
        private Brush _stroke;
        private DoubleCollection _strokeDashArray;
        public Point StartPoint
        {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                OnPropertyChanged("StartPoint");
                Update();
            }
        }
        public Point EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                OnPropertyChanged("EndPoint");
                Update();
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
        public ModelConnect InputConnect
        {
            get { return _inputConnect; }
            set
            {
                _inputConnect = value;
                OnPropertyChanged("InputConnect");
            }
        }
        public ModelConnect OutputConnect
        {
            get { return _outputConnect; }
            set
            {
                _outputConnect = value;
                OnPropertyChanged("OutputConnect");
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
        public DoubleCollection StrokeDashArray
        {
            get { return _strokeDashArray; }
            set
            {
                _strokeDashArray = value;
                OnPropertyChanged("StrokeDashArray");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void Update()
        {
            Vector different = EndPoint - StartPoint;
            Point1 = new Point(StartPoint.X + 3 * different.X / 8, StartPoint.Y + 1 * different.Y / 8);
            Point2 = new Point(StartPoint.X + 5 * different.X / 8, StartPoint.Y + 7 * different.Y / 8);
        }
    }
}
