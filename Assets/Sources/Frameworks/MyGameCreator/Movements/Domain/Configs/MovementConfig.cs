using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.BoundedContexts.Movements.Domain.Configs
{
    [CreateAssetMenu(menuName = "Configs/MovementConfig", fileName = "MovementConfig", order = 51)]
    public class MovementConfig : ScriptableObject
    {
        [SerializeField] private List<MovementStateConfig> _movementStates = new List<MovementStateConfig>();

        public IReadOnlyList<MovementStateConfig> MovementStates => _movementStates;
    }
}