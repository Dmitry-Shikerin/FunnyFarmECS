using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Cameras.Presentation;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.RabbitPens.Controllers;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.UI.Images;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.RabbitPens.Presentation
{
    public class RabbitPenView : PresentableView<RabbitPenPresenter>, ISelectableItem
    {
        [SerializeField] private List<ItemView> _pumpkins;
        [Required] [SerializeField] private LookAtCamera _lookAtCamera;
        [Required] [SerializeField] private UIButton _sowButton;
        [Required] [SerializeField] private UIButton _harvestButton;
        [Required] [SerializeField] private UIButton _selectableButton;
        [Required] [SerializeField] private ImageView _progressBarr;
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        
        public IReadOnlyList<ItemView> Pumpkins => _pumpkins;
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