using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Ids;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces
{
    public interface IViewCommand
    {
        ViewCommand Id { get; }

        void Handle();
    }
}