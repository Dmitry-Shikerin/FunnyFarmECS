using System;
using Animancer;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.MyLeoEcsProto.States.Controllers;
using Sources.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.Farmers.Infrastructure
{
    public class FarmerWorkSystem : EnumStateSystem<FarmerState, FarmerEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt 
            = new (It.Inc<
            FarmerEnumStateComponent,
            TransformComponent,
            AnimancerEcsComponent>());
        [DI] private readonly MainAspect _aspect;
        private readonly IAssetCollector _assetCollector;
        
        private FarmerConfig _config;

        public FarmerWorkSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<FarmerEnumStateComponent> Pool => _aspect.FarmerState;

        public override void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<FarmerConfig>();
            AddTransition(ToRandomTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == FarmerState.Work;

        protected override void Enter(ProtoEntity entity)
        {
            var animancer = _aspect.Animancer.Get(entity).Animancer;
            AnimancerState animancerState = animancer.Play(_config.WorkEnter);
            animancerState.OwnedEvents.OnEnd = () => animancer.Play(_config.WorkLoop);
            ref FarmerEnumStateComponent state = ref Pool.Get(entity);
            state.Timer = Random.Range(_config.WorkTimeRange.x, _config.WorkTimeRange.y);
        }

        protected override void Update(ProtoEntity entity)
        {
            ref FarmerEnumStateComponent state = ref Pool.Get(entity);
            state.Timer -= Time.deltaTime;
        }

        private MutableStateTransition<FarmerState> ToRandomTransition()
        {
            return new MutableStateTransition<FarmerState>(
                (_) =>
                {
                    int changeState = Random.Range(0, 100);

                    return changeState switch
                    {
                        < 50 => FarmerState.Work,
                        > 50 => FarmerState.Move,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                },
                (entity) =>
                {
                    ref FarmerEnumStateComponent stateComponent = ref Pool.Get(entity);
                    return stateComponent.Timer <= 0;
                });
        }
    }
}