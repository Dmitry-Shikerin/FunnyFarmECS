using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Dogs.Controllers;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.EcsBoundedContexts.Animals.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;
using VectorVisualizer;

namespace Sources.BoundedContexts.Dogs.Presentation
{
    public class DogHouseView : PresentableView<DogPresenter>, ISelectableItem
    {
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        [Required] [SerializeField] private UIButton _selectButton;
        [VisualizableVector]
        [SerializeField] private List<Vector3> _points = new List<Vector3>();
        [field: SerializeField] public AnimalView AnimalView { get; private set; }

        public HighlightEffect HighlightEffect => _highlightEffect;
        public UIButton SelectButton => _selectButton;
        public List<Vector3> Points => _points;

        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}