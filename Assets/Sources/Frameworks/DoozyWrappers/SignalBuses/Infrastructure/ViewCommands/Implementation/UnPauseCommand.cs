using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.UiFramework.Views.Domain;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation
{
    public class UnPauseCommand : IViewCommand
    {
        private readonly IEntityRepository _entityRepository;

        public UnPauseCommand(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
        }

        public FormCommandId Id => FormCommandId.UnPause;
        
        public void Handle()
        {
            var pause = _entityRepository.Get<Pause>(ModelId.Pause);
            
            if (pause.IsPaused == false)
                return;
            
            pause.ContinueGame();
        }
    }
}