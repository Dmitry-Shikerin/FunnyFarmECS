using Sources.BoundedContexts.Paths.Domain;
using Sources.EcsBoundedContexts.EntityLinks;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryCars.Presentation
{
    public class DeliveryCarView : EntityView
    {
        [field: SerializeField] public PathOwnerType PathOwnerType { get; private set; }
    }
}