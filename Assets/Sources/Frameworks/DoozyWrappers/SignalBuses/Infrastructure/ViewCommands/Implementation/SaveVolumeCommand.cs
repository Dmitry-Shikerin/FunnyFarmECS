using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Ids;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation
{
    public class SaveVolumeCommand : IViewCommand
    {
        private readonly IStorageService _storageService;

        public SaveVolumeCommand(IStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public ViewCommand Id => ViewCommand.SaveVolume;
        
        public void Handle()
        {
            _storageService.Save(ModelId.MusicVolume);
            _storageService.Save(ModelId.SoundsVolume);
        }
    }
}