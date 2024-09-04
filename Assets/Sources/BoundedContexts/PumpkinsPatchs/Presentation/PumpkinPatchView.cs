using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Cameras.Presentation;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.PumpkinsPatchs.Controllers;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.UI.Images;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.PumpkinsPatchs.Presentation
{
    public class PumpkinPatchView : PresentableView<PumpkinsPatchPresenter>
    {
        [SerializeField] private List<ItemView> _pumpkins;
        [Required] [SerializeField] private LookAtCamera _lookAtCamera;
        [Required] [SerializeField] private UIButton _sowButton;
        [Required] [SerializeField] private ImageView _progressBarr;
        
        public IReadOnlyList<ItemView> Pumpkins => _pumpkins;
        public LookAtCamera LookAtCamera => _lookAtCamera;
        public UIButton SowButton => _sowButton; 
        public ImageView ProgressBarr => _progressBarr;
    }
}