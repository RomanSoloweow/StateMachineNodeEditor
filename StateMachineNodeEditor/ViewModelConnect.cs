using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
    class ViewModelConnect: INotifyPropertyChanged
    {
        public ModelConnect Connect { get; set; }

        public ViewModelConnect()
        {
            Connect = new ModelConnect()
            {
            Stroke = Brushes.DarkGray,
            StrokeDashArray=DoubleCollection.Parse(" 10 3 ")
            //StartPoint = new Point(0,0),
            //EndPoint = new Point(100,100)
            };
        }

        public Point StartPoint
        {
            get { return Connect.StartPoint; }
            set
            {
                Connect.StartPoint = value;
                OnPropertyChanged("StartPoint");
            }
        }
        public Point EndPoint
        {
            get { return Connect.EndPoint; }
            set
            {
                Connect.EndPoint = value;
                OnPropertyChanged("EndPoint");
            }
        }
        public Point Point1
        {
            get { return Connect.Point1; }
            set
            {
                Connect.Point1 = value;
                OnPropertyChanged("Point1");
            }
        }
        public Point Point2
        {
            get { return Connect.Point2; }
            set
            {
                Connect.Point2 = value;
                OnPropertyChanged("Point2");
            }
        }
        public ModelConnector InputConnect
        {
            get { return Connect.InputConnect; }
            set
            {
                Connect.InputConnect = value;
                OnPropertyChanged("InputConnect");
            }
        }
        public ModelConnector OutputConnect
        {
            get { return Connect.OutputConnect; }
            set
            {
                Connect.OutputConnect = value;
                OnPropertyChanged("OutputConnect");
            }
        }
        public Brush Stroke
        {
            get { return Connect.Stroke; }
            set
            {
                Connect.Stroke = value;
                OnPropertyChanged("Stroke");
            }
        }
        public DoubleCollection StrokeDashArray
        {
            get { return Connect.StrokeDashArray; }
            set
            {
                Connect.StrokeDashArray = value;
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
