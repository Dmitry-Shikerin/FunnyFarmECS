using System;
using Sources.BoundedContexts.UiSelectables.Domain;
using Sources.BoundedContexts.UiSelectables.Presentation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;

namespace Sources.BoundedContexts.UiSelectables.Controllers
{
    public class UiSelectablePresenter : PresenterBase
    {
        private readonly UiSelectableView _view;
        private readonly ISelectable _selectable;

        public UiSelectablePresenter(
            string id, 
            UiSelectableView view, 
            IEntityRepository entityRepository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _selectable = entityRepository.Get<ISelectable>(id);
        }
    }
}