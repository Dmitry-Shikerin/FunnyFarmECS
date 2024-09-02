using Animancer;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Animations.Presentations;
using Sources.BoundedContexts.Movements.Domain.Configs;
using UnityEngine;

namespace Sources.OldBoundedContexts.Movements.Presentation.Views.Implementation
{
    public class MovementAnimation : AnimationViewBase
    {
        [Required] [SerializeField] private AnimancerComponent _animancer;
        
        public AnimancerComponent Animancer => _animancer;
        public MovementControllerState CurrentState { get; private set; }

        public void Play(ITransition transition)
        {
            CurrentState = (MovementControllerState)_animancer.Play(transition);
        }
        
        public void SetDirection(Vector2 position)
        {
            CurrentState.ParameterX = position.x;
            CurrentState.ParameterZ = position.y;
        }

        public void SetSpeed(float speed)
        {
            CurrentState.ParameterMovement = speed;
        }

        public void SetStand(float stand)
        {
            CurrentState.ParameterStand = stand;
        }

        private void DestroyController()
        {
            CurrentState?.Destroy();
            CurrentState = null;
        }
    }
}