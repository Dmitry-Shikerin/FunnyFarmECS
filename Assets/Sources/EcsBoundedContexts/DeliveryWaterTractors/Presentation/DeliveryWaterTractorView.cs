using Sources.BoundedContexts.Paths.Domain;
using Sources.EcsBoundedContexts.EntityLinks;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Presentation
{
    public class DeliveryWaterTractorView : EntityView
    {
        [field: SerializeField] public PathOwnerType PathOwnerType { get; private set; }
    }
}