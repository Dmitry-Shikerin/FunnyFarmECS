using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalIdleSystem : IProtoRunSystem
    {
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<
                AnimalTypeComponent, 
                AnimancerComponent, 
                AnimalStateComponent, 
                MovementPointComponent,
                NavMeshComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _animalIt)
            {
                ref AnimalStateComponent state = ref _aspect.AnimalStatePool.Get(entity);

                if (state.CurrentState != AnimalState.Idle)
                    return;
                
                state.CurentIdleTime += Time.deltaTime;

                if (state.CurentIdleTime < state.TargetIdleTime)
                    continue;
                
                state.CurrentState = AnimalState.ChangeState;
            }
        }
    }
}