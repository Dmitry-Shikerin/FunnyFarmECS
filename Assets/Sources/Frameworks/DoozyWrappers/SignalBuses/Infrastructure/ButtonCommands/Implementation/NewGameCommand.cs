using System;
using Doozy.Runtime.Signals;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Domain;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation
{
    public class NewGameCommand : IButtonCommand
    {
        private readonly IStorageService _storageService;
        private readonly ISceneService _sceneService;

        public NewGameCommand(
            IStorageService storageService,
            ISceneService sceneService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
        }

        public ButtonCommandId Id => ButtonCommandId.NewGame;

        public void Handle()
        {
            if (_storageService.HasKey(ModelId.PlayerWallet))
            {
                Signal.Send(StreamId.MainMenu.NewGame, true);

                return;
            }
            
            _sceneService.ChangeSceneAsync(ModelId.Gameplay);
        }
    }
}