using System;
using JetBrains.Annotations;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.AnimalAnimations.Domain;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animals.Infrastructure;
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
        private readonly IAssetCollector _assetCollector;
        private readonly AnimalEntityFactory _animalEntityFactory;
        private readonly RootGameObject _rootGameObject;

        private AnimalConfigCollector _configs;
        
        public AnimalInitializeSystem(
            IAssetCollector assetCollector,
            AnimalEntityFactory animalEntityFactory,
            RootGameObject rootGameObject)
        {
            _assetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
            _animalEntityFactory = animalEntityFactory ?? throw new ArgumentNullException(nameof(animalEntityFactory));
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
        }
        
        public void Init(IProtoSystems systems)
        {
            _configs = _assetCollector.Get<AnimalConfigCollector>();

            _animalEntityFactory.Create(_rootGameObject.DogHouseView.AnimalView);

            // foreach (ProtoEntity entity in _animalIt)
            // {
            //     ref AnimalTypeComponent animalType = ref _mainAspect.AnimalType.Get(entity);
            //     ref AnimancerComponent animancer = ref _mainAspect.Animancer.Get(entity);
            //     ref AnimalStateComponent state = ref _mainAspect.AnimalState.Get(entity);
            //     
            //     state.CurrentState = AnimalState.ChangeState;
            //     AnimationClip clip = GetClip(animalType.AnimalType);
            //     animancer.Animancer.Play(clip);
            // }
        }
        
        private AnimationClip GetClip(AnimalType animal) =>
            _configs.GetById(animal.ToString()).Walk;
    }
}