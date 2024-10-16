﻿using System;
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
        public readonly ProtoPool<TreeTag> TreePool = new ();
        public readonly ProtoPool<JumpEvent> JumpEventPool = new ();
        public readonly ProtoPool<SweengingTreeComponent> TreeSwingerPool = new ();
        public readonly ProtoPool<DogComponent> DogPool = new ();
        public readonly ProtoPool<AnimancerComponent> AnimancerPool = new ();
        public readonly ProtoPool<NavMeshComponent> NavMeshPool = new ();
        public readonly ProtoPool<MovementPointComponent> MovementPointsPool = new ();
        public readonly ProtoPool<MoveSpeedComponent> MoveSpeedPool = new ();
        public readonly ProtoPool<AnimalTypeComponent> AnimalTypePool = new ();
        public readonly ProtoPool<AnimalStateComponent> AnimalStatePool = new ();
        public readonly ProtoPool<TransformComponent> TransformPool = new ();

        public readonly Dictionary<Type, IProtoPool> Pools = new();
    }
}