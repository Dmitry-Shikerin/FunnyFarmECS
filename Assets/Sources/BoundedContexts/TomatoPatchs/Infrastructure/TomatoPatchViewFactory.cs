using System;
using MyDependencies.Sources.Containers;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.TomatoPatchs.Controllers;
using Sources.BoundedContexts.TomatoPatchs.Presentation;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.TomatoPatchs.Infrastructure
{
    public class TomatoPatchViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly DiContainer _container;
        private readonly ISelectableService _selectableService;

        public TomatoPatchViewFactory(
            IEntityRepository entityRepository,
            ICameraService cameraService,
            DiContainer container,
            ISelectableService selectableService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public TomatoPatchView Create(string id, TomatoPatchView view)
        {
            TomatoPatchPresenter presenter = new TomatoPatchPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            _container.Inject(view.LookAtCamera);

            return view;
        }
    }
}