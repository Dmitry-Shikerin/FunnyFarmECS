using HighlightPlus;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.BoundedContexts.Trucks.Controllers;
using Sources.EcsBoundedContexts.DeliveryCars.Presentation;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Trucks.Presentation
{
    public class TruckView : PresentableView<TruckPresenter>, ISelectableItem
    {
        [Required] [SerializeField] private HighlightEffect _highlightEffect;
        [field: SerializeField] public DeliveryCarView DeliveryCarView { get; private set; }

        public HighlightEffect HighlightEffect => _highlightEffect;

        public void Select() =>
            Presenter.Select();

        public void Deselect() =>
            Presenter.Deselect();
    }
}