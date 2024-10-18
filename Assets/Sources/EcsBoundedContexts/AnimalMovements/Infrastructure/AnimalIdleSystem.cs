using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using Sources.BoundedContexts.AnimalAnimations.Domain;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.MyLeoEcsProto.States.Controllers;
using Sources.MyLeoEcsProto.States.Controllers.Transitions;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalIdleSystem : StateSystem<AnimalState, AnimalStateComponent>
    {
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<
                AnimalTypeComponent, 
                AnimancerComponent, 
                AnimalStateComponent, 
                MovementPointComponent,
                NavMeshComponent>());
        private readonly AnimalConfigCollector _configs;

        public AnimalIdleSystem(DiContainer container)
        {
            _configs = container
                .Resolve<IAssetCollector>()
                .Get<AnimalConfigCollector>();
        }

        protected override ProtoIt ProtoIt => _animalIt;
        protected override ProtoPool<AnimalStateComponent> Pool => _aspect.AnimalState;

        public override void Init(IProtoSystems systems) =>
            AddTransition(ToChangeTransition());

        protected override bool IsState(ProtoEntity entity) =>
            _aspect.AnimalState.Get(entity).CurrentState == AnimalState.Idle;

        protected override void Enter(ProtoEntity entity)
        {
            ref AnimalStateComponent state = ref _aspect.AnimalState.Get(entity);
            AnimalTypeComponent animalType = _aspect.AnimalType.Get(entity);
            AnimancerComponent animancer = _aspect.Animancer.Get(entity);
            
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Idle;
            animancer.Animancer.Play(clip);
            state.TargetIdleTime = 5f;
            state.CurentIdleTime = 0;
        }

        protected override void Update(ProtoEntity entity)
        {
            ref AnimalStateComponent state = ref _aspect.AnimalState.Get(entity);
            
            state.CurentIdleTime += Time.deltaTime;
        }

        private Transition<AnimalState> ToChangeTransition()
        {
            return new Transition<AnimalState>(
                AnimalState.ChangeState,
                entity =>
                {
                    AnimalStateComponent state = _aspect.AnimalState.Get(entity);

                    return state.CurentIdleTime >= state.TargetIdleTime;
                });
        }
    }
}