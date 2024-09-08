using System;
using Sources.BoundedContexts.CowPens.Controllers;
using Sources.BoundedContexts.CowPens.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.TomatoPatchs.Controllers;
using Sources.BoundedContexts.TomatoPatchs.Presentation;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Zenject;

namespace Sources.BoundedContexts.CowPens.Infrastructure
{
    public class CowPenViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly DiContainer _container;
        private readonly ISelectableService _selectableService;

        public CowPenViewFactory(
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

        public CowPenView Create(string id, CowPenView view)
        {
            CowPenPresenter presenter = new CowPenPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            _container.Inject(view.LookAtCamera);

            return view;
        }
    }
}