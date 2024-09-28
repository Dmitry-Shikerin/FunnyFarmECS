using Sirenix.OdinInspector;
using Sources.Frameworks.GameServices.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.BoundedContexts.AnimalAnimations.Domain
{
    public class AnimalConfig : Config
    {
        [FoldoutGroup("Animations")]
        [field: SerializeField] public AnimationClip Breathing { get; private set; }
        [field: SerializeField] public AnimationClip Eat { get; private set; }
        [field: SerializeField] public AnimationClip Idle { get; private set; }
        [field: SerializeField] public AnimationClip Walk { get; private set; }
        [field: SerializeField] public AnimationClip Run { get; private set; }
    }
}