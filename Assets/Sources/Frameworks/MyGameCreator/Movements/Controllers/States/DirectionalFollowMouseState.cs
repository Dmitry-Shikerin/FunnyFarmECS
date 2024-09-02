using System;
using Sources.BoundedContexts.Movements.Controllers.Actions;
using Sources.BoundedContexts.Movements.Domain.Configs;
using Sources.BoundedContexts.Movements.Infrastructure.Services.Domain.Implementation;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.Frameworks.GameServices.InputServices;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation.States;
using Sources.Frameworks.Utils.Extensions;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.OldBoundedContexts.Movements.Controllers.States;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Movements.Controllers.States
{
    public class DirectionalFollowMouseState : FiniteState, IMovementState
    {
        private readonly Movement _movement;
        private readonly MovementView _view;
        private readonly MovementService _movementService;
        private IInputService _inputService;
        private readonly StandAction _standAction;

        public DirectionalFollowMouseState(
            Movement movement,
            MovementView view,
            MovementService movementService,
            NewInputService newInputService,
            StandAction standAction)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _movementService = movementService ?? throw new ArgumentNullException(nameof(movementService));
            _inputService = newInputService ?? throw new ArgumentNullException(nameof(newInputService));
            _standAction = standAction ?? throw new ArgumentNullException(nameof(standAction));
        }

        public override void Enter()
        {
            Debug.Log(nameof(DirectionalFollowMouseState));
            _inputService.InputData.StandChanged += _standAction.Execute;
        }

        public override void Update(float dataTime)
        {
            try
            {
                SetSpeed();
                Move();
                SetRotation();
            }
            catch (InvalidOperationException)
            {
            }
        }
        
        public override void Exit() =>
            _inputService.InputData.StandChanged -= _standAction.Execute;

        private void Move()
        {
            _movementService.SetDirection(
                _movement, _inputService.InputData.MoveDirection, Time.deltaTime);
            _view.Move(_movement.Direction);
        }
        
        private void SetSpeed()
        {
            float speed = _inputService.InputData.MoveDirection == Vector3.zero 
                ? 0 
                : _movement.GetState<BlendStateConfig>().MaxStandSpeed;
            _movementService.SetSpeed(_movement, speed, Time.deltaTime);
            
            float animationSpeed = _movement.CurrentSpeed
                .FloatToPercent(_movement.GetState<BlendStateConfig>().MaxStandSpeed)
                .FloatPercentToUnitPercent();
            _view.MovementAnimation.SetSpeed(animationSpeed);
        }

        private void SetRotation()
        {
            if (_inputService.InputData.LookPosition == Vector3.zero)
                return;

            float angle = _movementService.GetAngleRotation(
                _view.transform.position, _inputService.InputData.LookPosition);
            _movementService.SetAnimationDirection(
                _movement, _inputService.InputData.MoveDirection, angle, Time.deltaTime);
            _view.MovementAnimation.SetDirection(_movement.AnimationDirection);

            _view.SetLookRotation(angle, _movement.GetState<BlendStateConfig>().RotateSpeed * 3f);
        }
    }
}