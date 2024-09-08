using Doozy.Runtime.UIManager.Components;
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
        [Required] [SerializeField] private UIButton _selectButton;

        public HighlightEffect HighlightEffect => _highlightEffect;
        public UIButton SelectButton => _selectButton;

        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();

    }
}