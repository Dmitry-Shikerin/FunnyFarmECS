using System.Collections.Generic;
using Sources.EcsBoundedContexts.EntityLinks;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Harvesters.Presentation
{
    public class HarvesterView : EntityView
    {
        [field: SerializeField] public List<Transform> ToFieldPoints { get; private set; }
        [field: SerializeField] public List<Transform> ToHomePoints { get; private set; }
    }
}