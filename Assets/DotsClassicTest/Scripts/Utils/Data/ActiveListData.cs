using System;
using System.Collections.Generic;


namespace DotsClassicTest.Utils.Data
{
    [Serializable]
    public class ActiveListData<T> : List<T>
    {
        public event Action<List<T>> UpdateEvent;
        public event Action<T> AddEvent;
        public event Action<T> RemoveEvent;

        private List<T> value;

        public ActiveListData(List<T> list = null)
        {
            value = list != null ? list : new List<T>();
        }

        public new void Add(T item)
        {
            value.Add(item);
            UpdateEvent.Call(value);
            AddEvent.Call(item);
        }

        public void CallUpdated()
        {
            UpdateEvent.Call(value);
        }

        public new void CopyTo(T[] array, int arrayIndex)
        {
            value.CopyTo(array, arrayIndex);
        }

        public new bool Remove(T item)
        {
            var result = value.Remove(item);
            if (result)
            {
                UpdateEvent.Call(value);
                RemoveEvent.Call(item);
            }

            return result;
        }

        public new int Count => value.Count;

        public bool IsReadOnly => false;
        
        public new void Clear()
        {
            if (value.Count == 0) return;

            foreach (var item in value)
                RemoveEvent.Call(item);

            value.Clear();
            UpdateEvent.Call(value);
        }

        public new bool Contains(T item) => value.Contains(item);

        public void AddRange(List<T> range)
        {
            value.AddRange(range);
            UpdateEvent.Call(value);
        }

        public virtual List<T> Value
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
        public new int IndexOf(T item) => value.IndexOf(item);

        public new void Insert(int index, T item)
        {
            value.Insert(index, item);
            UpdateEvent.Call(value);
        }

        public new void RemoveAt(int index)
        {
            var result = value[index];
            value.RemoveAt(index);
            UpdateEvent.Call(value);
            RemoveEvent.Call(result);
        }

        public new T this[int index]
        {
            get => value[index];
            set => this.value[index] = value;
        }


        public new T Find(Predicate<T> match) => Value.Find(match);

        public new void ForEach(Action<T> action)
        {
            value.ForEach(action);
        }
    }
}