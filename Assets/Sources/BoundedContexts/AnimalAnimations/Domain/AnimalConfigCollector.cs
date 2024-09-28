using Sources.Frameworks.GameServices.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.BoundedContexts.AnimalAnimations.Domain
{
    [CreateAssetMenu(fileName = "AnimalConfigCollector", menuName = "Configs/AnimalConfigCollector", order = 51)]
    public class AnimalConfigCollector : ConfigCollector<AnimalConfig>
    {
    }
}