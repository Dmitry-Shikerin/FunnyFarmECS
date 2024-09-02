using Sources.BoundedContexts.Movements.Domain.Types;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.OldBoundedContexts.Movements.Domain.Types;

namespace Sources.BoundedContexts.Movements.Controllers.Changers
{
    public static class MovementStateChanger
    {
        public static void SetDirectionalFollowMouseState(this Movement movement)
        {
            movement.AnimationType = AnimationType.Blend;
            movement.MovementType = MovementType.Directional;
            movement.RotationType = RotationType.FollowMouse;
            movement.MoveState = MoveState.Move;
            movement.MovementState = MovementState.DirectionalFollowMouse;
        }
        
        public static void SetNavMeshMoveToTargetState(this Movement movement)
        {
            movement.DriverType = DriverType.NavMeshAgent;
            movement.AnimationType = AnimationType.Blend;
            movement.MoveState = MoveState.Move;
            movement.MovementState = MovementState.NawMeshMoveToTarget;
        }
        
        public static void SetDirectionalPivotState(this Movement movement)
        {
            movement.AnimationType = AnimationType.Blend;
            movement.MovementType = MovementType.Directional;
            movement.RotationType = RotationType.Pivot;
            movement.DriverType = DriverType.CharacterController;
            movement.MoveState = MoveState.Move;
            movement.MovementState = MovementState.DirectionalPivot;
        }
        
        public static void SetActionState(this Movement movement)
        {
            movement.AnimationType = AnimationType.Action;
            movement.MoveState = MoveState.Idle;
            movement.RotationType = RotationType.Pivot;
            movement.MovementState = MovementState.Action;
        }

        public static void SetActionFollowTargetState(this Movement movement)
        {
            movement.AnimationType = AnimationType.Action;
            movement.MoveState = MoveState.Idle;
            movement.RotationType = RotationType.FollowTarget;
            movement.MovementState = MovementState.ActionFollowTarget;
        }
        
        public static void SetNawMeshPointerClickState(this Movement movement)
        {
            movement.AnimationType = AnimationType.Blend;
            movement.MovementType = MovementType.PointerClick;
            movement.DriverType = DriverType.NavMeshAgent;
            movement.MoveState = MoveState.Move;
            movement.MovementState = MovementState.NawMeshPointerClick;
        }
    }
}