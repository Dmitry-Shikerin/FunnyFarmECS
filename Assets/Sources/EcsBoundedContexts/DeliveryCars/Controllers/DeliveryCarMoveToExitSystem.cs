using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Paths.Domain;
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
    public class DeliveryCarMoveToExitSystem : EnumStateSystem<DeliveryCarState, DeliveryCarEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                DeliveryCarEnumStateComponent,
                PointPathComponent,
                TransformComponent>());
        [DI] private readonly MainAspect _aspect;
        
        private readonly RootGameObject _rootGameObject;
        private readonly Dictionary<PathOwnerType, Vector3[]> _paths = new();

        public DeliveryCarMoveToExitSystem(RootGameObject rootGameObject)
        {
            _rootGameObject = rootGameObject;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<DeliveryCarEnumStateComponent> Pool => _aspect.DeliveryCarState;

        public override void Init(IProtoSystems systems)
        {
            AddTransition(ToMoveToExitTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == DeliveryCarState.MoveToExit;

        protected override void Enter(ProtoEntity entity)
        {
            ref TargetPointComponent targetPoint = ref _aspect.TargetPoint.Add(entity);
            ref PointPathComponent pointPath = ref _aspect.PointsPath.Get(entity);
            
            pointPath.Points = GetPath(pointPath.PathOwnerType);
            int targetPointIndex = 0;
            targetPoint.Value = pointPath.Points[targetPointIndex];
            targetPoint.Index = targetPointIndex;
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private Transition<DeliveryCarState> ToMoveToExitTransition()
        {
            return new Transition<DeliveryCarState>(
                DeliveryCarState.ExitIdle,
                entity => _aspect.TargetPoint.Has(entity) == false);
        }
        
        private Vector3[] GetPath(PathOwnerType pathOwnerType)
        {
            if (_paths.TryGetValue(pathOwnerType, out Vector3[] path))
                return path;
            
            _paths[pathOwnerType] = _rootGameObject
                .PathCollector
                .Paths[pathOwnerType]
                .PathTypes[PathType.Points]
                .Points
                .Select(pointData => pointData.Transform.position)
                .ToArray();
            
            return _paths[pathOwnerType];
        }
    }
}