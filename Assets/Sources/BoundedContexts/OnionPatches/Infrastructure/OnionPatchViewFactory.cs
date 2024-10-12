using System;
using MyDependencies.Sources.Containers;
using Sources.BoundedContexts.CabbagePatches.Controllers;
using Sources.BoundedContexts.CabbagePatches.Presentation;
using Sources.BoundedContexts.OnionPatches.Controllers;
using Sources.BoundedContexts.OnionPatches.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.OnionPatches.Infrastructure
{
    public class OnionPatchViewFactory
    {
        private readonly ICameraService _cameraService;
        private readonly IEntityRepository _entityRepository;
        private readonly DiContainer _container;
        private readonly ISelectableService _selectableService;

        public OnionPatchViewFactory(
            ICameraService cameraService,
            IEntityRepository entityRepository,
            DiContainer container,
            ISelectableService selectableService)
        {
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }
        
        public OnionPatchView Create(string id, OnionPatchView view)
        {
            OnionPatchPresenter presenter = new OnionPatchPresenter(
                id, _entityRepository, view, _cameraService, _selectableService);
            view.Construct(presenter);
            
            return view;
        }
    }
}