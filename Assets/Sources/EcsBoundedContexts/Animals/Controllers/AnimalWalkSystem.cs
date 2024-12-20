using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animals.Infrastructure;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Animals.Controllers
{
    public class AnimalWalkSystem : EnumStateSystem<AnimalState, AnimalEnumStateComponent>
    {
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _animalIt =
            new(It.Inc<
                AnimalTypeComponent,
                AnimancerEcsComponent,
                AnimalEnumStateComponent,
                NavMeshComponent,
                TransformComponent>());
        
        private readonly IAssetCollector _assetCollector;
        private readonly AnimalPointsService _pointsService;

        private AnimalConfigCollector _configs;

        public AnimalWalkSystem(
            IAssetCollector assetCollector, 
            AnimalPointsService pointsService)
        {
            _assetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
            _pointsService = pointsService;
        }

        protected override ProtoIt ProtoIt => _animalIt;
        protected override ProtoPool<AnimalEnumStateComponent> Pool => _aspect.AnimalState;

        public override void Init(IProtoSystems systems)
        {
            _configs = _assetCollector.Get<AnimalConfigCollector>();
            AddTransition(ToChangeTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            _aspect.AnimalState.Get(entity).State == AnimalState.Walk;

        protected override void Enter(ProtoEntity entity)
        {
            ref TargetPointComponent target = ref _aspect.TargetPoint.Add(entity);
            AnimalTypeComponent animalType = _aspect.AnimalType.Get(entity);
            AnimancerEcsComponent animancerEcs = _aspect.Animancer.Get(entity);

            target.Value = _pointsService.GetNextMovePoint(animalType.AnimalType);
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Walk;
            animancerEcs.Animancer.Play(clip);
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<AnimalState> ToChangeTransition()
        {
            return new Transition<AnimalState>(
                AnimalState.ChangeState,
                entity => entity.HasTargetPoint() == false);
        }
    }
}