using System;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Implementation;

namespace Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces
{
    public interface ISignalBus
    {
        void Handle<T>(T signal) 
            where T : struct, ISignal;
        SignalStream<T> GetStream<T>()
            where T : struct, ISignal;
        void Subscribe<T>(Action<T> signalHandler)
            where T : struct, ISignal;
        void Unsubscribe<T>(Action<T> signalHandler)
            where T : struct, ISignal;
        void Release();
    }
}