using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Ids;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation
{
    public class UnPauseCommand : IViewCommand
    {
        private readonly IEntityRepository _entityRepository;

        public UnPauseCommand(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
        }

        public ViewCommand Id => ViewCommand.UnPause;
        
        public void Handle()
        {
            var pause = _entityRepository.Get<Pause>(ModelId.Pause);
            
            if (pause.IsPaused == false)
                return;
            
            pause.ContinueGame();
        }
    }
}