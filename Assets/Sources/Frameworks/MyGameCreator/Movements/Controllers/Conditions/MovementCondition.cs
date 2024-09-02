using Sources.BoundedContexts.Movements.Domain.Types;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.OldBoundedContexts.Movements.Domain.Types;

namespace Sources.BoundedContexts.Movements.Controllers.Conditions
{
    public static class MovementCondition
    {
        public static bool ToAction(this Movement movement)
        {
            if (movement.AnimationType != AnimationType.Action)
                return false;

            if (movement.RotationType != RotationType.Pivot)
                return false;

            if (movement.MoveState != MoveState.Idle)
                return false;

            return true;
        }

        public static bool ToActionFollowTarget(this Movement movement)
        {
            if (movement.AnimationType != AnimationType.Action)
                return false;

            if (movement.RotationType != RotationType.FollowTarget)
                return false;
                    
            if (movement.MoveState != MoveState.Idle)
                return false;

            return true;
        }

        public static bool ToDirectionalPivot(this Movement movement)
        {
            if (movement.AnimationType != AnimationType.Blend)
                return false;

            if (movement.MovementType != MovementType.Directional)
                return false;

            if (movement.RotationType != RotationType.Pivot)
                return false;

            if (movement.DriverType != DriverType.CharacterController)
                return false;

            return true;
        }

        public static bool ToDirectionalFollowMouse(this Movement movement)
        {
            if (movement.AnimationType != AnimationType.Blend)
                return false;

            if (movement.MovementType != MovementType.Directional)
                return false;

            if (movement.RotationType != RotationType.FollowMouse)
                return false;

            if (movement.MoveState != MoveState.Move)
                return false;
                    
            return true;
        }

        public static bool ToNavMeshMoveToTarget(this Movement movement)
        {
            if (movement.AnimationType != AnimationType.Blend)
                return false;

            if (movement.DriverType != DriverType.NavMeshAgent)
                return false;

            if (movement.MovementType != MovementType.MoveToTarget)
                return false;
            
            if (movement.MoveState != MoveState.Move)
                return false;

            return true;
        }
        
        public static bool ToNawMeshPointerClick(this Movement movement)
        {
            if (movement.AnimationType != AnimationType.Blend)
                return false;

            if (movement.DriverType != DriverType.NavMeshAgent)
                return false;
            
            if (movement.MoveState != MoveState.Move)
                return false;

            if (movement.MovementType != MovementType.PointerClick)
                return false;
            
            return true;
        }
    }
}