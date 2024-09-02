using System;
using System.Linq;
using Sources.BoundedContexts.Movements.Domain.Configs;
using Sources.BoundedContexts.Movements.Domain.Types;
using Sources.OldBoundedContexts.Movements.Domain.Types;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.Movements.Domain.Models
{
    [Serializable]
    public class Movement
    {
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private MoveState _moveState;
        [SerializeField] private StandState _standState;
        [SerializeField] private MovementType _movementType;
        [SerializeField] private RotationType _rotationType;
        [SerializeField] private DriverType _driverType;
        
        private MovementState _movementState;

        public MovementState MovementState
        {
            get => _movementState;
            set => _movementState = value;
        }

        public DriverType DriverType
        {
            get => _driverType; 
            set => _driverType = value;
        }
        
        public RotationType RotationType
        {
            get => _rotationType; 
            set => _rotationType = value;
        }
        
        public MoveState MoveState
        {
            get => _moveState; 
            set => _moveState = value;
        }

        public StandState StandState
        {
            get => _standState; 
            set => _standState = value;
        }

        public MovementType MovementType
        {
            get => _movementType; 
            set => _movementType = value;
        }

        public StateId stateId { get; set; } = StateId.Default;
        public AnimationType AnimationType { get; set; }
        public MovementStateConfig CurrentStateConfig { get; private set; }
        public event Action AnimationChanged;
        public Vector2 AnimationDirection { get; set; }
        public Vector3 Direction { get; set; }
        public float CurrentSpeed { get; set; }

        public void ChangeAnimation(StateId newState)
        {
            CurrentStateConfig = _movementConfig.MovementStates.First(state => state.StateId == newState);
            AnimationType = CurrentStateConfig.AnimationType;
            stateId = newState;
        }

        public void ChangeStandState()
        {
            if (StandState == StandState.Stand)
                StandState = StandState.Crouch;
            else if (StandState == StandState.Crouch)
                StandState = StandState.Stand;
        }

        public T GetState<T>() where T : MovementStateConfig
        {
            if (CurrentStateConfig == null)
                throw new NullReferenceException();

            if (CurrentStateConfig is not T concrete)
            {
                Debug.Log($"{CurrentStateConfig.AnimationType}");
                Debug.Log($"{CurrentStateConfig.StateId}");
                throw new InvalidCastException(nameof(T));
            }
            
            return concrete;
        }
    }
}