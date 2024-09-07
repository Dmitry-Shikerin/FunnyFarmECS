using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.CabbagePatches.Controllers;
using Sources.BoundedContexts.Cameras.Presentation;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.UI.Images;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.BoundedContexts.CabbagePatches.Presentation
{
    public class CabbagePatchView : PresentableView<CabbagePatchPresenter>, ISelectableItem
    {
        [SerializeField] private List<ItemView> _cabbages;
        [Required] [SerializeField] private LookAtCamera _lookAtCamera;
        [Required] [SerializeField] private UIButton _sowButton;
        [Required] [SerializeField] private UIButton _harvestButton;
        [Required] [SerializeField] private UIButton _selectableButton;
        [Required] [SerializeField] private ImageView _progressBarr;
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        
        public IReadOnlyList<ItemView> Cabbages => _cabbages;
        public LookAtCamera LookAtCamera => _lookAtCamera;
        public UIButton SowButton => _sowButton; 
        public UIButton HarvestButton => _harvestButton;
        public UIButton SelectableButton => _selectableButton;
        public ImageView ProgressBarr => _progressBarr;
        public HighlightEffect HighlightEffect => _highlightEffect;
        
        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}