using System;

namespace Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces.Generic
{
    public interface ISignalStream<T> : ISignalStream
        where T : struct, ISignal
    {
        void Handle(T signal);
        void Subscribe(Action<T> signalHandler);
        void Unsubscribe(Action<T> signalHandler);
    }
}