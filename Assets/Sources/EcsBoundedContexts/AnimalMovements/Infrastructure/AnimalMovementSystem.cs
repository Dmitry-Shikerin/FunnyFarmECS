using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalMovementSystem : IProtoRunSystem
    {
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<
                AnimalTypeComponent, 
                AnimancerComponent, 
                AnimalStateComponent, 
                MovementPointComponent,
                NavMeshComponent,
                TransformComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _animalIt)
            {
                ref AnimalStateComponent state = ref _aspect.AnimalStatePool.Get(entity);

                if (state.AnimalState is AnimalState.Walk or AnimalState.Run)
                {
                    ref MovementPointComponent target = ref _aspect.MovementPointsPool.Get(entity);
                    ref NavMeshComponent agent = ref _aspect.NavMeshPool.Get(entity);
                    TransformComponent transform = _aspect.TransformPool.Get(entity);
                    agent.Agent.SetDestination(target.TargetPoint);

                    if (Vector3.Distance(target.TargetPoint, transform.Transform.position) > 2f)
                        continue;
                    
                    state.AnimalState = AnimalState.ChangeState;
                }
            }
        }
    }
}