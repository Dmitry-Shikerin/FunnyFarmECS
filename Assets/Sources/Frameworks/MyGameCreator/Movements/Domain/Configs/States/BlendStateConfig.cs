using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.BoundedContexts.Movements.Domain.Configs
{
    [CreateAssetMenu(menuName = "Configs/MovementStates/Blend", fileName = "BlendState", order = 51)]
    public class BlendStateConfig : MovementStateConfig
    {
        [field: SerializeField] public MovementControllerTransition Transition { get; private set; }
        [Range(0, 7)] 
        [SerializeField] public float _maxStandSpeed = 3;
        [Range(0, 7)] 
        [SerializeField] private float _maxCrouchSpeed = 3;
        
        public float MaxStandSpeed => _maxStandSpeed;
        public float MaxCrouchSpeed => _maxCrouchSpeed;
    }
}