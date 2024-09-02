using System;
using Sources.BoundedContexts.Movements.Domain.Configs;
using Sources.BoundedContexts.Movements.Infrastructure.Services.Domain.Implementation;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.Frameworks.GameServices.InputServices;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation.States;
using Sources.Frameworks.Utils.Extensions;
using Sources.OldBoundedContexts.Movements.Controllers.States;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Movements.Controllers.States
{
    public class NawMeshPointerClickState : FiniteState, IMovementState
    {
        private readonly Movement _movement;
        private readonly MovementView _view;
        private readonly NewInputService _inputService;
        private readonly MovementService _movementService;

        public NawMeshPointerClickState(
            Movement movement,
            MovementView movementView,
            NewInputService inputService,
            MovementService movementService)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _view = movementView ?? throw new ArgumentNullException(nameof(movementView));
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _movementService = movementService ?? throw new ArgumentNullException(nameof(movementService));
        }

        public override void Enter()
        {
            Debug.Log(nameof(NawMeshPointerClickState));
            ChangeSpeed();
        }

        public override void Exit()
        {
        }

        public override void Update(float deltaTime)
        {
            NawMeshPointerMove();
            SetSpeed();
            SetAnimationDirection();
        }

        private void NawMeshPointerMove()
        {
            if (_inputService.InputData.PointerPosition == Vector3.zero)
                return;

            _view.Agent.destination = _inputService.InputData.PointerPosition;
        }

        private void ChangeSpeed()
        {
            _view.Agent.angularSpeed = _movement.GetState<BlendStateConfig>().RotateSpeed * 100f;
            _view.Agent.speed = _movement.GetState<BlendStateConfig>().MaxStandSpeed;
        }
        
        private void SetAnimationDirection()
        {
            Vector3 moveDirection = _view.transform.position - _view.Agent.destination;
            float angle = _movementService.GetAngle(
                _view.transform.position, moveDirection);
            _movementService.SetAnimationDirection(
                _movement, moveDirection, angle, Time.deltaTime);
            _view.MovementAnimation.SetDirection(_movement.AnimationDirection);
        }

        private void SetSpeed()
        {
            float speed = Vector3.Distance(
                _view.transform.position, _view.Agent.destination) <= _view.Agent.stoppingDistance + 0.1f
                ? 0 
                : _movement.GetState<BlendStateConfig>().MaxStandSpeed;
            _movementService.SetSpeed(_movement, speed, Time.deltaTime);
            
            Debug.Log($"speed: {speed}");
            
            float animationSpeed = _movement.CurrentSpeed
                .FloatToPercent(_movement.GetState<BlendStateConfig>().MaxStandSpeed)
                .FloatPercentToUnitPercent();
            _view.MovementAnimation.SetSpeed(animationSpeed);
        }
    }
}