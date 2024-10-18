using System;
using System.Collections.Generic;
using System.Reflection;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.Transforms;
using Sources.Trees.Components;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Core
{
    public class MainAspect : ProtoAspectInject
    {
        public readonly ProtoPool<TreeTag> Tree = new ();
        public readonly ProtoPool<JumpEvent> JumpEvent = new ();
        public readonly ProtoPool<SweengingTreeComponent> TreeSwinger = new ();
        public readonly ProtoPool<DogComponent> Dog = new ();
        public readonly ProtoPool<AnimancerComponent> Animancer = new ();
        public readonly ProtoPool<NavMeshComponent> NavMesh = new ();
        public readonly ProtoPool<MovementPointComponent> MovementPoints = new ();
        public readonly ProtoPool<MoveSpeedComponent> MoveSpeed = new ();
        public readonly ProtoPool<AnimalTypeComponent> AnimalType = new ();
        public readonly ProtoPool<AnimalStateComponent> AnimalState = new ();
        public readonly ProtoPool<TransformComponent> Transform = new ();

        public readonly Dictionary<Type, IProtoPool> Pools = new();
    }
}