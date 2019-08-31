using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineNodeEditor
{
    public class SimpleCommand
    {
        private Func<object, object> _execute;

        private Func<object, object> _unExecute;
        public object Parameters { get; protected set; }
        public object Result { get; protected set; }
        public object Owner { get; protected set; }
        private void Init()
        {
            _execute = Empty;
            _unExecute = Empty;
        }
        public SimpleCommand(object owner)
        {        
            Owner = owner;
            Init();
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
            this._execute(param);
        }
        public void UnExecute()
        {
            this._unExecute(Parameters);
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
