using System.Collections.Generic;
using Sources.EcsBoundedContexts.EntityLinks;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryCars.Presentation
{
    public class DeliveryCarView : EntityView
    {
        [field: SerializeField] public List<Transform> MovePoints { get; private set; }
    }
}