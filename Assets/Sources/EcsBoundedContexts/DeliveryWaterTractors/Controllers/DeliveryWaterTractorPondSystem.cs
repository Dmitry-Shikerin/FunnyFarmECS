using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Controllers
{
    public class DeliveryWaterTractorPondSystem : EnumStateSystem<DeliveryWaterTractorState, DeliveryWaterTractorEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                DeliveryWaterTractorEnumStateComponent,
                GameObjectComponent>());
        [DI] private readonly MainAspect _aspect;

        private readonly IAssetCollector _assetCollector;

        private DeliveryWaterTractorConfig _config;

        public DeliveryWaterTractorPondSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<DeliveryWaterTractorEnumStateComponent> Pool => _aspect.DeliveryWaterTractorState;

        public override void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<DeliveryWaterTractorConfig>();

            AddTransition(ToMoveToExitTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == DeliveryWaterTractorState.Pond;

        protected override void Enter(ProtoEntity entity)
        {
            ref TimerComponent timer = ref _aspect.Timer.Add(entity);
            timer.Value = Random.Range(_config.PondIdleTime.x, _config.PondIdleTime.y);
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<DeliveryWaterTractorState> ToMoveToExitTransition()
        {
            return new Transition<DeliveryWaterTractorState>(
                DeliveryWaterTractorState.MoveToHome,
                entity => _aspect.Timer.Has(entity) == false);
        }
    }
}