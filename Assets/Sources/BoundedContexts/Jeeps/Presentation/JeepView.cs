using Doozy.Runtime.UIManager.Components;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Jeeps.Controllers;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Jeeps.Presentation
{
    public class JeepView : PresentableView<JeepPresenter>, ISelectableItem
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