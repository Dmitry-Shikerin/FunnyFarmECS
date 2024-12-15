using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Harvesters.Domain;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Harvesters.Controllers
{
    public class HarvesterHomeSystem : EnumStateSystem<HarvesterState, HarvesterEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                HarvesterEnumStateComponent,
                GameObjectComponent>());
        [DI] private readonly MainAspect _aspect;

        private readonly IAssetCollector _assetCollector;

        private HarvesterConfig _config;

        public HarvesterHomeSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<HarvesterEnumStateComponent> Pool => _aspect.HarvesterState;

        public override void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<HarvesterConfig>();

            AddTransition(ToMoveToFieldTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == HarvesterState.Home;

        protected override void Enter(ProtoEntity entity)
        {
            ref TimerComponent timer = ref _aspect.Timer.Add(entity);
            timer.Value = Random.Range(_config.HomeIdleTime.x, _config.HomeIdleTime.y);
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<HarvesterState> ToMoveToFieldTransition()
        {
            return new Transition<HarvesterState>(
                HarvesterState.MoveToField,
                entity => _aspect.Timer.Has(entity) == false);
        }
    }
}