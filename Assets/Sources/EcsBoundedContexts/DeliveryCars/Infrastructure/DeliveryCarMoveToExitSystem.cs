using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryCars.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DeliveryCars.Infrastructure
{
    public class DeliveryCarMoveToExitSystem : EnumStateSystem<DeliveryCarState, DeliveryCarEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                DeliveryCarEnumStateComponent,
                TransformComponent>());

        [DI] private readonly MainAspect _aspect;

        private readonly IAssetCollector _assetCollector;

        private DeliveryCarConfig _config;

        public DeliveryCarMoveToExitSystem(IAssetCollector assetCollector)
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
            Pool.Get(entity).State == DeliveryCarState.MoveToExit;

        protected override void Enter(ProtoEntity entity)
        {
            ref MovementPointComponent movePoint = ref _aspect.MovementPoints.Get(entity);
            int targetPointIndex = 0;
            movePoint.TargetPoint = movePoint.Points[targetPointIndex];
            movePoint.TargetPointIndex = targetPointIndex;
        }

        protected override void Update(ProtoEntity entity)
        {
            ref TransformComponent transformComponent = ref _aspect.Transform.Get(entity);
            ref MovementPointComponent movePointComponent = ref _aspect.MovementPoints.Get(entity);
            
            Transform transform = transformComponent.Transform;
            Vector3 targetPoint = movePointComponent.TargetPoint;
            
            transform.position = Vector3.MoveTowards(
                transform.position, 
                targetPoint, 
                _config.MoveSpeed * Time.deltaTime);
            
            Vector3 targetDirection = targetPoint - transform.position;
            float angle = Vector3.SignedAngle(Vector3.forward, targetDirection, Vector3.up);
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation, 
                _config.RotationSpeed * Time.deltaTime);

            if (transform.position != targetPoint)
                return;

            if (movePointComponent.TargetPointIndex == movePointComponent.Points.Length - 1)
                return;
            
            movePointComponent.TargetPointIndex++;
            movePointComponent.TargetPoint = movePointComponent.Points[movePointComponent.TargetPointIndex];
        }

        private Transition<DeliveryCarState> ToMoveToExitTransition()
        {
            return new Transition<DeliveryCarState>(
                DeliveryCarState.ExitIdle,
                entity =>
                {
                    ref MovementPointComponent movePoint = ref _aspect.MovementPoints.Get(entity);
                    ref TransformComponent transform = ref _aspect.Transform.Get(entity);
                    
                    return transform.Transform.position == movePoint.Points[^1];
                });
        }
    }
}