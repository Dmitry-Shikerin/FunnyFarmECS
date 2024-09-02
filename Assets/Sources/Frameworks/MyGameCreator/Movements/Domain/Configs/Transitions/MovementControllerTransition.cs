using Animancer;
using UnityEngine;

namespace Sources.BoundedContexts.Movements.Domain.Configs
{
    [CreateAssetMenu(fileName = "MovementControllerTransition", menuName = "Configs/MovementControllerTransition", order = 0)]
    public class MovementControllerTransition : AnimancerTransition<MovementControllerState.Transition>
    {
    }
}