using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.WaterTractors.Controllers
{
    public class WaterTractorHomeSystem : EnumStateSystem<WaterTractorState, WaterTractorEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                WaterTractorEnumStateComponent,
                GameObjectComponent>());
        [DI] private readonly MainAspect _aspect;

        private readonly IAssetCollector _assetCollector;

        private WaterTractorConfig _config;

        public WaterTractorHomeSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<WaterTractorEnumStateComponent> Pool => _aspect.WaterTractorState;

        public override void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<WaterTractorConfig>();
            AddTransition(ToMoveToFieldTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == WaterTractorState.Home;

        protected override void Enter(ProtoEntity entity)
        {
            ref TimerComponent timer = ref _aspect.Timer.Add(entity);
            timer.Value = Random.Range(_config.HomeIdleTime.x, _config.HomeIdleTime.y);
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<WaterTractorState> ToMoveToFieldTransition()
        {
            return new Transition<WaterTractorState>(
                WaterTractorState.MoveToField,
                entity => _aspect.Timer.Has(entity) == false);
        }
    }
}