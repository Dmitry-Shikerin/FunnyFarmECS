using System.Collections.Generic;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Cameras.Presentation;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.BoundedContexts.SheepPens.Controllers;
using Sources.EcsBoundedContexts.Animals.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.UI.Images;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.SheepPens.Presentation
{
    public class SheepPenView : PresentableView<SheepPenPresenter>, ISelectableItem
    {
        [SerializeField] private List<ItemView> _pumpkins;
        [Required] [SerializeField] private LookAtCamera _lookAtCamera;
        [Required] [SerializeField] private ImageView _progressBarr;
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        [SerializeField] private List<AnimalView> _ships;
        
        public IReadOnlyList<AnimalView> Ships => _ships;
        public IReadOnlyList<ItemView> Pumpkins => _pumpkins;
        public LookAtCamera LookAtCamera => _lookAtCamera;
        public ImageView ProgressBarr => _progressBarr;
        public HighlightEffect HighlightEffect => _highlightEffect;
        
        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}