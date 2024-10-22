using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Ids;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces.Handlers
{
    public interface IViewCommandHandler
    {
        void Handle(ViewCommand viewCommand);
    }
}