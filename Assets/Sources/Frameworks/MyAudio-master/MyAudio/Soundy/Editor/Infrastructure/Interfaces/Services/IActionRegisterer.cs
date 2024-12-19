using System;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public interface IActionRegisterer<T>
    {
        void UnRegister(Action<T> action);
        void Register(Action<T> action);
        void UnregisterAll();
    }
}