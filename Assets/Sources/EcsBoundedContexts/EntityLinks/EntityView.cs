using System;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EntityLinks
{
    [RequireComponent(typeof(EntityLink))]
    public abstract class EntityView : View
    {
        [SerializeField] private bool _canDestroyAfterInitialize;
        
        public EntityLink EntityLink { get; private set; }

        private void Awake()
        {
            EntityLink = GetComponent<EntityLink>() ?? throw new ArgumentNullException(nameof(EntityLinks.EntityLink));
        }
    }
}