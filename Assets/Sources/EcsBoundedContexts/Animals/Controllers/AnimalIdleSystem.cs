using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.AnimalAnimations.Domain;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Animals.Controllers
{
    public class AnimalIdleSystem : EnumStateSystem<AnimalState, AnimalEnumStateComponent>
    {
        private readonly IAssetCollector _assetCollector;
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<
                AnimalTypeComponent, 
                AnimancerEcsComponent, 
                AnimalEnumStateComponent,
                NavMeshComponent>());
        private AnimalConfigCollector _configs;

        public AnimalIdleSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
        }

        protected override ProtoIt ProtoIt => _animalIt;
        protected override ProtoPool<AnimalEnumStateComponent> Pool => _aspect.AnimalState;

        public override void Init(IProtoSystems systems)
        {
            _configs = _assetCollector.Get<AnimalConfigCollector>();
            AddTransition(ToChangeTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            _aspect.AnimalState.Get(entity).State == AnimalState.Idle;

        protected override void Enter(ProtoEntity entity)
        {
            ref TimerComponent timer = ref _aspect.Timer.Add(entity);
            AnimalTypeComponent animalType = _aspect.AnimalType.Get(entity);
            AnimancerEcsComponent animancerEcs = _aspect.Animancer.Get(entity);
            
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Idle;
            animancerEcs.Animancer.Play(clip);
            timer.Value = 5f;
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<AnimalState> ToChangeTransition()
        {
            return new Transition<AnimalState>(
                AnimalState.ChangeState,
                entity => entity.HasTimer() == false);
        }
    }
}