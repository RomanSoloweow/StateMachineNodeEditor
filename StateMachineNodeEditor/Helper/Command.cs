using System;
using System.Windows.Input;
using System.Windows;

namespace StateMachineNodeEditor
{
    public class Command : RoutedUICommand
    {
        //private Func<object, object> _execute;
        private Func<object, object> _execute;

        private Func<object, object> _unExecute;
        public object Parameters { get; protected set; }
        public object Result { get; protected set; }
        public object Owner { get; protected set; }
        public void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Parameters = e.Parameter;
            Owner = sender;
            Result = _execute(Parameters);
        }
        private void Init()
        {
            _execute = Empty;
            _unExecute = Empty;
        }
        public Command()
        {
            Init();
        }
        public Command(Func<object, object> action) : this()
        {
            SetExecute(action);
        }
        public Command(Func<object, object> action, Func<object, object> unAction) : this(action)
        {
            SetUnExecute(unAction);
        }
        public Command(string text, string name, Type ownerType) : base(text, name, ownerType)
        {
            Init();
        }
        public Command(string text, string name, Type ownerType, Func<object, object> action) : this(text, name, ownerType)
        {
            SetExecute(action);
        }
        public Command(string text, string name, Type ownerType, Func<object, object> action, Func<object, object> unAction) : this(text, name, ownerType, action)
        {
            SetUnExecute(unAction);
        }
        public Command(string text, string name, Type ownerType, InputGestureCollection inputGestures) : base(text, name, ownerType, inputGestures)
        {
            Init();
        }
        public Command(string text, string name, Type ownerType, InputGestureCollection inputGestures, Func<object, object> action) : base(text, name, ownerType, inputGestures)
        {
            SetExecute(action);
        }
        public Command(string text, string name, Type ownerType, InputGestureCollection inputGestures, Func<object, object> action, Func<object, object> unAction) : this(text, name, ownerType, inputGestures, action)
        {
            SetUnExecute(unAction);
        }
        public Command(RoutedUICommand routedUICommand) : this(routedUICommand.Text, routedUICommand.Name, routedUICommand.OwnerType, routedUICommand.InputGestures)
        {

        }
        public Command(RoutedUICommand routedUICommand, Func<object, object> action) : this(routedUICommand)
        {
            SetExecute(action);
        }
        public Command(RoutedUICommand routedUICommand, Func<object, object> action, Func<object, object> unAction) : this(routedUICommand, action)
        {
            SetUnExecute(unAction);

        }
        public void SetExecute(Func<object, object> action)
        {
            _execute = action;
        }
        public void SetUnExecute(Func<object, object> action)
        {
            _unExecute = action;
        }
        public void RemoveExecute()
        {
            _execute = Empty;
        }
        public void RemoveUnExecute()
        {
            _unExecute = Empty;
        }
        public void UnExecute()
        {
            this._unExecute(Parameters);
        }
        private object Empty(object parameter)
        {
            return null;
        }

    }
}
