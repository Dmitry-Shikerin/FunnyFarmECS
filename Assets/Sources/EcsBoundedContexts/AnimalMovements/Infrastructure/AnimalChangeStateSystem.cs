using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.MyLeoEcsProto.States.Controllers;
using Sources.MyLeoEcsProto.States.Controllers.Transitions;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalChangeStateSystem : StateSystem<AnimalState, AnimalStateComponent>
    {
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<
                AnimalTypeComponent, 
                AnimancerComponent, 
                AnimalStateComponent, 
                MovementPointComponent,
                NavMeshComponent>());

        protected override ProtoIt ProtoIt => _animalIt;
        protected override ProtoPool<AnimalStateComponent> Pool => _aspect.AnimalState;

        public override void Init(IProtoSystems systems) =>
            AddTransition(ToRandomStateTransition());

        protected override bool IsState(ProtoEntity entity) =>
            _aspect.AnimalState.Get(entity).CurrentState == AnimalState.ChangeState;

        protected override void Update(ProtoEntity entity)
        {
        }

        private MutableStateTransition<AnimalState> ToRandomStateTransition() =>
            new(GetRandomState, _ => true);

        private AnimalState GetRandomState()
        {
            return Random.Range(0, 100) switch
            {
                < 33 => AnimalState.Walk,
                > 33 and < 66 => AnimalState.Run,
                _ => AnimalState.Idle,
            };
        }
    }
}