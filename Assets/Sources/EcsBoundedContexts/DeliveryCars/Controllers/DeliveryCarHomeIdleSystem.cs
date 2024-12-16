using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryCars.Domain;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryCars.Controllers
{
    public class DeliveryCarHomeIdleSystem : EnumStateSystem<DeliveryCarState, DeliveryCarEnumStateComponent>
    {
        private readonly IAssetCollector _assetCollector;

        [DI] private readonly ProtoIt _protoIt = 
            new (It.Inc<
                DeliveryCarEnumStateComponent>());
        [DI] private readonly MainAspect _aspect;
        
        private DeliveryCarConfig _config;

        public DeliveryCarHomeIdleSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<DeliveryCarEnumStateComponent> Pool => _aspect.DeliveryCarState;

        public override void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<DeliveryCarConfig>();
            AddTransition(ToMoveToExitTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == DeliveryCarState.HomeIdle;

        protected override void Enter(ProtoEntity entity)
        {
            ref TimerComponent timer = ref _aspect.Timer.Add(entity);
            timer.Value = Random.Range(_config.HomeIdleTime.x, _config.HomeIdleTime.y);
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<DeliveryCarState> ToMoveToExitTransition()
        {
            return new Transition<DeliveryCarState>(
                DeliveryCarState.MoveToExit,
                entity => _aspect.Timer.Has(entity) == false);
        }
    }
}