using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation.States;
using Sources.OldBoundedContexts.Movements.Controllers.States;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Movements.Controllers.States
{
    public class ActionState : FiniteState, IMovementState
    {
        public override void Enter()
        {
            Debug.Log(nameof(ActionState));
        }
    }
}