using System;
using Animancer;
using UnityEngine;

namespace Sources.BoundedContexts.Movements.Domain.Configs
{
    public class MovementControllerState : ControllerState
    {
        private ParameterID _ParameterXID;

        public ParameterID ParameterXID
        {
            get => _ParameterXID;
            set
            {
                _ParameterXID = value;
                _ParameterXID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            }
        }

        public float ParameterX
        {
            get => Playable.GetFloat(_ParameterXID.Hash);
            set => Playable.SetFloat(_ParameterXID.Hash, value);
        }
        
        private ParameterID _ParameterYID;

        public ParameterID ParameterYID
        {
            get => _ParameterYID;
            set
            {
                _ParameterYID = value;
                _ParameterYID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            }
        }

        public float ParameterY
        {
            get => Playable.GetFloat(_ParameterYID.Hash);
            set => Playable.SetFloat(_ParameterYID.Hash, value);
        }
        
        private ParameterID _ParameterZID;

        public ParameterID ParameterZID
        {
            get => _ParameterZID;
            set
            {
                _ParameterZID = value;
                _ParameterZID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            }
        }

        public float ParameterZ
        {
            get => Playable.GetFloat(_ParameterZID.Hash);
            set => Playable.SetFloat(_ParameterZID.Hash, value);
        }

        public Vector3 Parameter
        {
            get => new Vector3(ParameterX, ParameterY, ParameterZ);
            set
            {
                ParameterX = value.x;
                ParameterY = value.y;
                ParameterZ = value.z;
            }
        }
        
        private ParameterID _parameterMovementID;
        
        public ParameterID ParameterMovementID
        {
            get => _parameterMovementID;
            set
            {
                _parameterMovementID = value;
                _parameterMovementID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            }
        }
        
        public float ParameterMovement
        {
            get => Playable.GetFloat(_parameterMovementID.Hash);
            set => Playable.SetFloat(_parameterMovementID.Hash, value);
        }        
        
        private ParameterID _parameterPivotID;
        
        public ParameterID ParameterPivotID
        {
            get => _parameterPivotID;
            set
            {
                _parameterPivotID = value;
                _parameterPivotID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            }
        }
        
        public float ParameterPivot
        {
            get => Playable.GetFloat(_parameterPivotID.Hash);
            set => Playable.SetFloat(_parameterPivotID.Hash, value);
        }        
        
        private ParameterID _parameterStandID;
        
        public ParameterID ParameterStandID
        {
            get => _parameterStandID;
            set
            {
                _parameterStandID = value;
                _parameterStandID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            }
        }
        
        public float ParameterStand
        {
            get => Playable.GetFloat(_parameterStandID.Hash);
            set => Playable.SetFloat(_parameterStandID.Hash, value);
        }        
        
        private ParameterID _parameterGroundedID;
        
        public ParameterID ParameterGroundedID
        {
            get => _parameterGroundedID;
            set
            {
                _parameterGroundedID = value;
                _parameterGroundedID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            }
        }
        
        public float ParameterGrounded
        {
            get => Playable.GetFloat(_parameterGroundedID.Hash);
            set => Playable.SetFloat(_parameterGroundedID.Hash, value);
        }
        
        public MovementControllerState(
            RuntimeAnimatorController controller, 
            ParameterID parameterX,
            ParameterID parameterY,
            ParameterID parameterZ,
            ParameterID movement,
            ParameterID pivot,
            ParameterID stand,
            ParameterID grounded,
            bool keepStateOnStop = false) 
            : base(controller, keepStateOnStop)
        {
            _ParameterXID = parameterX;
            _ParameterXID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);

            _ParameterYID = parameterY;
            _ParameterYID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);

            _ParameterZID = parameterZ;
            _ParameterZID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);

            ParameterMovementID = movement;
            ParameterMovementID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            
            ParameterPivotID = pivot;
            ParameterPivotID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            
            ParameterStandID = stand;
            ParameterStandID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
            
            ParameterGroundedID = grounded;
            ParameterGroundedID.ValidateHasParameter(Controller, AnimatorControllerParameterType.Float);
        }

        public override int ParameterCount => 6;
        
        public override int GetParameterHash(int index)
        {
            switch (index)
            {
                case 0: return _ParameterXID;
                case 1: return _ParameterYID;
                case 2: return _ParameterZID;
                case 3: return ParameterMovementID;
                case 4: return ParameterPivotID;
                case 5: return ParameterStandID;
                case 6: return ParameterGroundedID;
                default: throw new ArgumentOutOfRangeException(nameof(index));
            };
        }
        
        [Serializable]
        public new class Transition : Transition<MovementControllerState>
        {
            [SerializeField]
            private string _ParameterNameX;

            public ref string ParameterNameX => ref _ParameterNameX;

            [SerializeField]
            private string _ParameterNameY;

            public ref string ParameterNameY => ref _ParameterNameY;

            [SerializeField]
            private string _ParameterNameZ;

            public ref string ParameterNameZ => ref _ParameterNameZ;
            
            [SerializeField]
            private string _ParameterNameMovement;

            public ref string ParameterNameMovement => ref _ParameterNameMovement;
            
            [SerializeField]
            private string _ParameterNamePivot;

            public ref string ParameterNamePivot => ref _ParameterNamePivot;
            
            [SerializeField]
            private string _ParameterNameStand;

            public ref string ParameterNameStand => ref _ParameterNameStand;
            
            [SerializeField]
            private string _ParameterNameGrounded;

            public ref string ParameterNameGrounded => ref _ParameterNameGrounded;

            public Transition() { }

            public Transition(
                RuntimeAnimatorController controller,
                string parameterNameX, 
                string parameterNameY, 
                string parameterNameZ,
                string parameterNameMovement)
            {
                Controller = controller;
                _ParameterNameX = parameterNameX;
                _ParameterNameY = parameterNameY;
                _ParameterNameZ = parameterNameZ;
                _ParameterNameMovement = parameterNameMovement;
            }

            public override MovementControllerState CreateState()
                => State = new MovementControllerState(
                    Controller, 
                    _ParameterNameX, 
                    _ParameterNameY, 
                    _ParameterNameZ, 
                    _ParameterNameMovement,
                    _ParameterNamePivot,
                    _ParameterNameStand,
                    _ParameterNameGrounded,
                    KeepStateOnStop);

            [UnityEditor.CustomPropertyDrawer(typeof(Transition), true)]
            public class Drawer : ControllerState.Transition.Drawer
            {
                public Drawer() 
                    : base(
                        nameof(_ParameterNameX), 
                        nameof(_ParameterNameY), 
                        nameof(_ParameterNameZ),
                        nameof(_ParameterNameMovement),
                        nameof(_ParameterNamePivot),
                        nameof(_ParameterNameStand),
                        nameof(_ParameterNameGrounded)) { }
            }
        }

    }
}