using System.Collections.Generic;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Ids;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces.Handlers;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation.Handlers
{
    public abstract class ViewCommandHandler : IViewCommandHandler
    {
        private readonly Dictionary<ViewCommand, IViewCommand> _commands = 
            new Dictionary<ViewCommand, IViewCommand>();

        protected void Add(IViewCommand viewCommand) =>
            _commands[viewCommand.Id] = viewCommand;

        public void Handle(ViewCommand viewCommand)
        {
            if(_commands.ContainsKey(viewCommand) == false)
                throw new KeyNotFoundException(nameof(viewCommand));
            
            _commands[viewCommand].Handle();
        }
    }
}