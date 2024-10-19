using System;
using Sources.BoundedContexts.UiSelectables.Controllers;
using Sources.BoundedContexts.UiSelectables.Presentation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.UiSelectables.Infrastructure
{
    public class UiSelectableViewFactory
    {
        private readonly IEntityRepository _entityRepository;

        public UiSelectableViewFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
        }

        public UiSelectableView Create(string id, UiSelectableView view)
        {
            UiSelectablePresenter presenter = new UiSelectablePresenter(id, view, _entityRepository);
            view.Construct(presenter);
            
            return view;
        }
    }
}