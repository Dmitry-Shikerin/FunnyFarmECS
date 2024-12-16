using System;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animals.Presentation;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.EntityLinks.Domain;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Animals.Infrastructure
{
    public class AnimalEntityFactory : EntityFactory
    {
        public AnimalEntityFactory(ProtoWorld world, MainAspect aspect) 
            : base(world, aspect)
        {
        }

        public ProtoEntity Create(AnimalView view)
        {
            ref AnimalEnumStateComponent enumState = ref Aspect.AnimalState.NewEntity(out ProtoEntity entity);
            enumState.State = AnimalState.ChangeState;
            view.EntityLink.Construct(entity, Aspect, World);
            
            ref GameObjectComponent gameObject = ref Aspect.GameObject.Add(entity);
            gameObject.GameObject = view.gameObject;
            ref EntityLinkComponent entityLink = ref Aspect.EntityLink.Add(entity);
            entityLink.EntityLink = view.EntityLink;
            entityLink.EntityId = entity.GetHashCode();
            ref AnimancerEcsComponent animancerEcs = ref Aspect.Animancer.Add(entity);
            animancerEcs.Animancer = view.Animancer;
            ref AnimalTypeComponent animalType = ref Aspect.AnimalType.Add(entity);
            animalType.AnimalType = view.AnimalType;
            ref NavMeshComponent navMesh = ref Aspect.NavMesh.Add(entity);
            navMesh.Agent = view.Agent;
            ref MoveSpeedComponent moveSpeed = ref Aspect.MoveSpeed.Add(entity);
            moveSpeed.MoveSpeed = 3f;
            ref TargetPointComponent movementPoint = ref Aspect.TargetPoint.Add(entity);
            movementPoint.Value = Vector3.zero;
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            
            return new ProtoEntity();
        }
    }
}