using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.BoundedContexts.TomatoPatchs.Controllers;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;

namespace Sources.BoundedContexts.TomatoPatchs.Presentation
{
    public class TomatoPatchView : PresentableView<TomatoPatchPresenter>, ISelectableItem
    {
        public void Select()
        {
            
        }

        public void Deselect()
        {
        }
    }
}