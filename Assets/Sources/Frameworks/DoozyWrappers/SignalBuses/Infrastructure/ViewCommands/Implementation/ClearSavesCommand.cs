using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Ids;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation
{
    public class ClearSavesCommand : IViewCommand
    {
        private readonly IStorageService _storageService;

        public ClearSavesCommand(IStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public ViewCommand Id => ViewCommand.ClearSaves;
        
        public void Handle()
        {
            _storageService.Clear(ModelId.GetDeleteIds());
        }
    }
}