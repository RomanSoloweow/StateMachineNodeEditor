using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineNodeEditor
{
    public class SimpleCommand : ICloneable
    {
        public static Stack<SimpleCommand> Redo { get; set; } = new Stack<SimpleCommand>();
        public static Stack<SimpleCommand> Undo { get; set; } = new Stack<SimpleCommand>();
        private Func<object, object> _execute;

        private Func<object, object> _unExecute;
        public object Parameters { get; protected set; }
        public object Result { get; protected set; }
        public object Owner { get; protected set; }
        public object Clone()
        {
            SimpleCommand simpleCommand  = new SimpleCommand(Owner, _execute, _unExecute);
            simpleCommand.Parameters = this.Parameters;
            simpleCommand.Result = this.Result;
            return simpleCommand;
        }
        
        private void Init()
        {
            _execute = Empty;
            _unExecute = Empty;
        }
        public SimpleCommand(object owner)
        {        
            Owner = owner;        
        }
        public SimpleCommand(object owner, Func<object, object> action):this(owner)
        {
            SetExecute(action);
        }
        public SimpleCommand(object owner, Func<object, object> action, Func<object, object> unAction) : this(owner,action)
        {
            SetUnExecute(unAction);
        }
        public void Execute(object param)
        {
            Parameters = param;
            Result = this._execute(param);
            if (_unExecute != null)
            {
                Undo.Push(this.Clone() as SimpleCommand);
                Redo.Clear();
            }
        }
        public void Execute()
        {
            Result = this._execute(Parameters);
            Undo.Push(this.Clone() as SimpleCommand);
        }
        public void UnExecute()
        {
            this._unExecute(Result);
            Redo.Push(this.Clone() as SimpleCommand);
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
        
        private object Empty(object parameter)
        {
            return null;
        }

    }
}
