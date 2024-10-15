using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Domain;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation
{
    public class SaveVolumeButtonCommand : IButtonCommand
    {
        private readonly IStorageService _storageService;

        public SaveVolumeButtonCommand(IStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public ButtonCommandId Id => ButtonCommandId.SaveVolume;
        
        public void Handle()
        {
            _storageService.Save(ModelId.SoundsVolume);
            _storageService.Save(ModelId.MusicVolume);
        }
    }
}