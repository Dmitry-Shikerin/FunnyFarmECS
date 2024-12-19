using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Paths.Domain;
using Sources.BoundedContexts.Paths.Presentation;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryCars.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryCars.Controllers
{
    public class DeliveryCarMoveToHomeSystem : EnumStateSystem<DeliveryCarState, DeliveryCarEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                DeliveryCarEnumStateComponent,
                TransformComponent>());
        [DI] private readonly MainAspect _aspect;
        
        private readonly PathCollectorView _pathCollector;
        private readonly Dictionary<PathOwnerType, Vector3[]> _paths = new();

        public DeliveryCarMoveToHomeSystem(RootGameObject rootGameObject)
        {
            _pathCollector = rootGameObject.PathCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<DeliveryCarEnumStateComponent> Pool => _aspect.DeliveryCarState;

        public override void Init(IProtoSystems systems)
        {
            AddTransition(ToMoveToExitTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == DeliveryCarState.MoveToHome;

        protected override void Enter(ProtoEntity entity)
        {
            ref PointPathComponent movePointPath = ref _aspect.PointsPath.Get(entity);
            ref TargetPointComponent targetPoint = ref _aspect.TargetPoint.Add(entity);
            
            movePointPath.Points = GetPath(movePointPath.PathOwnerType);
            int targetPointIndex = 0;
            targetPoint.Value = movePointPath.Points[targetPointIndex];
            targetPoint.Index = targetPointIndex;
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<DeliveryCarState> ToMoveToExitTransition()
        {
            return new Transition<DeliveryCarState>(
                DeliveryCarState.HomeIdle,
                entity => _aspect.TargetPoint.Has(entity) == false);
        }
        
        private Vector3[] GetPath(PathOwnerType pathOwnerType)
        {
            if (_paths.TryGetValue(pathOwnerType, out Vector3[] path))
                return path;
            
            _paths[pathOwnerType] = _pathCollector.GetPath(pathOwnerType, PathType.PathPoints, true);
            
            return _paths[pathOwnerType];
        }
    }
}