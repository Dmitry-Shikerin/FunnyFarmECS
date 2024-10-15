using System;
using Doozy.Runtime.Signals;
using Sources.BoundedContexts.Tutorials.Domain.Models;
using Sources.BoundedContexts.Tutorials.Services.Interfaces;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Constants;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.Tutorials.Services.Implementation
{
    public class TutorialService : ITutorialService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;
        
        private Tutorial _tutorial;
        private SignalStream _stream;
        private Pause _pause;

        public TutorialService(
            IEntityRepository entityRepository,
            IStorageService storageService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public void Initialize()
        {
            _tutorial = _entityRepository.Get<Tutorial>(ModelId.Tutorial);
            _pause = _entityRepository.Get<Pause>(ModelId.Pause);
            _stream = SignalStream.Get(StreamConst.Gameplay, StreamConst.ShowTutorial);
            
            if (_tutorial.HasCompleted)
                return;
            
            _stream.SendSignal(true);
            _pause.PauseGame();
        }

        public void Complete()
        {
            _tutorial.HasCompleted = true;
            _storageService.Save(_tutorial);
        }
    }
}