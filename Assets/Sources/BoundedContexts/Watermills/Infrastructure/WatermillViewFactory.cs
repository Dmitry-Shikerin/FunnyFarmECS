using System;
using Sources.BoundedContexts.Houses.Controllers;
using Sources.BoundedContexts.Houses.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.Watermills.Controllers;
using Sources.BoundedContexts.Watermills.Presentation;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.Watermills.Infrastructure
{
    public class WatermillViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        public WatermillViewFactory(
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public WatermillView Create(string id, WatermillView view)
        {
            WatermillPresenter presenter = new WatermillPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            return view;
        }
    }
}