using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EntityLinks
{
    public class EntityLink : View
    {
        [SerializeField] private int _entityId;
        
        public ProtoEntity Entity { get; private set; }
        public MainAspect MainAspect { get; private set; }
        public ProtoWorld World { get; private set; }

        public void Construct(ProtoEntity entity, MainAspect aspect, ProtoWorld world)
        {
            Entity = entity;
            _entityId = entity.GetHashCode();
            MainAspect = aspect;
            World = world;
        }
    }
}