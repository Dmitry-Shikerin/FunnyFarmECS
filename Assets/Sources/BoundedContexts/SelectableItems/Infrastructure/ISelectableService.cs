using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.BoundedContexts.SelectableItems.Infrastructure
{
    public interface ISelectableService : IInitialize, IDestroy
    {
        void Add(ISelectableItem item);
        void Select(ISelectableItem item);
    }
}