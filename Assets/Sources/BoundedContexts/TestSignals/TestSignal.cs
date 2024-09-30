using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;

namespace Sources.BoundedContexts.TestSignals
{
    public struct TestSignal : ISignal
    {
        public TestSignal(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}