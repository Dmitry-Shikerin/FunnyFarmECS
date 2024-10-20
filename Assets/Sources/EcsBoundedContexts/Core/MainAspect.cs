using System;
using System.Collections.Generic;
using System.Reflection;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.EntityLinks.Domain;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.Transforms;
using Sources.Trees.Components;

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
        public readonly ProtoPool<AnimalEnumStateComponent> AnimalState = new ();
        public readonly ProtoPool<TransformComponent> Transform = new ();
        public readonly ProtoPool<EntityLinkComponent> EntityLink = new ();
        public readonly ProtoPool<GameObjectComponent> GameObject = new ();

        public readonly Dictionary<Type, IProtoPool> Pools = new();
        
        public override void Init(ProtoWorld world)
        {
            base.Init(world);
            
            FieldInfo[] fields = typeof(MainAspect).GetFields(BindingFlags.Public | BindingFlags.Instance);
            
            foreach (var fieldInfo in fields)
            {
                object value = fieldInfo.GetValue(this);
                
                if (value is IProtoPool pool)
                    Pools.Add(value.GetType(), pool);
            }
            
            AspectExt.Construct(this);
        }
    }
}