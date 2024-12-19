using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Paths.Domain;
using Sources.BoundedContexts.Paths.Presentation;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Controllers
{
    public class DeliveryWaterTractorMoveToHomeSystem : EnumStateSystem<DeliveryWaterTractorState, DeliveryWaterTractorEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                DeliveryWaterTractorEnumStateComponent,
                TransformComponent>());
        [DI] private readonly MainAspect _aspect;
        
        private readonly PathCollectorView _pathCollector;
        private Vector3[] _path;

        public DeliveryWaterTractorMoveToHomeSystem(RootGameObject rootGameObject)
        {
            _pathCollector = rootGameObject.PathCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<DeliveryWaterTractorEnumStateComponent> Pool => _aspect.DeliveryWaterTractorState;

        public override void Init(IProtoSystems systems)
        {
            _path = _pathCollector.GetPath(PathOwnerType.ThirdLocationDeliveryWaterTractor, PathType.PathPoints, true);

            AddTransition(ToMoveToExitTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == DeliveryWaterTractorState.MoveToHome;

        protected override void Enter(ProtoEntity entity)
        {
            ref TargetPointComponent targetPoint = ref _aspect.TargetPoint.Add(entity);
            ref PointPathComponent movePointPath = ref _aspect.PointsPath.Get(entity);

            movePointPath.Points = _path;
            int targetPointIndex = movePointPath.Points.Length - 1;
            targetPoint.Value = movePointPath.Points[targetPointIndex];
            targetPoint.Index = targetPointIndex;
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<DeliveryWaterTractorState> ToMoveToExitTransition()
        {
            return new Transition<DeliveryWaterTractorState>(
                DeliveryWaterTractorState.Home,
                entity => entity.HasTargetPoint() == false);
        }
    }
}