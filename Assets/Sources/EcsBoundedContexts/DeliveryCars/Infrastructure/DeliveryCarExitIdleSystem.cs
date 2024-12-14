using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryCars.Domain;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryCars.Infrastructure
{
    public class DeliveryCarExitIdleSystem : EnumStateSystem<DeliveryCarState, DeliveryCarEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                DeliveryCarEnumStateComponent,
                GameObjectComponent>());

        [DI] private readonly MainAspect _aspect;

        private readonly IAssetCollector _assetCollector;

        private DeliveryCarConfig _config;

        public DeliveryCarExitIdleSystem(IAssetCollector assetCollector)
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
            Pool.Get(entity).State == DeliveryCarState.ExitIdle;

        protected override void Enter(ProtoEntity entity)
        {
            ref TimerComponent timer = ref _aspect.Timer.Add(entity);
            timer.Value = Random.Range(_config.ExitIdleTime.x, _config.ExitIdleTime.y);
            
            ref GameObjectComponent gameObject = ref _aspect.GameObject.Get(entity);
            gameObject.GameObject.SetActive(false);
        }

        protected override void Update(ProtoEntity entity)
        {
        }
        
        protected override void Exit(ProtoEntity entity)
        {
            ref GameObjectComponent gameObject = ref _aspect.GameObject.Get(entity);
            gameObject.GameObject.SetActive(true);
        }

        private Transition<DeliveryCarState> ToMoveToExitTransition()
        {
            return new Transition<DeliveryCarState>(
                DeliveryCarState.MoveToHome,
                entity => _aspect.Timer.Has(entity) == false);
        }
    }
}