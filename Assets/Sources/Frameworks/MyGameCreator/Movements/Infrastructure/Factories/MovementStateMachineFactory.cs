using System;
using Sources.BoundedContexts.Movements.Controllers.Actions;
using Sources.BoundedContexts.Movements.Controllers.Conditions;
using Sources.BoundedContexts.Movements.Infrastructure.Services.Domain.Implementation;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.Frameworks.GameServices.InputServices;
using Sources.Frameworks.MyGameCreator.Movements.Controllers;
using Sources.Frameworks.MyGameCreator.Movements.Controllers.States;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation.Transitions;

namespace Sources.Frameworks.MyGameCreator.Movements.Infrastructure.Factories
{
    public class MovementStateMachineFactory
    {
        private readonly NewInputService _inputService;

        public MovementStateMachineFactory(NewInputService inputService)
        {
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
        }

        public MovementStateMachine Create(Movement movement, MovementView view)
        {
            MovementService movementService = new MovementService();
            
            //Actions
            StandAction standAction = new StandAction(
                movement, view);

            //States
            ActionState actionState = new ActionState();
            DirectionalPivotState directionalPivotState = new DirectionalPivotState(
                movement, view, movementService, _inputService, standAction);
            DirectionalFollowMouseState directionalFollowMouseState = new DirectionalFollowMouseState(
                movement,
                view,
                movementService,
                _inputService, 
                standAction);
            NawMeshMoveToTargetState nawMeshMoveToTargetState = new NawMeshMoveToTargetState(
                movement, view, movementService);
            ActionFollowTargetState actionFollowTargetState = new ActionFollowTargetState(
                movement, view, _inputService, movementService);
            NawMeshPointerClickState nawMeshPointerClickState = new NawMeshPointerClickState(
                movement, view, _inputService, movementService);

            //Action
            FiniteTransitionBase toActionTransition = new FiniteTransitionBase(
                actionState, movement.ToAction);
            directionalPivotState.AddTransition(toActionTransition);
            nawMeshMoveToTargetState.AddTransition(toActionTransition);
            actionFollowTargetState.AddTransition(toActionTransition);
            directionalFollowMouseState.AddTransition(toActionTransition);
            nawMeshPointerClickState.AddTransition(toActionTransition);

            //ActionFollowTarget
            FiniteTransitionBase toActionFollowTargetTransition = new FiniteTransitionBase(
                actionFollowTargetState, movement.ToActionFollowTarget);
            actionState.AddTransition(toActionFollowTargetTransition);
            directionalPivotState.AddTransition(toActionFollowTargetTransition);
            actionFollowTargetState.AddTransition(toActionFollowTargetTransition);
            directionalFollowMouseState.AddTransition(toActionFollowTargetTransition);
            nawMeshPointerClickState.AddTransition(toActionFollowTargetTransition);
            
            //Directional
            FiniteTransitionBase toDirectionalPivotTransition = new FiniteTransitionBase(
                directionalPivotState, movement.ToDirectionalPivot);
            actionState.AddTransition(toDirectionalPivotTransition);
            nawMeshMoveToTargetState.AddTransition(toDirectionalPivotTransition);
            actionFollowTargetState.AddTransition(toDirectionalPivotTransition);
            directionalFollowMouseState.AddTransition(toDirectionalPivotTransition);
            nawMeshPointerClickState.AddTransition(toDirectionalPivotTransition);
            
            //DirectionalFollowMouse
            FiniteTransitionBase toDirectionalFollowMouseTransition = 
                new FiniteTransitionBase(directionalFollowMouseState, 
                movement.ToDirectionalFollowMouse);
            actionState.AddTransition(toDirectionalFollowMouseTransition);
            directionalPivotState.AddTransition(toDirectionalFollowMouseTransition);
            actionFollowTargetState.AddTransition(toDirectionalFollowMouseTransition);
            nawMeshMoveToTargetState.AddTransition(toDirectionalFollowMouseTransition);
            nawMeshPointerClickState.AddTransition(toDirectionalFollowMouseTransition);
            
            //NawMeshBlend
            FiniteTransitionBase toNawMeshBlendTransition = new FiniteTransitionBase(
                nawMeshMoveToTargetState, movement.ToNavMeshMoveToTarget);
            directionalPivotState.AddTransition(toNawMeshBlendTransition);
            actionState.AddTransition(toNawMeshBlendTransition);
            directionalFollowMouseState.AddTransition(toNawMeshBlendTransition);
            actionFollowTargetState.AddTransition(toNawMeshBlendTransition);
            nawMeshPointerClickState.AddTransition(toNawMeshBlendTransition);
            
            //NawMeshPointerClick
            FiniteTransitionBase toNawMeshPointerClickTransition = new FiniteTransitionBase(
                nawMeshPointerClickState, movement.ToNawMeshPointerClick);
            actionState.AddTransition(toNawMeshPointerClickTransition);
            directionalPivotState.AddTransition(toNawMeshPointerClickTransition);
            directionalFollowMouseState.AddTransition(toNawMeshPointerClickTransition);
            actionFollowTargetState.AddTransition(toNawMeshPointerClickTransition);
            nawMeshMoveToTargetState.AddTransition(toNawMeshPointerClickTransition);
            
            return new MovementStateMachine(directionalPivotState, movement, view);
        }
    }
}