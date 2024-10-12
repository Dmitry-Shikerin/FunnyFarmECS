using System;
using JetBrains.Annotations;
using MyDependencies.Sources.Containers;
using Sources.BoundedContexts.ChikenCorrals.Controllers;
using Sources.BoundedContexts.ChikenCorrals.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.ChikenCorrals.Infrastructure
{
    public class ChickenCorralViewFactory
    {
        private readonly ICameraService _cameraService;
        private readonly IEntityRepository _entityRepository;
        private readonly DiContainer _container;
        private readonly ISelectableService _selectableService;

        public ChickenCorralViewFactory(
            ICameraService cameraService,
            IEntityRepository entityRepository,
            DiContainer container,
            ISelectableService selectableService)
        {
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public ChickenCorralView Create(string id, ChickenCorralView view)
        {
            ChickenCorralPresenter presenter = new ChickenCorralPresenter(
                id, view, _entityRepository, _cameraService, _selectableService);
            view.Construct(presenter);

            return view;
        }
    }
}