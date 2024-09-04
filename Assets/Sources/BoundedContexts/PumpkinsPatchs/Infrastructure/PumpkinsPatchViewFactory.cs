using System;
using Sources.BoundedContexts.PumpkinsPatchs.Controllers;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.PumpkinsPatchs.Infrastructure
{
    public class PumpkinsPatchViewFactory
    {
        private readonly IEntityRepository _entityRepository;

        public PumpkinsPatchViewFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
        }

        public PumpkinPatchView Create(string id, PumpkinPatchView view)
        { 
            PumpkinsPatchPresenter presenter = new PumpkinsPatchPresenter(id, view, _entityRepository);
            view.Construct(presenter);

            return view;
        }
    }
}