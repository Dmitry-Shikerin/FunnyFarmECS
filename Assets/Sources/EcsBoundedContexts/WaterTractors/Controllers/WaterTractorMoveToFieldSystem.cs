using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Paths.Domain;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.WaterTractors.Controllers
{
    public class WaterTractorMoveToFieldSystem : EnumStateSystem<WaterTractorState, WaterTractorEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                WaterTractorEnumStateComponent,
                TransformComponent>());
        [DI] private readonly MainAspect _aspect;

        private readonly RootGameObject _rootGameObject;

        private Vector3[] _path;

        public WaterTractorMoveToFieldSystem(
            RootGameObject rootGameObject)
        {
            _rootGameObject = rootGameObject;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<WaterTractorEnumStateComponent> Pool => _aspect.WaterTractorState;

        public override void Init(IProtoSystems systems)
        {
            _path = _rootGameObject
                .PathCollector
                .Paths[PathOwnerType.ThirdLocationWaterTractor]
                .PathTypes[PathType.ToFieldPoints]
                .Points
                .Select(pointData => pointData.Transform.position)
                .ToArray();

            AddTransition(ToMoveToHomeTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == WaterTractorState.MoveToField;

        protected override void Enter(ProtoEntity entity)
        {
            ref TargetPointComponent targetPoint = ref _aspect.TargetPoint.Add(entity);
            ref PointPathComponent pointPath = ref _aspect.PointsPath.Get(entity);
            
            pointPath.Points = _path;
            int targetPointIndex = 0;
            targetPoint.Value = pointPath.Points[targetPointIndex];
            targetPoint.Index = targetPointIndex;
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<WaterTractorState> ToMoveToHomeTransition()
        {
            return new Transition<WaterTractorState>(
                WaterTractorState.MoveToHome,
                entity => _aspect.TargetPoint.Has(entity) == false);
        }
    }
}