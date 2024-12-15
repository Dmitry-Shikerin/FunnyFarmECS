using Sirenix.OdinInspector;
using Sources.Frameworks.GameServices.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Harvesters.Domain
{
    [CreateAssetMenu(fileName = nameof(HarvesterConfig), menuName = "Configs/" + nameof(HarvesterConfig))]
    public class HarvesterConfig : Config
    {
        [field: MinMaxSlider(3, 10, true)] 
        [field: SerializeField] public Vector2 HomeIdleTime { get; private set; } = new Vector2(3, 4);
        [field: MinMaxSlider(3, 10, true)] 
        [field: SerializeField] public Vector2 PondIdleTime { get; private set; } = new Vector2(3, 4);
        [field: Range(2, 15)]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [Range(2, 15)]
        [SerializeField] private float _rotationSpeed = 3f;
        [Range(20, 100)]
        [SerializeField] private int _rotationSpeedMultiplier = 50;
        
        public float RotationSpeed => _rotationSpeed * _rotationSpeedMultiplier;
    }
}