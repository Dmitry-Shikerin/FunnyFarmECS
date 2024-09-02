using System.Collections.Generic;
using UnityEngine;

namespace Sources.BoundedContexts.Movements.Domain.Configs
{
    [CreateAssetMenu(menuName = "Configs/MovementStates/Animation", fileName = "AnimationState", order = 51)]
    public class AnimationStateConfig : MovementStateConfig
    {
        public float RotateSpeed;
        [field: SerializeField] public List<AnimationClip> AnimationClips { get; private set; }
    }
}