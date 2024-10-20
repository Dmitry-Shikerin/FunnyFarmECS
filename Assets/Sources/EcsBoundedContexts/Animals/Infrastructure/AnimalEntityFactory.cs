using System;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animals.Presentation;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.EntityLinks.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Animals.Infrastructure
{
    public class AnimalEntityFactory
    {
        private readonly ProtoWorld _world;
        private readonly MainAspect _aspect;

        public AnimalEntityFactory(ProtoWorld world, MainAspect aspect)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
            _aspect = aspect ?? throw new ArgumentNullException(nameof(aspect));
        }

        public ProtoEntity Create(AnimalView view)
        {
            ref AnimalStateComponent state = ref _aspect.AnimalState.NewEntity(out ProtoEntity entity);
            state.CurrentState = AnimalState.ChangeState;
            view.EntityLink.Construct(entity, _aspect, _world);
            
            ref EntityLinkComponent entityLink = ref _aspect.EntityLink.Add(entity);
            entityLink.EntityLink = view.EntityLink;
            entityLink.EntityId = entity.GetHashCode();
            ref AnimancerComponent animancer = ref _aspect.Animancer.Add(entity);
            animancer.Animancer = view.Animancer;
            ref AnimalTypeComponent animalType = ref _aspect.AnimalType.Add(entity);
            animalType.AnimalType = view.AnimalType;
            ref NavMeshComponent navMesh = ref _aspect.NavMesh.Add(entity);
            navMesh.Agent = view.Agent;
            ref MoveSpeedComponent moveSpeed = ref _aspect.MoveSpeed.Add(entity);
            moveSpeed.Current = 3f;
            ref MovementPointComponent movementPoint = ref _aspect.MovementPoints.Add(entity);
            movementPoint.TargetPoint = Vector3.zero;
            ref TransformComponent transform = ref _aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            
            return new ProtoEntity();
        }
    }
}