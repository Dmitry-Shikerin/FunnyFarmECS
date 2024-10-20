﻿using System;
using JetBrains.Annotations;
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
using Sources.MyLeoEcsProto.States.Controllers.Transitions.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimalMovements.Infrastructure
{
    public class AnimalIdleSystem : EnumStateSystem<AnimalState, AnimalEnumStateComponent>
    {
        private readonly IAssetCollector _assetCollector;
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _animalIt = 
            new (It.Inc<
                AnimalTypeComponent, 
                AnimancerComponent, 
                AnimalEnumStateComponent, 
                MovementPointComponent,
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
            _aspect.AnimalState.Get(entity).CurrentState == AnimalState.Idle;

        protected override void Enter(ProtoEntity entity)
        {
            ref AnimalEnumStateComponent enumState = ref _aspect.AnimalState.Get(entity);
            AnimalTypeComponent animalType = _aspect.AnimalType.Get(entity);
            AnimancerComponent animancer = _aspect.Animancer.Get(entity);
            
            AnimationClip clip = _configs.GetById(animalType.AnimalType.ToString()).Idle;
            animancer.Animancer.Play(clip);
            enumState.TargetIdleTime = 5f;
            enumState.CurentIdleTime = 0;
        }

        protected override void Update(ProtoEntity entity)
        {
            ref AnimalEnumStateComponent enumState = ref _aspect.AnimalState.Get(entity);
            
            enumState.CurentIdleTime += Time.deltaTime;
        }

        private Transition<AnimalState> ToChangeTransition()
        {
            return new Transition<AnimalState>(
                AnimalState.ChangeState,
                entity =>
                {
                    AnimalEnumStateComponent enumState = _aspect.AnimalState.Get(entity);

                    return enumState.CurentIdleTime >= enumState.TargetIdleTime;
                });
        }
    }
}