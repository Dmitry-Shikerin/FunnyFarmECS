using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Houses.Controllers;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.EcsBoundedContexts.Farmers.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Houses.Presentation
{
    public class HouseView : PresentableView<HousePresenter>, ISelectableItem
    {
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        [field: SerializeField] public FarmerView Farmer { get; private set; }

        public HighlightEffect HighlightEffect => _highlightEffect;

        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}