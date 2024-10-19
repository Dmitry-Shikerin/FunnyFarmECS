using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.UiSelectables.Controllers;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.UiSelectables.Presentation
{
    public class UiSelectableView : PresentableView<UiSelectablePresenter>
    {
        [Required] [SerializeField] private UIButton _selectButton;
        
        public UIButton SelectButton => _selectButton;
    }
}