using System;
using JetBrains.Annotations;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.Trucks.Controllers;
using Sources.BoundedContexts.Trucks.Presentation;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.Trucks.Infrastructure
{
    public class TruckViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        public TruckViewFactory(
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public TruckView Create(string id, TruckView view)
        {
            TruckPresenter presenter = new TruckPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            
            return view;
        }
    }
}