using Sources.EcsBoundedContexts.EntityLinks;
using Sources.EcsBoundedContexts.Vegetations.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Vegetations.Presentation
{
    public class VegetationView : EntityView
    {
        [field: SerializeField] public VegetationState State { get; private set; }
        [field: SerializeField] public VegetationType Type { get; private set; }
    }
}