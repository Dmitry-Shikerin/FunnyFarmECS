using System.Collections.Generic;
using Animancer;
using Sources.EcsBoundedContexts.EntityLinks;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.EcsBoundedContexts.Farmers.Presentation
{
    public class FarmerView : EntityView
    {
        [field: SerializeField] public List<FarmerMovePointView> MovePoints { get; private set; }
        [field: SerializeField] public AnimancerComponent Animancer { get; private set; }
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    }
}