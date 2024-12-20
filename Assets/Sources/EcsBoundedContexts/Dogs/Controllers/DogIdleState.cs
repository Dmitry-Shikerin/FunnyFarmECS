using System;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.Frameworks.StateMachines.ContextStateMachines.Implementation.States;
using Sources.Frameworks.StateMachines.ContextStateMachines.Interfaces.Contexts;

namespace Sources.EcsBoundedContexts.Dogs.Controllers
{
    public class DogIdleState : ContextStateBase
    {
        private readonly MainAspect _mainAspect;
        private readonly AnimalConfig _animalConfig;

        private ProtoEntity _entity;

        public DogIdleState(
            MainAspect mainAspect,
            AnimalConfig animalConfig)
        {
            _mainAspect = mainAspect ?? throw new ArgumentNullException(nameof(mainAspect));
            _animalConfig = animalConfig ?? throw new ArgumentNullException(nameof(animalConfig));
        }

        public override void Enter(object payload = null)
        {
            ref DogComponent dog = ref _mainAspect.Dog.Get(_entity);
            // Debug.Log($"Idle enter");

            if (dog.AnimalState == AnimalState.Idle)
                return;
            
            // Debug.Log($"Idle enter change");
            dog.AnimalState = AnimalState.Idle;
            ref AnimancerEcsComponent animancerEcs = ref _mainAspect.Animancer.Get(_entity);
            animancerEcs.Animancer.Play(_animalConfig.Idle);
        }

        public override void Exit()
        {
        }

        public override void Update(float deltaTime)
        {
            // Debug.Log($"Idle update {_entity.GetHashCode()}");
        }

        public override void BeforeApply(IContext context)
        {
            if (context is not EntityProvider entityProvider)
                throw new InvalidCastException();
            
            _entity = entityProvider.Entity;
            Enter();
            // Debug.Log($" Idle before apply {_entity.GetHashCode()}");
        }
    }
}