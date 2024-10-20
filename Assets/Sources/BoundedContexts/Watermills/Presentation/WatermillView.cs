using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.BoundedContexts.Watermills.Controllers;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Watermills.Presentation
{
    public class WatermillView : PresentableView<WatermillPresenter>, ISelectableItem
    {
        [Required] [SerializeField] private HighlightEffect _highlightEffect;

        public HighlightEffect HighlightEffect => _highlightEffect;

        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();

    }
}