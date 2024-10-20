using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Cameras.Presentation;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.PumpkinsPatchs.Controllers;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.UI.Images;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.PumpkinsPatchs.Presentation
{
    public class PumpkinPatchView : PresentableView<PumpkinsPatchPresenter>, ISelectableItem
    {
        [SerializeField] private List<ItemView> _pumpkins;
        [Required] [SerializeField] private LookAtCamera _lookAtCamera;
        [Required] [SerializeField] private ImageView _progressBarr;
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        
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