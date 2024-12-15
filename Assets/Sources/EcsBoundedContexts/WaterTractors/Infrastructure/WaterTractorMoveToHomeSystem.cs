using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.WaterTractors.Infrastructure
{
    public class WaterTractorMoveToHomeSystem : EnumStateSystem<WaterTractorState, WaterTractorEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                WaterTractorEnumStateComponent,
                TransformComponent,
                WaterTractorMovePointComponent>());

        [DI] private readonly MainAspect _aspect;

        private readonly IAssetCollector _assetCollector;

        private WaterTractorConfig _config;

        public WaterTractorMoveToHomeSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<WaterTractorEnumStateComponent> Pool => _aspect.WaterTractorState;

        public override void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<WaterTractorConfig>();

            AddTransition(ToMoveToExitTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == WaterTractorState.MoveToHome;

        protected override void Enter(ProtoEntity entity)
        {
            ref WaterTractorMovePointComponent movePoint = ref _aspect.WaterTractorMovePoint.Get(entity);
            int targetPointIndex = 0;
            movePoint.TargetPoint = movePoint.ToHomePoints[targetPointIndex];
            movePoint.TargetPointIndex = targetPointIndex;
        }

        protected override void Update(ProtoEntity entity)
        {
            ref TransformComponent transformComponent = ref _aspect.Transform.Get(entity);
            ref WaterTractorMovePointComponent movePointComponent = ref _aspect.WaterTractorMovePoint.Get(entity);

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

            if (movePointComponent.TargetPointIndex == movePointComponent.ToHomePoints.Length - 1)
                return;

            movePointComponent.TargetPointIndex++;
            movePointComponent.TargetPoint = movePointComponent.ToHomePoints[movePointComponent.TargetPointIndex];
        }

        private Transition<WaterTractorState> ToMoveToExitTransition()
        {
            return new Transition<WaterTractorState>(
                WaterTractorState.Home,
                entity =>
                {
                    ref WaterTractorMovePointComponent movePoint = ref _aspect.WaterTractorMovePoint.Get(entity);
                    ref TransformComponent transform = ref _aspect.Transform.Get(entity);

                    return transform.Transform.position == movePoint.ToHomePoints[^1];
                });
        }
    }
}