using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;

namespace StateMachineNodeEditor
{
    public class ViewModelSelector : INotifyPropertyChanged
    {
        public ModelSelector Selector;
        public ViewModelSelector()
        {
            Selector = new ModelSelector();
            Selector.Translate.X = 10;
            Selector.Translate.Y = 10;
            Selector.Width = 500;
            Selector.Height = 500;
        }
        public Point Point1
        {
            get { return Selector.Point1; }
            set
            {
                Selector.Point1 = value;
                OnPropertyChanged("Point1");
            }
        }
        public Point Point2
        {
            get { return Selector.Point2; }
            set
            {
                Selector.Point2 = value;
                OnPropertyChanged("Point2");
            }
        }
        public Translates Translate
        {
            get { return Selector.Translate; }
            set
            {
                Selector.Translate = value;
                OnPropertyChanged("Translate");

            }
        }
        public Scales Sclale
        {
            get { return Selector.Sclale; }
            set
            {
                Selector.Sclale = value;
                OnPropertyChanged("Sclale");
            }
        }
        public bool? Visible
        {
            get { return Selector.Visible; }
            set
            {
                Selector.Visible = value;
                OnPropertyChanged("Visible");
            }
        }
        public double Width
        {
            get { return Selector.Width; }
            set
            {
                Selector.Width = value;
                OnPropertyChanged("Width");
            }
        }
        public double Height
        {
            get { return Selector.Height; }
            set
            {
                Selector.Height = value;
                OnPropertyChanged("Height");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
