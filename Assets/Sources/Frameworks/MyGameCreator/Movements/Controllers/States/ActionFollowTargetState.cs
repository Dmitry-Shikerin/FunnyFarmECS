using System;
using Sources.BoundedContexts.Movements.Domain.Configs;
using Sources.BoundedContexts.Movements.Infrastructure.Services.Domain.Implementation;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.Frameworks.GameServices.InputServices;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation.States;
using Sources.OldBoundedContexts.Movements.Controllers.States;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Movements.Controllers.States
{
    public class ActionFollowTargetState : FiniteState, IMovementState
    {
        private readonly Movement _movement;
        private readonly MovementView _view;
        private readonly NewInputService _inputService;
        private readonly MovementService _movementService;

        public ActionFollowTargetState(
            Movement movement,
            MovementView view,
            NewInputService inputService,
            MovementService movementService)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _movementService = movementService ?? throw new ArgumentNullException(nameof(movementService));
        }

        public override void Enter()
        {
            Debug.Log(nameof(ActionFollowTargetState));
        }

        public override void Update(float deltaTime)
        {
            FollowTargetRotate();
        }

        private void FollowTargetRotate()
        {
            float angle = _movementService.GetAngleRotation(
                _view.transform.position, _view.TargetFollow.position);
            // _movementService.SetAnimationDirection(
            //     _movement, _view.TargetFollow.position, angle, Time.deltaTime);
            // _view.MovementAnimation.SetDirection(_movement.AnimationDirection);

            _view.SetLookRotation(angle, _movement.GetState<AnimationStateConfig>().RotateSpeed);
        }
    }
}