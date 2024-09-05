using System;
using Sources.BoundedContexts.PumpkinsPatchs.Controllers;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Zenject;

namespace Sources.BoundedContexts.PumpkinsPatchs.Infrastructure
{
    public class PumpkinsPatchViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly DiContainer _container;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        public PumpkinsPatchViewFactory(
            IEntityRepository entityRepository,
            DiContainer container,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public PumpkinPatchView Create(string id, PumpkinPatchView view)
        { 
            PumpkinsPatchPresenter presenter = new PumpkinsPatchPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            _container.Inject(view.LookAtCamera);
            view.Construct(presenter);

            return view;
        }
    }
}