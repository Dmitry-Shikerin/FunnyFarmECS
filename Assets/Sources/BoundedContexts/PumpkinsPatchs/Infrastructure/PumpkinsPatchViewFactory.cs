using System;
using JetBrains.Annotations;
using Sources.BoundedContexts.PumpkinsPatchs.Controllers;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Zenject;

namespace Sources.BoundedContexts.PumpkinsPatchs.Infrastructure
{
    public class PumpkinsPatchViewFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly DiContainer _container;

        public PumpkinsPatchViewFactory(
            IEntityRepository entityRepository,
            DiContainer container)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public PumpkinPatchView Create(string id, PumpkinPatchView view)
        { 
            PumpkinsPatchPresenter presenter = new PumpkinsPatchPresenter(id, view, _entityRepository);
            _container.Inject(view.LookAtCamera);
            view.Construct(presenter);

            return view;
        }
    }
}