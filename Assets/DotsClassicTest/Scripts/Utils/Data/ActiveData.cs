using System;


namespace DotsClassicTest.Utils.Data
{

    public class ActiveData<T>
    {
        public event Action<T> UpdateEvent;

        private T value;

        public ActiveData(T defaultValue = default(T))
        {
            value = defaultValue;
        }

        public virtual T Value
        {
            get => value;
            set
            {
                if (this.value != null && !this.value.Equals(value))
                {
                    this.value = value;
                    UpdateEvent.Call(this.value);
                }
                else if(this.value == null && value != null)
                {
                    this.value = value;
                    UpdateEvent.Call(this.value);
                }
            }
        }
    }
}
