using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.UiFramework.Views.Domain;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation
{
    public class SaveVolumeCommand : IViewCommand
    {
        private readonly IStorageService _storageService;

        public SaveVolumeCommand(IStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public FormCommandId Id => FormCommandId.SaveVolume;
        
        public void Handle()
        {
            _storageService.Save(ModelId.MusicVolume);
            _storageService.Save(ModelId.SoundsVolume);
        }
    }
}