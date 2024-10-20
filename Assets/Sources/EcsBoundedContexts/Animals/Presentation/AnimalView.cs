using System;
using Animancer;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.EntityLinks;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Sources.EcsBoundedContexts.Animals.Presentation
{
    [RequireComponent(typeof(EntityLink))]
    public class AnimalView : View
    {
        [field: SerializeField] public AnimalType AnimalType { get; private set; }
        [field: SerializeField] public AnimancerComponent Animancer { get; private set; }
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        
        public EntityLink EntityLink { get; private set; }

        private void Awake()
        {
            EntityLink = GetComponent<EntityLink>() ?? throw new ArgumentNullException(nameof(EntityLinks.EntityLink));
        }
    }
}