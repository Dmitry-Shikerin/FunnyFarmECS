using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Domain;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation
{
    public class ClearSavesButtonCommand : IButtonCommand
    {
        private readonly IStorageService _storageService;

        public ClearSavesButtonCommand(IStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public ButtonCommandId Id => ButtonCommandId.ClearSaves;

        public void Handle() =>
            _storageService.ClearAll();
    }
}