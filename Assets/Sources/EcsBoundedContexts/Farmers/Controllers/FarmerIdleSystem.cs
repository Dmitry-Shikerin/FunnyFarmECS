using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.Farmers.Controllers
{
    public class FarmerIdleSystem : EnumStateSystem<FarmerState, FarmerEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt 
            = new (It.Inc<
            FarmerEnumStateComponent,
            TransformComponent,
            AnimancerEcsComponent>());
        [DI] private readonly MainAspect _aspect;
        
        private readonly IAssetCollector _assetCollector;
        
        private FarmerConfig _config;

        public FarmerIdleSystem(IAssetCollector assetCollector)
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
            Pool.Get(entity).State == FarmerState.Idle;

        protected override void Enter(ProtoEntity entity)
        {
            ref TimerComponent timer = ref _aspect.Timer.Add(entity);
            
            _aspect.Animancer.Get(entity).Animancer.Play(_config.Idle);
            timer.Value = Random.Range(_config.IdleTimeRange.x, _config.IdleTimeRange.y);
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private MutableStateTransition<FarmerState> ToRandomTransition()
        {
            return new MutableStateTransition<FarmerState>(
                _ =>
                {
                    int changeState = Random.Range(0, 100);
                    
                    return changeState switch
                    {
                        < 40 => FarmerState.Idle,
                        > 40 => FarmerState.Move,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                },
                entity => _aspect.Timer.Has(entity) == false);
        }
    }
}