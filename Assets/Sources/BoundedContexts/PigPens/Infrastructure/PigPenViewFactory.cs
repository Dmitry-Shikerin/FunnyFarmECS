using System;
using MyDependencies.Sources.Containers;
using Sources.BoundedContexts.PigPens.Controllers;
using Sources.BoundedContexts.PigPens.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.PigPens.Infrastructure
{
    public class PigPenViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly DiContainer _container;
        private readonly ISelectableService _selectableService;

        public PigPenViewFactory(
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

        public PigPenView Create(string id, PigPenView view)
        {
            PigPenPresenter presenter = new PigPenPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            _container.Inject(view.LookAtCamera);

            return view;
        }
    }
}