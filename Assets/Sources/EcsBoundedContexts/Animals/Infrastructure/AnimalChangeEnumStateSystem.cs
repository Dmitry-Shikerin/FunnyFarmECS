using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.MyLeoEcsProto.States.Controllers;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Animals.Infrastructure
{
    public class AnimalChangeEnumStateSystem : EnumStateSystem<AnimalState, AnimalEnumStateComponent>
    {
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<
                AnimalTypeComponent, 
                AnimancerEcsComponent, 
                AnimalEnumStateComponent, 
                MovementPointComponent,
                NavMeshComponent>());

        protected override ProtoIt ProtoIt => _animalIt;
        protected override ProtoPool<AnimalEnumStateComponent> Pool => _aspect.AnimalState;

        public override void Init(IProtoSystems systems) =>
            AddTransition(ToRandomStateTransition());

        protected override bool IsState(ProtoEntity entity) =>
            _aspect.AnimalState.Get(entity).State == AnimalState.ChangeState;

        protected override void Update(ProtoEntity entity)
        {
        }

        private MutableStateTransition<AnimalState> ToRandomStateTransition() =>
            new(GetRandomState, _ => true);

        private AnimalState GetRandomState(ProtoEntity entity)
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