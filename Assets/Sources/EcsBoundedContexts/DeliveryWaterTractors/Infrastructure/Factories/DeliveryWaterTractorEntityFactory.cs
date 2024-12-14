using System.Linq;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Presentation;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Infrastructure.Factories
{
    public class DeliveryWaterTractorEntityFactory : EntityFactory
    {
        public DeliveryWaterTractorEntityFactory(
            ProtoWorld world,
            MainAspect aspect) 
            : base(world, aspect)
        {
        }

        public ProtoEntity Create(DeliveryWaterTractorView view)
        {
            ref DeliveryWaterTractorEnumStateComponent state = ref Aspect.DeliveryWaterTractorState.NewEntity(out ProtoEntity entity);
            state.State = DeliveryWaterTractorState.Home;
            
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