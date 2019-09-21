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

        private Func<object, object, object> _execute;
        private Func<object, object, object> _unExecute;     
        public object Parameters { get; protected set; }
        public object Result { get; protected set; }
        public object Owner { get; protected set; }
        private bool CanUnExecute
        {
            get { return _unExecute != null; }
        }
        public object Clone()
        {
            SimpleCommand simpleCommand  = new SimpleCommand(Owner, _execute, _unExecute);
            simpleCommand.Parameters = this.Parameters;
            simpleCommand.Result = this.Result;

            return simpleCommand;
        }
        public SimpleCommand(object owner)
        {        
            Owner = owner;
        }
        public SimpleCommand(object owner, Func<object, object, object> action):this(owner)
        {
            SetExecute(action);
        }
        public static object Test(object parameters, object resultExecute)
        {
            return null;
        }
        public SimpleCommand(object owner, Func<object, object, object> action, Func<object, object, object> unAction) : this(owner,action)
        {
            SetUnExecute(unAction);
        }
        public object Execute(object param)
        {
            Parameters = param;
            Result = this._execute(Parameters, Result);
            if (CanUnExecute)
            {
                 AddInUndo();            
                Redo.Clear();
            }
            object resultCopy = Result;
            Result = null;
            Parameters = null;
            return resultCopy;
        }
        private void AddInUndo()
        {
            Undo.Push(this.Clone() as SimpleCommand);
        }


        public object Execute()
        {
            Result = this._execute(Parameters,Result);
            Undo.Push(this.Clone() as SimpleCommand);
            return Result;
        }
        public void UnExecute()
        {
            this._unExecute(Parameters,Result);
            Redo.Push(this.Clone() as SimpleCommand);
        }
        public void SetExecute(Func<object, object, object> action)
        {
            _execute = action;
        }
        public void RemoveExecute()
        {
            _execute = null;
        }
        public void SetUnExecute(Func<object, object,object> action)
        {
            _unExecute = action;
        }     
        public void RemoveUnExecute()
        {
            _unExecute = null;
        }        
        
    }
}
