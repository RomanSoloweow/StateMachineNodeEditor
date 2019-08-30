using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
    public class ViewModelConnect: INotifyPropertyChanged
    {
        private ModelConnect connect { get; set; }
        public ViewModelConnect(ModelConnect modelConnect)
        {
            connect = modelConnect;
        }
        public Point StartPoint
        {
            get { return connect.StartPoint; }
            set
            {
                connect.StartPoint = value;
                OnPropertyChanged("StartPoint");
            }
        }
        public Point EndPoint
        {
            get { return connect.EndPoint; }
            set
            {
                connect.EndPoint = value;
                OnPropertyChanged("EndPoint");
            }
        }
        public Point Point1
        {
            get { return connect.Point1; }
            set
            {
                connect.Point1 = value;
                OnPropertyChanged("Point1");
            }
        }
        public Point Point2
        {
            get { return connect.Point2; }
            set
            {
                connect.Point2 = value;
                OnPropertyChanged("Point2");
            }
        }
        public Brush Stroke
        {
            get { return connect.Stroke; }
            set
            {
                connect.Stroke = value;
                OnPropertyChanged("Stroke");
            }
        }
        public DoubleCollection StrokeDashArray
        {
            get { return connect.StrokeDashArray; }
            set
            {
                connect.StrokeDashArray = value;
                OnPropertyChanged("StrokeDashArray");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
