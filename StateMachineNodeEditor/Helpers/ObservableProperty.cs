using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineNodeEditor.Helpers
{
    public class ObservableProperty<T> : IObservable<T> where T : new()
    {
        private List<IObserver<T>> observers;
        public T Value { get; set; }

        public ObservableProperty()
        {
            observers = new List<IObserver<T>>();
        }

        //public delegate void ChangeEvent(T data);
        //public event ChangeEvent changed;

        //public ObservableProperty(T initialValue = default(T))
        //{
        //    Value = initialValue;
        //}

        //public void Set(T v)
        //{
        //    if (!v.Equals(Value))
        //    {
        //        Value = v;
        //        if (changed != null)
        //        {
        //            changed(Value);
        //        }
        //    }
        //}

        //public T Get()
        //{
        //    return Value;
        //}

        //public static implicit operator T(ObservableProperty<T> p)
        //{
        //    return p.Value;
        //}
        public IDisposable Subscribe(IObserver<T> observer)
        {
            throw new NotImplementedException();
        }
    }
}
