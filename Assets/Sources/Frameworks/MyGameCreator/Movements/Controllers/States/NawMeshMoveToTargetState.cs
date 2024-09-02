using System;
using Sources.BoundedContexts.Movements.Domain.Configs;
using Sources.BoundedContexts.Movements.Infrastructure.Services.Domain.Implementation;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation.States;
using Sources.OldBoundedContexts.Movements.Controllers.States;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Movements.Controllers.States
{
    public class NawMeshMoveToTargetState : FiniteState, IMovementState
    {
        private readonly Movement _movement;
        private readonly MovementView _view;
        private readonly MovementService _movementService;

        public NawMeshMoveToTargetState(
            Movement movement,
            MovementView view,
            MovementService movementService)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _movementService = movementService ?? throw new ArgumentNullException(nameof(movementService));
        }

        public override void Enter()
        {
            Debug.Log(nameof(NawMeshMoveToTargetState));
            ChangeSpeed();
        }

        public override void Exit()
        {
        }

        public override void Update(float deltaTime)
        {
            Move();
        }

        private void Move()
        {
            if (_view.TargetFollow == null)
                return;
            
            _view.Agent.destination = _view.TargetFollow.position;
        }
        
        private void ChangeSpeed()
        {
            _view.Agent.angularSpeed = _movement.GetState<BlendStateConfig>().RotateSpeed * 100f;
            _view.Agent.speed = _movement.GetState<BlendStateConfig>().MaxStandSpeed;
        }
    }
}