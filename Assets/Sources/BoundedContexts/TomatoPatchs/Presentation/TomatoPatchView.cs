using System.Collections.Generic;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Cameras.Presentation;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.BoundedContexts.TomatoPatchs.Controllers;
using Sources.EcsBoundedContexts.Vegetations.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.UI.Images;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.TomatoPatchs.Presentation
{
    public class TomatoPatchView : PresentableView<TomatoPatchPresenter>, ISelectableItem
    {
        [field: SerializeField] public List<VegetationView> Pumpkins { get; private set; }
        [Required] [SerializeField] private LookAtCamera _lookAtCamera;
        [Required] [SerializeField] private ImageView _progressBarr;
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        
        public LookAtCamera LookAtCamera => _lookAtCamera;
        public ImageView ProgressBarr => _progressBarr;
        public HighlightEffect HighlightEffect => _highlightEffect;
        
        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}