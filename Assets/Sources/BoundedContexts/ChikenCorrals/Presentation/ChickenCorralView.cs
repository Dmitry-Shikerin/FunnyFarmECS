using System.Collections.Generic;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.AnimalMovePoints;
using Sources.BoundedContexts.Cameras.Presentation;
using Sources.BoundedContexts.ChikenCorrals.Controllers;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.EcsBoundedContexts.Animals.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.UI.Images;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.ChikenCorrals.Presentation
{
    public class ChickenCorralView : PresentableView<ChickenCorralPresenter>, ISelectableItem
    {
        [SerializeField] private List<AnimalView> _chickens;
        [Required] [SerializeField] private LookAtCamera _lookAtCamera;
        [Required] [SerializeField] private ImageView _progressBarr;
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        
        public IReadOnlyList<AnimalView> Chickens => _chickens;
        public LookAtCamera LookAtCamera => _lookAtCamera;
        public ImageView ProgressBarr => _progressBarr;
        public HighlightEffect HighlightEffect => _highlightEffect;
        
        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}