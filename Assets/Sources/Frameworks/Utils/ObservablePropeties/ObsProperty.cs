using System;
using System.Collections.Generic;

namespace Sources.Frameworks.Utils.ObservablePropeties
{
    public struct ObsProperty<T>
    {
        private readonly List<Action> _actions;
        
        private T _value;

        public ObsProperty(T value = default)
        {
            _value = value;
            _actions = new List<Action>();
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Invoke();
            }
        }

        public void Subscribe(Action action)
        {
            if (_actions.Contains(action))
                throw new InvalidOperationException();

            _actions.Add(action);
        }

        public void Unsubscribe(Action action)
        {
            if (_actions.Contains(action) == false)
                throw new InvalidOperationException();

            _actions.Remove(action);
        }

        private void Invoke()
        {
            for (int i = _actions.Count; i > 0; i--)
                _actions[i].Invoke();
        }
    }
}