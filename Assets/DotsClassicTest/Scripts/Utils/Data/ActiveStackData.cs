using System;
using System.Collections.Generic;
using System.Linq;


namespace DotsClassicTest.Utils.Data
{
    [Serializable]
    public class ActiveStackData<T> : Stack<T>
    {
        public event Action<Stack<T>> UpdateEvent;
        public event Action<T> AddEvent;
        public event Action<T> RemoveEvent;

        public T PreLast;

        private Stack<T> value;

        public ActiveStackData(Stack<T> stack = null)
        {
            value = stack != null ? stack : new Stack<T>();
        }

        public new void Push(T item)
        {
            value.Push(item);
            UpdateEvent.Call(value);
            AddEvent.Call(item);
        }

        public void CallUpdated()
        {
            UpdateEvent.Call(value);
        }
        
        public bool IsHasDuplicates=> value.Distinct().Count() != value.Count;

        public new void CopyTo(T[] array, int arrayIndex)
        {
            value.CopyTo(array, arrayIndex);
        }

        public T Pop()
        {
            var result = value.Pop();
            if (result!=null)
            {
                UpdateEvent.Call(value);
                RemoveEvent.Call(result);
            }

            return result;
        }
        
        public T Peek()=>value.Peek();

        public new int Count => value.Count;

        public bool IsReadOnly => false;
        
        public new void Clear()
        {
            PreLast = default;
            if (value.Count == 0) return;

            foreach (var item in value)
                RemoveEvent.Call(item);

            value.Clear();
            UpdateEvent.Call(value);
        }

        public new bool Contains(T item) => value.Contains(item);

        public virtual Stack<T> Value
        {
            get => value;
            set
            {
                if (this.value != null && !this.value.Equals(value))
                {
                    this.value = value;
                    UpdateEvent.Call(this.value);
                }
                else if (this.value == null)
                {
                    this.value = value;
                    UpdateEvent.Call(this.value);
                }
            }
        }

        public new IEnumerator<T> GetEnumerator() => value.GetEnumerator();
    }
}