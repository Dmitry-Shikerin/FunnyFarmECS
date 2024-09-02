using Sources.BoundedContexts.Movements.Domain.Types;
using Sources.OldBoundedContexts.Movements.Domain.Types;
using UnityEngine;

namespace Sources.BoundedContexts.Movements.Domain.Configs
{
    public class MovementStateConfig : ScriptableObject
    {
        [field: SerializeField] public StateId StateId { get; private set; } 
        [field: SerializeField] public AnimationType AnimationType { get; private set; }
        [Range(0, 7)] 
        [SerializeField] public float _maxAnimationSpeed = 1;
        [Range(2, 15)] 
        [SerializeField] private float _rotateSpeed = 3f;
        
        public float MaxAnimationSpeed => _maxAnimationSpeed;
        public float RotateSpeed => _rotateSpeed;
    }
}