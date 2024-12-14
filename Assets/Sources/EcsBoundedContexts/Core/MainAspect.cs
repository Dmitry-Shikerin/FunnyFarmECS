using System;
using System.Collections.Generic;
using System.Reflection;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.DeliveryCars.Domain;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.EntityLinks.Domain;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.EcsBoundedContexts.Scales;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.EcsBoundedContexts.Vegetations.Domain;
using Sources.EcsBoundedContexts.Vegetations.Domain.Events;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Transforms;
using Sources.Trees.Components;

namespace Sources.EcsBoundedContexts.Core
{
    public class MainAspect : ProtoAspectInject
    {
        public readonly ProtoPool<EventBufferTag> EventBuffer = new ();
        public readonly ProtoPool<AnimancerEcsComponent> Animancer = new ();
        public readonly ProtoPool<NavMeshComponent> NavMesh = new ();
        public readonly ProtoPool<MovementPointComponent> MovementPoints = new ();
        public readonly ProtoPool<MoveSpeedComponent> MoveSpeed = new ();
        public readonly ProtoPool<TransformComponent> Transform = new ();
        public readonly ProtoPool<EntityLinkComponent> EntityLink = new ();
        public readonly ProtoPool<GameObjectComponent> GameObject = new ();
        public readonly ProtoPool<ScaleComponent> Scale = new();
        public readonly ProtoPool<TimerComponent> Timer = new ();
        //SwingTree
        public readonly ProtoPool<TreeTag> Tree = new ();
        public readonly ProtoPool<SweengingTreeComponent> TreeSwinger = new ();
        //Animals
        public readonly ProtoPool<DogComponent> Dog = new ();
        public readonly ProtoPool<AnimalTypeComponent> AnimalType = new ();
        public readonly ProtoPool<AnimalEnumStateComponent> AnimalState = new ();
        //Vegetations
        public readonly ProtoPool<VegetationEnumStateComponent> VegetationState = new();
        public readonly ProtoPool<ChangeVegetationStateEvent> ChangeVegetationState = new();

        //Farmers
        public readonly ProtoPool<FarmerEnumStateComponent> FarmerState = new ();
        public readonly ProtoPool<FarmerMovePointComponent> FarmerMovePoint = new ();
        
        //DeliveryCars
        public readonly ProtoPool<DeliveryCarEnumStateComponent> DeliveryCarState = new ();
        
        //DeliveryWaterTractor
        public readonly ProtoPool<DeliveryWaterTractorEnumStateComponent> DeliveryWaterTractorState = new ();

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