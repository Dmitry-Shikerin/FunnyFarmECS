using System;
using JetBrains.Annotations;
using Sources.BoundedContexts.Jeeps.Controllers;
using Sources.BoundedContexts.Jeeps.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.Jeeps.Infrastructure
{
    public class JeepViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        public JeepViewFactory(
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public JeepView Create(string id, JeepView view)
        {
            JeepPresenter presenter = new JeepPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);
            return view;
        }
    }
}