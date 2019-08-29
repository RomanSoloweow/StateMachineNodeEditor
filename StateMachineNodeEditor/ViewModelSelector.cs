using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;

namespace StateMachineNodeEditor
{
    public class ViewModelSelector : INotifyPropertyChanged
    {
        private ModelSelector selector;
        public ViewModelSelector(ModelSelector modelSelector)
        {
            selector = modelSelector;
        }
        public Point Point1
        {
            get { return selector.Point1; }
            set
            {
                selector.Point1 = value;
                OnPropertyChanged("Point1");
            }
        }
        public Point Point2
        {
            get { return selector.Point2; }
            set
            {
                selector.Point2 = value;
                OnPropertyChanged("Point2");
            }
        }
        public Translates Translate
        {
            get { return selector.Translate; }
            set
            {
                selector.Translate = value;
                OnPropertyChanged("Translate");

            }
        }
        public Scales Sclale
        {
            get { return selector.Sclale; }
            set
            {
                selector.Sclale = value;
                OnPropertyChanged("Sclale");
            }
        }
        public bool? Visible
        {
            get { return selector.Visible; }
            set
            {
                selector.Visible = value;
                OnPropertyChanged("Visible");
            }
        }
        public double Width
        {
            get { return selector.Width; }
            set
            {
                selector.Width = value;
                OnPropertyChanged("Width");
            }
        }
        public double Height
        {
            get { return selector.Height; }
            set
            {
                selector.Height = value;
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
