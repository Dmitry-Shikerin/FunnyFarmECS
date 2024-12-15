using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.Frameworks.GameServices.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.WaterTractors.Domain
{
    [CreateAssetMenu(fileName = nameof(WaterTractorConfig), menuName = "Configs/" + nameof(WaterTractorConfig))]
    public class WaterTractorConfig : Config
    {
        [field: MinMaxSlider(3, 10, true)] 
        [field: SerializeField] public Vector2 HomeIdleTime { get; private set; } = new Vector2(2, 4);        
        [field: MinMaxSlider(3, 10, true)] 
        [field: SerializeField] public Vector2 PondIdleTime { get; private set; } = new Vector2(2, 4);
        [field: Range(2, 15)]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [Range(2, 15)]
        [SerializeField] private float _rotationSpeed = 3f;
        [Range(20, 100)]
        [SerializeField] private int _rotationSpeedMultiplier = 50;
        
        public float RotationSpeed => _rotationSpeed * _rotationSpeedMultiplier;
    }
}