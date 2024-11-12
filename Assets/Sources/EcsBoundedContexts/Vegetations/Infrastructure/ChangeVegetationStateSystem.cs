using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Vegetations.Domain;
using Sources.EcsBoundedContexts.Vegetations.Domain.Events;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Vegetations.Infrastructure
{
    public class ChangeVegetationStateSystem : EventSystem<ChangeVegetationStateEvent>
    {
        [DI] private readonly MainAspect _aspect;
        [DI] private readonly ProtoIt _it = 
            new (It.Inc<ChangeVegetationStateEvent>());

        [DI] private readonly ProtoIt _vegetationIt =
            new(It.Inc<VegetationEnumStateComponent>());

        protected override ProtoPool<ChangeVegetationStateEvent> Pool => _aspect.ChangeVegetationState;
        protected override ProtoIt Iterator => _it;
        
        protected override void Receive(ref ChangeVegetationStateEvent @event)
        {
            foreach (ProtoEntity entity in _vegetationIt)
            {
                ref VegetationEnumStateComponent vegetationState = ref _aspect.VegetationState.Get(entity);

                if (vegetationState.Type != @event.Type)
                    continue;
                
                vegetationState.State = @event.State;
            }
        }
    }
}