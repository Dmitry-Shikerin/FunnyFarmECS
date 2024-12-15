using System.Collections.Generic;
using Sources.EcsBoundedContexts.EntityLinks;
using UnityEngine;

namespace Sources.EcsBoundedContexts.WaterTractors.Presentation
{
    public class WaterTractorView : EntityView
    {
        [field: SerializeField] public List<Transform> ToFieldPoints { get; private set; }
        [field: SerializeField] public List<Transform> ToHomePoints { get; private set; }
    }
}