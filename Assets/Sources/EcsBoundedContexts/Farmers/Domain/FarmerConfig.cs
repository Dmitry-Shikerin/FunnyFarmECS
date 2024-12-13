using Animancer;
using Sirenix.OdinInspector;
using Sources.Frameworks.GameServices.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Farmers.Domain
{
    [CreateAssetMenu(fileName = nameof(FarmerConfig), menuName = "Configs/" + nameof(FarmerConfig), order = 51)]
    public class FarmerConfig : Config
    {
        [field: Header("Animations")]
        [field: SerializeField] public ClipTransition Idle { get; private set; } = new ();
        [field: SerializeField] public ClipTransition Move { get; private set; }
        [field: SerializeField] public ClipTransition WorkEnter { get; private set; }
        [field: SerializeField] public ClipTransition WorkLoop { get; private set; }
        [field: PropertySpace(10)]
        [field: Header("Values")]
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: MinMaxSlider(1f, 15f, true)]
        [field: SerializeField] public Vector2 IdleTimeRange { get; private set; } = new (5f, 10f);
        [field: MinMaxSlider(1f, 15f, true)]
        [field: SerializeField] public Vector2 WorkTimeRange { get; private set; } = new (5f, 10f);
    }
}