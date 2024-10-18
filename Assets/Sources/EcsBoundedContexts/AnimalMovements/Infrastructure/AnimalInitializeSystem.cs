using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using Sources.BoundedContexts.AnimalAnimations.Domain;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalInitializeSystem : IProtoInitSystem
    {
        [DI] private readonly MainAspect _mainAspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<AnimalTypeComponent, AnimancerComponent>());
        private readonly AnimalConfigCollector _configs;
        
        public AnimalInitializeSystem(DiContainer container)
        {
            _configs = container
                .Resolve<IAssetCollector>()
                .Get<AnimalConfigCollector>();
        }
        
        public void Init(IProtoSystems systems)
        {
            foreach (ProtoEntity entity in _animalIt)
            {
                ref AnimalTypeComponent animalType = ref _mainAspect.AnimalTypePool.Get(entity);
                ref AnimancerComponent animancer = ref _mainAspect.AnimancerPool.Get(entity);
                ref AnimalStateComponent state = ref _mainAspect.AnimalStatePool.Get(entity);
                state.CurrentState = AnimalState.ChangeState;
                AnimationClip clip = GetClip(animalType.AnimalType);
                animancer.Animancer.Play(clip);
            }
        }
        
        private AnimationClip GetClip(AnimalType animal) =>
            _configs.GetById(animal.ToString()).Walk;
    }
}