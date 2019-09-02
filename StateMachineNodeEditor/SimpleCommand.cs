using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineNodeEditor
{
    public class SimpleCommand : ICloneable, IEquatable<SimpleCommand>
    {
        public static Stack<SimpleCommand> Redo { get; set; } = new Stack<SimpleCommand>();
        public static Stack<SimpleCommand> Undo { get; set; } = new Stack<SimpleCommand>();
        public static byte Types { get; set; }

        private Func<object, object, object> _execute;
        private Func<object, object, object> _unExecute;
        private Func<object, object, object> _combineParametr;
        private Func<object, object, object> _combineResult;
        private Func<object, object, bool> _equalsResult;
      
        public byte Type { get; protected set; }
        public object Parameters { get; protected set; }
        public object Result { get; protected set; }
        public object Owner { get; protected set; }
        public bool Combined { get; protected set; }
        public object Clone()
        {
            SimpleCommand simpleCommand  = new SimpleCommand(Owner, _execute, _unExecute, _combineParametr, _equalsResult, _combineResult);
            --Types;
            simpleCommand.Parameters = this.Parameters;
            simpleCommand.Result = this.Result;
            simpleCommand.Type = this.Type;

            return simpleCommand;
        }
        public bool Equals(SimpleCommand other)
        {
            if (other == null)
                return false;
            
            if(this.Type!= other.Type)
                return false;

            return _equalsResult(this.Result, other.Result);

        }     
        public SimpleCommand(object owner)
        {        
            Owner = owner;
            Type = (++Types);
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
        public SimpleCommand(object owner, Func<object, object, object> action, Func<object, object, object> unAction, Func<object, object, object> combineParametr, Func<object, object, bool> equalResult, Func<object, object, object> combineResult=null) : this(owner, action, unAction)
        {
            SetCombineParametr(combineParametr);
            SetCombineResult(combineResult);
            SetEqulsResult(equalResult);
        }
        public object Execute(object param)
        {
            Parameters = param;
            Result = this._execute(Parameters, Result);
            if (_unExecute != null)
            {
                if (!Combined)
                    AddInUndo();
                else
                    Combine();              
                Redo.Clear();
            }
            object resultCopy = Result;
            Result = null;
            Parameters = null;
            return resultCopy;
        }
        private void Combine()
        {
            SimpleCommand last = Undo.First();
            if (!last.Equals(this))
                AddInUndo();
            else
            {
                if (_combineParametr != null)
                    last.Parameters = _combineParametr(last.Parameters, this.Parameters);
                if (_combineResult != null)
                    last.Result = _combineResult(last.Result, this.Result);
            }
        }
        private void UpdateCombined()
        {
            Combined = (_combineParametr != null) || (_combineResult != null);
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
        public void SetCombineParametr(Func<object, object, object> combineParametr)
        {
            _combineParametr = combineParametr;
            UpdateCombined();
        }
        public void RemoveCombineParametr()
        {
            _combineParametr = null;
        }
        public void SetCombineResult(Func<object, object, object> combineResult)
        {
            _combineResult = combineResult;
            UpdateCombined();
        }
        public void RemoveCombineResult()
        {
            _combineResult = null;
        }
      
        public void SetEqulsResult(Func<object, object,bool> equal)
        {
            _equalsResult = equal; ;
        }
        public void RemoveEqulsResult()
        {
            _equalsResult = null;
        }
        
    }
}
