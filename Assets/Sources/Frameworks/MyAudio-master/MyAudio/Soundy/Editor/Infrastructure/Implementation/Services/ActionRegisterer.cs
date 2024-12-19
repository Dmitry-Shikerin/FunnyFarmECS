using System;
using System.Collections.Generic;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public class ActionRegisterer<T> : IActionRegisterer<T>
    {
        protected readonly List<Action<T>> Actions = new();

        public void UnRegister(Action<T> action) =>
            Actions.Remove(action);

        public void Register(Action<T> action) =>
            Actions.Add(action);

        public void UnregisterAll() =>
            Actions.Clear();
    }
}