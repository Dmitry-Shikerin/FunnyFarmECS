using System;
using JetBrains.Annotations;
using Sources.BoundedContexts.ChikenCorrals.Controllers;
using Sources.BoundedContexts.ChikenCorrals.Presentation;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Zenject;

namespace Sources.BoundedContexts.ChikenCorrals.Infrastructure
{
    public class ChickenCorralViewFactory
    {
        private readonly ICameraService _cameraService;
        private readonly IEntityRepository _entityRepository;
        private readonly DiContainer _container;

        public ChickenCorralViewFactory(
            ICameraService cameraService,
            IEntityRepository entityRepository,
            DiContainer container)
        {
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public ChickenCorralView Create(string id, ChickenCorralView view)
        {
            ChickenCorralPresenter presenter = new ChickenCorralPresenter(
                id, view, _entityRepository, _cameraService);
            view.Construct(presenter);

            return view;
        }
    }
}