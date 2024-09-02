namespace Sources.BoundedContexts.Movements.Domain.Types
{
    public enum MovementState
    {
        ActionFollowTarget = 0,
        Action = 1,
        DirectionalFollowMouse = 2,
        DirectionalPivot = 3,
        NawMeshPointerClick = 4,
        NawMeshMoveToTarget = 5,
    }
}