using System;
using Sources.BoundedContexts.Movements.Infrastructure.Services.Domain.Interfaces;
using Sources.Domain.Models.Constants;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using UnityEngine;

namespace Sources.BoundedContexts.Movements.Infrastructure.Services.Domain.Implementation
{
    public class MovementService : IMovementService
    {
        private const float Delta = 5;
        private const float Scalar = 2;
        private const float MinReactionDistance = 0.7f;

        public void SetSpeed(Movement characterMovement, float speed, float deltaTime)
        {
            characterMovement.CurrentSpeed = Mathf.MoveTowards(
                characterMovement.CurrentSpeed, speed, Delta * deltaTime);
        }

        public void SetDirection(Movement movement, Vector3 movementDirection,  float deltaTime)
        {
            movement.Direction = movement.CurrentSpeed * Scalar *
                                           deltaTime *
                                           movementDirection.normalized +
                                           Physics.gravity;
        }

        public float GetAngleRotation(Vector3 characterPosition, Vector3 lookPosition)
        {
            Vector3 lookDirection = lookPosition - characterPosition;
            lookDirection.y = characterPosition.y;
            float distance = lookDirection.magnitude;

            if (distance < MinReactionDistance)
                throw new InvalidOperationException(nameof(distance));

            return Vector3.SignedAngle(Vector3.forward, lookDirection, Vector3.up);
        }

        public float GetAngle(Vector3 characterPosition, Vector3 lookDirection)
        {
            lookDirection.y = characterPosition.y;
            // float distance = lookDirection.magnitude;

            return Vector3.SignedAngle(Vector3.forward, lookDirection, Vector3.up);
        }

        public void SetAnimationDirection(
            Movement characterMovement,
            Vector3 movementDirection,
            float angleRotation,
            float deltaTime)
        {
            Vector3 direction = Quaternion.Euler(0, -angleRotation, 0) * movementDirection;

            Vector2 direction2 = new Vector2(direction.x, direction.z).normalized;
            characterMovement.AnimationDirection =
                Vector2.MoveTowards(
                    characterMovement.AnimationDirection,
                    direction2,
                    MovementConst.AnimationDirectionSpeed * deltaTime);
        }
    }
}