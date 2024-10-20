using System;
using JetBrains.Annotations;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using Sources.BoundedContexts.AnimalAnimations.Domain;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.Utils.ObservablePropeties;
using Sources.MyLeoEcsProto.States.Controllers;
using Sources.MyLeoEcsProto.States.Controllers.Transitions;
using Sources.MyLeoEcsProto.States.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalWalkSystem : EnumStateSystem<AnimalState, AnimalEnumStateComponent>
    {
        private readonly IAssetCollector _assetCollector;
        [DI] private readonly MainAspect _aspect = default;

        [DI] private readonly ProtoIt _animalIt =
            new(It.Inc<
                AnimalTypeComponent,
                AnimancerComponent,
                AnimalEnumStateComponent,
                MovementPointComponent,
                NavMeshComponent,
                TransformComponent>());

        private AnimalConfigCollector _configs;
        private readonly RootGameObject _rootGameObject;

        public AnimalWalkSystem(
            IAssetCollector assetCollector, 
            RootGameObject rootGameObject)
        {
            _assetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
        }

        protected override ProtoIt ProtoIt => _animalIt;
        protected override ProtoPool<AnimalEnumStateComponent> Pool => _aspect.AnimalState;

        public override void Init(IProtoSystems systems)
        {
            _configs = _assetCollector.Get<AnimalConfigCollector>();
            AddTransition(ToChangeTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            _aspect.AnimalState.Get(entity).CurrentState == AnimalState.Walk;

        protected override void Enter(ProtoEntity entity)
        {
            ref MovementPointComponent target = ref _aspect.MovementPoints.Get(entity);
            AnimalTypeComponent animalType = _aspect.AnimalType.Get(entity);
            AnimancerComponent animancer = _aspect.Animancer.Get(entity);

            target.TargetPoint = GetNextMovePoint(animalType.AnimalType);
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Walk;
            animancer.Animancer.Play(clip);
        }

        protected override void Update(ProtoEntity entity)
        {
            ref MovementPointComponent target = ref _aspect.MovementPoints.Get(entity);
            NavMeshAgent agent = _aspect.NavMesh.Get(entity).Agent;

            agent.SetDestination(target.TargetPoint);
        }

        private Vector3 GetNextMovePoint(AnimalType animal)
        {
            return animal switch
            {
                AnimalType.Dog => _rootGameObject.DogMovePoints[Random.Range(0, _rootGameObject.DogMovePoints.Count)]
                    .Position,
                _ => throw new System.ArgumentException("unknown animal type")
            };
        }

        private Transition<AnimalState> ToChangeTransition()
        {
            return new Transition<AnimalState>(
                AnimalState.ChangeState,
                entity =>
                {
                    NavMeshAgent agent = _aspect.NavMesh.Get(entity).Agent;

                    return Vector3.Distance(agent.destination, agent.transform.position) < 2f;
                });
        }
    }
}