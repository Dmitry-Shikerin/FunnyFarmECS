using System;
using Sources.BoundedContexts.Movements.Controllers.Changers;
using Sources.BoundedContexts.Movements.Domain.Configs;
using Sources.BoundedContexts.Movements.Domain.Types;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.Frameworks.MyGameCreator.Movements.Controllers.States;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation;
using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation.States;
using Sources.OldBoundedContexts.Movements.Controllers.States;
using Sources.OldBoundedContexts.Movements.Domain.Types;
using Type = System.Type;

namespace Sources.Frameworks.MyGameCreator.Movements.Controllers
{
    public class MovementStateMachine : FiniteStateMachine
    {
        private FiniteState _firstState;
        private MovementView _view;
        private Movement _movement;
        private Type _currentStateType;

        public MovementStateMachine(FiniteState firstState, Movement movement, MovementView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _firstState = firstState ?? throw new ArgumentNullException(nameof(firstState));
            ChangeAnimation(StateId.Default);
        }

        public void StartStateMachine() =>
            Start(_firstState);

        public void StopStateMachine() =>
            Stop();

        public void UpdateStateMachine(float deltaTime) =>
            Update(deltaTime);

        public void ChangeState<T>(StateId stateId)
            where T : IMovementState
        {
            Action action = typeof(T) switch
            { 
                Type type when type == typeof(ActionState) => _movement.SetActionState,
                Type type when type == typeof(DirectionalPivotState) => _movement.SetDirectionalPivotState,
                Type type when type == typeof(NawMeshMoveToTargetState) => _movement.SetNavMeshMoveToTargetState,
                Type type when type == typeof(ActionFollowTargetState) => _movement.SetActionFollowTargetState,
                Type type when type == typeof(DirectionalFollowMouseState) => _movement.SetDirectionalFollowMouseState,
                Type type when type == typeof(NawMeshPointerClickState) => _movement.SetNawMeshPointerClickState,
                _ => throw new ArgumentOutOfRangeException(nameof(stateId), stateId, null)
            };

            if (_currentStateType != typeof(T))
            {
                action?.Invoke();
                _currentStateType = typeof(T);
            }

            ChangeAnimation(stateId);
        }

        public void ChangeState(MovementState movementState, StateId stateId)
        {
            Action action = movementState switch
            { 
                MovementState.Action => _movement.SetActionState,
                MovementState.DirectionalPivot => _movement.SetDirectionalPivotState,
                MovementState.NawMeshMoveToTarget => _movement.SetNavMeshMoveToTargetState,
                MovementState.ActionFollowTarget => _movement.SetActionFollowTargetState,
                MovementState.DirectionalFollowMouse => _movement.SetDirectionalFollowMouseState,
                MovementState.NawMeshPointerClick => _movement.SetNawMeshPointerClickState,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (_movement.MovementState != movementState)
                action?.Invoke();

            ChangeAnimation(stateId);
        }

        public void ChangeAnimation(StateId stateId)
        {
            if (_movement.stateId == stateId && _movement.CurrentStateConfig != null)
                return;
            
            _movement.ChangeAnimation(stateId);
            ApplyAnimations();
        }

        private void ApplyAnimations()
        {
            if (_movement.AnimationType != AnimationType.Blend)
                return;

            _view.MovementAnimation.Play(_movement.GetState<BlendStateConfig>().Transition);
            _view.MovementAnimation.CurrentState.Speed = _movement.CurrentStateConfig.MaxAnimationSpeed;
        }
    }
}