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
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalMovementSystem : IProtoRunSystem
    {
        [DI] private readonly MainAspect _mainAspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<
                AnimalTypeComponent, 
                AnimancerComponent, 
                AnimalStateComponent, 
                MovementPointComponent,
                NavMeshComponent>());
        private readonly AnimalConfigCollector _configs;
        private readonly RootGameObject _rootGameObject;

        public AnimalMovementSystem(DiContainer container)
        {
            _configs = container
                .Resolve<IAssetCollector>()
                .Get<AnimalConfigCollector>();
            _rootGameObject = container.Resolve<RootGameObject>();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _animalIt)
            {
                AnimalStateComponent state = _mainAspect.AnimalStatePool.Get(entity);
                
                if (state.AnimalState != AnimalState.Walk)
                    continue;
                
                ref MovementPointComponent target = ref _mainAspect.MovementPointsPool.Get(entity);
                ref NavMeshComponent agent = ref _mainAspect.NavMeshPool.Get(entity);
                agent.Agent.Move(target.TargetPoint);
            }
        }

        private AnimationClip GetClip(AnimalType animal) =>
            _configs.GetById(animal.ToString()).Walk;
    }
}