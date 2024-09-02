using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using UnityEngine;

namespace Sources.BoundedContexts.Movements.Infrastructure.Services.Domain.Interfaces
{
    public interface IMovementService
    {
        void SetSpeed(Movement characterMovement, float speed, float deltaTime);
        void SetDirection(Movement movement, Vector3 movementDirection, float deltaTime);
        float GetAngleRotation(Vector3 characterPosition, Vector3 lookPosition);
        void SetAnimationDirection(
            Movement characterMovement, Vector3 movementDirection, float angleRotation, float deltaTime);
        float GetAngle(Vector3 characterPosition, Vector3 lookDirection);
    }
}