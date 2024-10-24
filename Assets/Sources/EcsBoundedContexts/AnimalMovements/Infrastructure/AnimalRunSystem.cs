﻿using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.AnimalAnimations.Domain;
using Sources.BoundedContexts.AnimalMovePoints;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.MyLeoEcsProto.States.Controllers;
using Sources.MyLeoEcsProto.States.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalRunSystem : EnumStateSystem<AnimalState, AnimalEnumStateComponent>
    {
        private readonly IAssetCollector _collector;
        [DI] private readonly MainAspect _aspect = default;

        [DI] private readonly ProtoIt _animalIt =
            new(It.Inc<
                AnimalTypeComponent,
                AnimancerComponent,
                AnimalEnumStateComponent,
                MovementPointComponent,
                NavMeshComponent,
                TransformComponent>());
        private readonly RootGameObject _rootGameObject;
        
        private AnimalConfigCollector _configs;

        public AnimalRunSystem(
            IAssetCollector collector,
            RootGameObject rootGameObject)
        {
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
        }

        protected override ProtoIt ProtoIt => _animalIt;
        protected override ProtoPool<AnimalEnumStateComponent> Pool => _aspect.AnimalState;

        public override void Init(IProtoSystems systems)
        {
            _configs = _collector.Get<AnimalConfigCollector>();
            AddTransition(ToChangeTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            _aspect.AnimalState.Get(entity).CurrentState == AnimalState.Run;

        protected override void Enter(ProtoEntity entity)
        {
            ref MovementPointComponent target = ref _aspect.MovementPoints.Get(entity);
            AnimalTypeComponent animalType = _aspect.AnimalType.Get(entity);
            AnimancerComponent animancer = _aspect.Animancer.Get(entity);
            
            target.TargetPoint = GetNextMovePoint(animalType.AnimalType);
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Run;
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
            IReadOnlyList<AnimalMovePoint> dogPoints = _rootGameObject.DogHouseView.Points;
            IReadOnlyList<AnimalMovePoint> catPoints = _rootGameObject.CatHouseView.Points;
            IReadOnlyList<AnimalMovePoint> sheepPoints = _rootGameObject.SheepPenView.Points;
            IReadOnlyList<AnimalMovePoint> chickenPoints = _rootGameObject.ChickenCorralView.Points;
            
            return animal switch
            {
                AnimalType.Dog => dogPoints[Random.Range(0, dogPoints.Count)].Position,
                AnimalType.Cat => catPoints[Random.Range(0, catPoints.Count)].Position,
                AnimalType.Sheep => sheepPoints[Random.Range(0, sheepPoints.Count)].Position,
                AnimalType.Chicken => chickenPoints[Random.Range(0, chickenPoints.Count)].Position,
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