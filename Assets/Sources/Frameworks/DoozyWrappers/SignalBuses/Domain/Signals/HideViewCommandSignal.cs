using System.Collections.Generic;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Ids;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Signals
{
    public struct HideViewCommandSignal
    {
        public HideViewCommandSignal(IEnumerable<ViewCommand> hideCommands)
        {
            HideCommands = hideCommands;
        }

        public IEnumerable<ViewCommand> HideCommands { get; }
    }
}