using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
    public class ViewModelConnect: INotifyPropertyChanged,IEquatable<ViewModelConnect>
    {
        private ModelConnect connect { get; set; }
        private Brush _stroke = Brushes.White;
        private DoubleCollection _strokeDashArray = null;
        public bool Equals(ViewModelConnect other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this.connect, other.connect))
                return true;

            if (this.GetType() != other.GetType())
                return false;

            return Equals(this.connect, other.connect) ;

        }
        public ViewModelConnect(ModelConnect modelConnect)
        {
            connect = modelConnect;
            connect.PropertyChanged += ModelPropertyChange;
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
        public void ModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName));
        }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
