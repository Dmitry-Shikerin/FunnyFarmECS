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
    public class AnimalChangeStateSystem : IProtoRunSystem
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

        public AnimalChangeStateSystem(DiContainer container)
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
                ref AnimalStateComponent state = ref _mainAspect.AnimalStatePool.Get(entity);
                
                if (state.AnimalState != AnimalState.ChangeState)
                    continue;

                state.AnimalState = GetState(entity);
                Debug.Log($"{state.AnimalState}");
            }
        }

        private AnimalState GetState(ProtoEntity entity)
        {
            int stateValue = Random.Range(0, 100);

            return stateValue switch
            {
                < 33 => SetWalkState(entity),
                > 33 and < 66 => SetRunState(entity),
                _ => SetIdleState(entity),
            };
        }


        private AnimalState SetWalkState(ProtoEntity entity)
        {
            ref MovementPointComponent target = ref _mainAspect.MovementPointsPool.Get(entity);
            AnimalTypeComponent animalType = _mainAspect.AnimalTypePool.Get(entity);
            AnimancerComponent animancer = _mainAspect.AnimancerPool.Get(entity);
            target.TargetPoint = GetNextMovePoint(animalType.AnimalType);
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Walk;
            animancer.Animancer.Play(clip);
            
            return AnimalState.Walk;
        }

        private AnimalState SetRunState(ProtoEntity entity)
        {
            ref MovementPointComponent target = ref _mainAspect.MovementPointsPool.Get(entity);
            AnimalTypeComponent animalType = _mainAspect.AnimalTypePool.Get(entity);
            AnimancerComponent animancer = _mainAspect.AnimancerPool.Get(entity);
            target.TargetPoint = GetNextMovePoint(animalType.AnimalType);
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Run;
            animancer.Animancer.Play(clip);
            
            return AnimalState.Run;
        }

        private AnimalState SetIdleState(ProtoEntity entity)
        {
            ref MovementPointComponent target = ref _mainAspect.MovementPointsPool.Get(entity);
            ref AnimalStateComponent state = ref _mainAspect.AnimalStatePool.Get(entity);
            AnimalTypeComponent animalType = _mainAspect.AnimalTypePool.Get(entity);
            AnimancerComponent animancer = _mainAspect.AnimancerPool.Get(entity);
            target.TargetPoint = GetNextMovePoint(animalType.AnimalType);
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Idle;
            animancer.Animancer.Play(clip);
            state.TargetIdleTime = 5f;
            state.CurentIdleTime = 0;
            
            return AnimalState.Idle;
        }
        
        private Vector3 GetNextMovePoint(AnimalType animal)
        {
            return animal switch
            {
                AnimalType.Dog => _rootGameObject.DogMovePoints[Random.Range(0, _rootGameObject.DogMovePoints.Count)].Position,
                _ => throw new System.ArgumentException("unknown animal type")
            };
        }
    }
}