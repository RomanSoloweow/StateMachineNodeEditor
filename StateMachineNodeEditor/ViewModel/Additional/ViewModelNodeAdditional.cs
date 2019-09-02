using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;

namespace StateMachineNodeEditor
{
   public partial class ViewModelNode
    {
        private Brush _borderBrush = Brushes.DarkGray;
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                _borderBrush = value;
                OnPropertyChanged("BorderBrush");
            }
        }
        public void AddCommands()
        {
            CommandSelect = new SimpleCommand(this, Select);
        }
        public SimpleCommand CommandSelect { get; set; }
        public object Select(object parameters, object resultExecute)
        {
            bool selectOnlyOne = false;
            bool.TryParse(parameters.ToString(), out selectOnlyOne);
            node.Select(selectOnlyOne);

            return null;
        }
        private void UpdateBorderBrush()
        {
            if (Selected)
                BorderBrush = Brushes.Red;
            else
                BorderBrush = Brushes.DarkGray;
        }
    }
}
