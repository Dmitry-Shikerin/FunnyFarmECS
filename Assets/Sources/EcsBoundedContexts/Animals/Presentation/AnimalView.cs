using Animancer;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.EntityLinks;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.EcsBoundedContexts.Animals.Presentation
{
    public class AnimalView : EntityView
    {
        [field: SerializeField] public AnimalType AnimalType { get; private set; }
        [field: SerializeField] public AnimancerComponent Animancer { get; private set; }
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    }
}