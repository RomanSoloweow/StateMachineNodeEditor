using System;
using System.Windows.Input;
using System.Windows;

namespace StateMachineNodeEditor
{
    public class Command:RoutedUICommand
    {
        private Action<object> _execute;
        private Action<object> _unExecute;
        private object _parameter;
        private object _owner;
        public object Owner { get; protected set; }
        public new void Execute(object parameter, IInputElement target)
        {

        }
        public void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            _parameter = e.Parameter;
            _owner = sender;
            _execute(_parameter);
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
        public Command(Action<object> action) : this()
        {
            SetExecute(action);
        }
        public Command(Action<object> action, Action<object> unAction) : this(action)
        {
            SetUnExecute(unAction);
        }
        public Command(string text, string name, Type ownerType):base(text,name,ownerType)
        {
            Init();
        }
        public Command(string text, string name, Type ownerType, Action<object> action) : this(text, name, ownerType)
        {
            SetExecute(action);
        }
        public Command(string text, string name, Type ownerType, Action<object> action, Action<object> unAction) : this(text, name, ownerType, action)
        {
            SetUnExecute(unAction);
        }
        public Command(string text, string name, Type ownerType, InputGestureCollection inputGestures):base(text, name, ownerType, inputGestures)
        {
            Init();
        }
        public Command(string text, string name, Type ownerType, InputGestureCollection inputGestures, Action<object> action):base(text, name, ownerType, inputGestures)
        {
            SetExecute(action);
        }
        public Command(string text, string name, Type ownerType, InputGestureCollection inputGestures, Action<object> action, Action<object> unAction) : this(text, name, ownerType, inputGestures, action)
        {
            SetUnExecute(unAction);
        }
        public Command(RoutedUICommand routedUICommand):this(routedUICommand.Text,routedUICommand.Name,routedUICommand.OwnerType,routedUICommand.InputGestures)
        {

        }
        public Command(RoutedUICommand routedUICommand,Action<object> action) : this(routedUICommand)
        {
           SetExecute(action);
        }
        public Command(RoutedUICommand routedUICommand, Action<object> action, Action<object> unAction) : this(routedUICommand, action)
        {
            SetUnExecute(unAction);

        }
        public void SetExecute(Action<object> action) 
        {
            _execute = action;
        }
        public void SetUnExecute(Action<object> action)
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
            this._unExecute(_parameter);
        }
        private void Empty(object parameter)
        {

        }
        public CommandBinding GetCommandBinding()
        {
            return new CommandBinding(this, Execute);
        }
    }
}
