using System;
using Sources.BoundedContexts.Cats.Controllers;
using Sources.BoundedContexts.Cats.Presentation;
using Sources.BoundedContexts.Jeeps.Controllers;
using Sources.BoundedContexts.Jeeps.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.Cats.Infrastructure
{
    public class CatViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        public CatViewFactory(
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public CatView Create(string id, CatView view)
        {
            CatPresenter presenter = new CatPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            
            return view;
        }

    }
}