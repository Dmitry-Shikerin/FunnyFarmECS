using System;
using Sources.BoundedContexts.Dogs.Controllers;
using Sources.BoundedContexts.Dogs.Presentation;
using Sources.BoundedContexts.Jeeps.Controllers;
using Sources.BoundedContexts.Jeeps.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.Dogs.Infrastructure
{
    public class DogViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        public DogViewFactory(
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public DogView Create(string id, DogView view)
        {
            DogPresenter presenter = new DogPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            
            return view;
        }
    }
}