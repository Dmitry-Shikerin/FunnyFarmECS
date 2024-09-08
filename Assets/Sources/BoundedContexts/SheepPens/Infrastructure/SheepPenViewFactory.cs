using System;
using Sources.BoundedContexts.RabbitPens.Controllers;
using Sources.BoundedContexts.RabbitPens.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.SheepPens.Controllers;
using Sources.BoundedContexts.SheepPens.Presentation;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Zenject;

namespace Sources.BoundedContexts.SheepPens.Infrastructure
{
    public class SheepPenViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly DiContainer _container;
        private readonly ISelectableService _selectableService;

        public SheepPenViewFactory(
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

        public SheepPenView Create(string id, SheepPenView view)
        {
            SheepPenPresenter presenter = new SheepPenPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            _container.Inject(view.LookAtCamera);

            return view;
        }
    }
}