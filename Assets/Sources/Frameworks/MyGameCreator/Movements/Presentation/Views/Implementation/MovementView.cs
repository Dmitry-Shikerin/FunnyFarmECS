using Sources.BoundedContexts.Movements.Controllers;
using Sources.BoundedContexts.Movements.Domain.Types;
using Sources.Frameworks.MyGameCreator.Core;
using Sources.Frameworks.MyGameCreator.Movements.Controllers;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.OldBoundedContexts.Movements.Controllers.States;
using Sources.OldBoundedContexts.Movements.Domain.Types;
using Sources.OldBoundedContexts.Movements.Presentation.Views.Implementation;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.BoundedContexts.Movements.Presentation.Views.Implementation
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovementView : MonoBehaviour
    {
        [SerializeField] private bool _isPlayer;
        [SerializeField] private bool _isInitializeFromCore;
        [SerializeField] private Movement _movement;

        private CharacterController _controller;
        private MovementStateMachine _movementStateMachine;
        
        private MyGameCreator MyGameCreator => MyGameCreator.Instance;
        public MovementAnimation MovementAnimation { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public Transform TargetFollow { get; set; }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            MovementAnimation = GetComponentInChildren<MovementAnimation>();
            Agent = GetComponent<NavMeshAgent>();
            _movementStateMachine = MyGameCreator.MovementStateMachineFactory
                .Create(_movement, this);
        }

        private void OnEnable()
        {
            SetState(StateId.Default);
            _movementStateMachine.StartStateMachine();
            MyGameCreator.OnUpdate += OnUpdate;
        }

        private void OnDisable()
        {
            _movementStateMachine.StopStateMachine();
            MyGameCreator.OnUpdate += OnUpdate;
        }

        public void SetLookRotation(float angle, float speed)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, Quaternion.Euler(0, angle, 0), speed);
        }

        public void Move(Vector3 direction) =>
            _controller.Move(direction);

        public void SetState(StateId state) =>
            _movementStateMachine.ChangeAnimation(state);
        
        public void ChangeState(MovementState movementState, StateId stateId) =>
            _movementStateMachine.ChangeState(movementState, stateId);

        public void ChangeState<T>(StateId stateId = StateId.Default)
            where T : IMovementState =>
            _movementStateMachine.ChangeState<T>(stateId);

        private void OnUpdate(float deltaTime) =>
            _movementStateMachine.UpdateStateMachine(deltaTime);
    }
}