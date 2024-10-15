using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Domain;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation
{
    public class UnPauseButtonCommand : IButtonCommand
    {
        private readonly IEntityRepository _entityRepository;

        public UnPauseButtonCommand(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
        }

        public ButtonCommandId Id => ButtonCommandId.UnPause;

        public void Handle()
        {
            Pause pause = _entityRepository.Get<Pause>(ModelId.Pause);
            
            if (pause.IsPaused == false)
                return;

            pause.ContinueGame();
        }
    }
}