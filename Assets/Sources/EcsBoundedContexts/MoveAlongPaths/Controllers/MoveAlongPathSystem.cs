using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.MoveAlongPaths.Controllers
{
    public class MoveAlongPathSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                TransformComponent,
                PointPathComponent,
                TargetPointComponent,
                MoveSpeedComponent>());
        [DI] private readonly MainAspect _aspect;

        public void Run()
        {
            foreach (ProtoEntity entity in _protoIt)
            {
                ref TransformComponent transformComponent = ref _aspect.Transform.Get(entity);
                ref PointPathComponent movePointPathComponent = ref _aspect.PointsPath.Get(entity);
                ref TargetPointComponent targetPointComponent = ref _aspect.TargetPoint.Get(entity);
                MoveSpeedComponent moveSpeedComponent = _aspect.MoveSpeed.Get(entity);

                Transform transform = transformComponent.Transform;
                Vector3 targetPoint = targetPointComponent.Value;
                float moveSpeed = moveSpeedComponent.MoveSpeed;
                float rotationSpeed = moveSpeedComponent.RotationSpeed;

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPoint,
                    moveSpeed * Time.deltaTime);

                Vector3 targetDirection = targetPoint - transform.position;
                float angle = Vector3.SignedAngle(Vector3.forward, targetDirection, Vector3.up);
                Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);

                if (transform.position == movePointPathComponent.Points[^1])
                {
                    _aspect.TargetPoint.Del(entity);

                    continue;
                }

                if (transform.position != targetPoint)
                    continue;

                if (targetPointComponent.Index == movePointPathComponent.Points.Length - 1)
                    continue;

                targetPointComponent.Index++;
                targetPointComponent.Value = movePointPathComponent.Points[targetPointComponent.Index];
            }
        }
    }
}