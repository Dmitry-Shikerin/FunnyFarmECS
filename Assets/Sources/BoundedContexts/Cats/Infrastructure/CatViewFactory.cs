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

        public CatHouseView Create(string id, CatHouseView houseView)
        {
            CatHousePresenter housePresenter = new CatHousePresenter(
                id, houseView, _entityRepository, _cameraService, _selectableService);
            houseView.Construct(housePresenter);
            
            return houseView;
        }

    }
}