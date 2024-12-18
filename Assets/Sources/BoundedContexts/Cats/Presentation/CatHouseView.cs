using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Cats.Controllers;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.EcsBoundedContexts.Animals.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Cats.Presentation
{
    public class CatHouseView : PresentableView<CatHousePresenter>, ISelectableItem
    {
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        [Required] [SerializeField] private AnimalView _animalView;

        public AnimalView AnimalView => _animalView;
        public HighlightEffect HighlightEffect => _highlightEffect;

        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}