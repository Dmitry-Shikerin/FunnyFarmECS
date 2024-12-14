using System.Linq;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryCars.Domain;
using Sources.EcsBoundedContexts.DeliveryCars.Presentation;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.DeliveryCars.Infrastructure.Factories
{
    public class DeliveryCarEntityFactory : EntityFactory
    {
        public DeliveryCarEntityFactory(
            ProtoWorld world,
            MainAspect aspect) 
            : base(world, aspect)
        {
        }
        
        public ProtoEntity Create(DeliveryCarView view)
        {
            ref DeliveryCarEnumStateComponent state = ref Aspect.DeliveryCarState.NewEntity(out ProtoEntity entity);
            state.State = DeliveryCarState.HomeIdle;
            
            ref MovementPointComponent movePoint = ref Aspect.MovementPoints.Add(entity);
            movePoint.Points = view.MovePoints.Select(point => point.position).ToArray();
            movePoint.TargetPoint = movePoint.Points[0];
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            ref GameObjectComponent gameObject = ref Aspect.GameObject.Add(entity);
            gameObject.GameObject = view.gameObject;
            
            return new ProtoEntity();
        }
    }
}