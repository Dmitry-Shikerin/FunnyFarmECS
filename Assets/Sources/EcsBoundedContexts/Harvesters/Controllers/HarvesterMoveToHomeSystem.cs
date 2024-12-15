using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Harvesters.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Harvesters.Controllers
{
    public class HarvesterMoveToHomeSystem : EnumStateSystem<HarvesterState, HarvesterEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                HarvesterEnumStateComponent,
                TransformComponent,
                HarvesterMovePointComponent>());

        [DI] private readonly MainAspect _aspect;

        private readonly IAssetCollector _assetCollector;

        private HarvesterConfig _config;

        public HarvesterMoveToHomeSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<HarvesterEnumStateComponent> Pool => _aspect.HarvesterState;

        public override void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<HarvesterConfig>();

            AddTransition(ToMoveToExitTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == HarvesterState.MoveToHome;

        protected override void Enter(ProtoEntity entity)
        {
            ref HarvesterMovePointComponent movePoint = ref _aspect.HarvesterMovePoint.Get(entity);
            int targetPointIndex = 0;
            movePoint.TargetPoint = movePoint.ToHomePoints[targetPointIndex];
            movePoint.TargetPointIndex = targetPointIndex;
        }

        protected override void Update(ProtoEntity entity)
        {
            ref TransformComponent transformComponent = ref _aspect.Transform.Get(entity);
            ref HarvesterMovePointComponent movePointComponent = ref _aspect.HarvesterMovePoint.Get(entity);

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

        private Transition<HarvesterState> ToMoveToExitTransition()
        {
            return new Transition<HarvesterState>(
                HarvesterState.Home,
                entity =>
                {
                    ref HarvesterMovePointComponent movePoint = ref _aspect.HarvesterMovePoint.Get(entity);
                    ref TransformComponent transform = ref _aspect.Transform.Get(entity);

                    return transform.Transform.position == movePoint.ToHomePoints[^1];
                });
        }
    }
}