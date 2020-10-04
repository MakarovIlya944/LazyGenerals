using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LazyGenerals.Client.Utility {
    public class NotifyPropertyField<T> : INotifyPropertyChanged, INotifyPropertyChanging, IDisposable
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                var oldValue = this.value;
                OnPropertyChanging(oldValue, value, nameof(Value));
                this.value = value;
                OnPropertyChanged(oldValue, value, nameof(Value));
            }
        }

        public NotifyPropertyField(T value)
        {
            Value = value;
        }

        public NotifyPropertyField() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanging(T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            if (PropertyChanged == null)
            {
                return;
            }

            foreach (var d in PropertyChanged.GetInvocationList())
            {
                PropertyChanged -= (PropertyChangedEventHandler)d;
            }
        }
    }
}