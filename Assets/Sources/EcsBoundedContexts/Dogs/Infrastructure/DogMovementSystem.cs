using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using Sources.BoundedContexts.AnimalAnimations.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Controllers;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.StateMachines.ContextStateMachines.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Dogs.Infrastructure
{
    public class DogMovementSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] private readonly MainAspect _mainAspect = default;
        [DI] private readonly ProtoIt _dogIt = 
            new (It.Inc<DogComponent, AnimancerComponent>());
        private readonly AnimalConfig _animalConfig;
        
        private ContextStateMachine _dogStateMachine;

        public DogMovementSystem(DiContainer container)
        {
            _animalConfig = container
                .Resolve<IAssetCollector>()
                .Get<AnimalConfigCollector>()
                .GetById("Dog");
        }

        public void Init(IProtoSystems systems)
        {
            foreach (ProtoEntity entity in _dogIt)
            {
                ref DogComponent dog = ref _mainAspect.DogPool.Get(entity);
                ref AnimancerComponent animancer = ref _mainAspect.AnimancerPool.Get(entity);
                dog.AnimalState = AnimalState.Walk;
                animancer.Animancer.Play(_animalConfig.Walk);
            }
            
            _dogStateMachine = CreateStateMachine();
            _dogStateMachine.Run();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _dogIt)
            {
                _dogStateMachine.Apply(new EntityProvider(entity));
                _dogStateMachine.Update(Time.deltaTime);
            }
        }

        private ContextStateMachine CreateStateMachine()
        {
            DogIdleState idleState = new DogIdleState(_mainAspect, _animalConfig);
            
            return new ContextStateMachine(idleState);
        }
    }
}