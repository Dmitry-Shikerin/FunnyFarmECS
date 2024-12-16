using System;
using Animancer;
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
            ref FarmerEnumStateComponent state = ref Pool.Get(entity);
            ref TimerComponent timer = ref _aspect.Timer.Add(entity);
            
            timer.Value = Random.Range(_config.WorkTimeRange.x, _config.WorkTimeRange.y);
            AnimancerComponent animancer = _aspect.Animancer.Get(entity).Animancer;
            AnimancerState animancerState = animancer.Play(_config.WorkEnter);
            animancerState.OwnedEvents.OnEnd = () => animancer.Play(_config.WorkLoop);
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
                        < 40 => FarmerState.Work,
                        > 40 => FarmerState.Move,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                },
                entity => _aspect.Timer.Has(entity) == false);
        }
    }
}