using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Paths.Domain;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.WaterTractors.Controllers
{
    public class WaterTractorMoveToHomeSystem : EnumStateSystem<WaterTractorState, WaterTractorEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                WaterTractorEnumStateComponent,
                PointPathComponent,
                TransformComponent>());

        [DI] private readonly MainAspect _aspect;

        private readonly IAssetCollector _assetCollector;
        private readonly RootGameObject _rootGameObject;

        private WaterTractorConfig _config;
        private Vector3[] _path;

        public WaterTractorMoveToHomeSystem(
            IAssetCollector assetCollector,
            RootGameObject rootGameObject)
        {
            _assetCollector = assetCollector;
            _rootGameObject = rootGameObject;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<WaterTractorEnumStateComponent> Pool => _aspect.WaterTractorState;

        public override void Init(IProtoSystems systems)
        {
            _path = _rootGameObject
                .PathCollector
                .Paths[PathOwnerType.ThirdLocationWaterTractor]
                .PathTypes[PathType.ToHomePoints]
                .Points
                .Select(pointData => pointData.Transform.position)
                .ToArray();

            AddTransition(ToMoveToExitTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == WaterTractorState.MoveToHome;

        protected override void Enter(ProtoEntity entity)
        {
            ref PointPathComponent movePoint = ref _aspect.PointsPath.Get(entity);
            ref TargetPointComponent targetPoint = ref _aspect.TargetPoint.Add(entity);
            
            movePoint.Points = _path;
            int targetPointIndex = 0;
            targetPoint.Value = movePoint.Points[targetPointIndex];
            targetPoint.Index = targetPointIndex;
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<WaterTractorState> ToMoveToExitTransition()
        {
            return new Transition<WaterTractorState>(
                WaterTractorState.Home,
                entity => _aspect.PointsPath.Has(entity) == false);
        }
    }
}