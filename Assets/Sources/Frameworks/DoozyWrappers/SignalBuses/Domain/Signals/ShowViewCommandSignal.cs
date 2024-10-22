using System.Collections.Generic;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Ids;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Signals
{
    public struct ShowViewCommandSignal
    {
        public ShowViewCommandSignal(IEnumerable<ViewCommand> showCommands)
        {
            ShowCommands = showCommands;
        }

        public IEnumerable<ViewCommand> ShowCommands { get; }
    }
}