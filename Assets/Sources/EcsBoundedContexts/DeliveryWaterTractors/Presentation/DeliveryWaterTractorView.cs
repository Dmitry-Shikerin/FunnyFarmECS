using System.Collections.Generic;
using Sources.EcsBoundedContexts.EntityLinks;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Presentation
{
    public class DeliveryWaterTractorView : EntityView
    {
        [field: SerializeField] public List<Transform> MovePoints { get; private set; }
    }
}