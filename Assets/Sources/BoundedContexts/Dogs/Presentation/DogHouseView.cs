using System.Collections.Generic;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.AnimalMovePoints;
using Sources.BoundedContexts.Dogs.Controllers;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.EcsBoundedContexts.Animals.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Dogs.Presentation
{
    public class DogHouseView : PresentableView<DogPresenter>, ISelectableItem
    {
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        [SerializeField] private List<AnimalMovePoint> _points;
        [field: SerializeField] public AnimalView AnimalView { get; private set; }

        public HighlightEffect HighlightEffect => _highlightEffect;
        public IReadOnlyList<AnimalMovePoint> Points => _points;

        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}