using System.Linq;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Presentation;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.WaterTractors.Infrastructure.Factories
{
    public class WaterTractorEntityFactory : EntityFactory
    {
        public WaterTractorEntityFactory(
            ProtoWorld world, 
            MainAspect aspect) 
            : base(world, aspect)
        {
        }
        
        public ProtoEntity Create(WaterTractorView view)
        {
            ref WaterTractorEnumStateComponent state = ref Aspect.WaterTractorState.NewEntity(out ProtoEntity entity);
            state.State = WaterTractorState.Home;
            
            ref WaterTractorMovePointComponent movePoint = ref Aspect.WaterTractorMovePoint.Add(entity);
            movePoint.ToFieldPoints = view.ToFieldPoints.Select(point => point.position).ToArray();
            movePoint.ToHomePoints = view.ToHomePoints.Select(point => point.position).ToArray();
            movePoint.TargetPoint = movePoint.ToFieldPoints[0];
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            ref GameObjectComponent gameObject = ref Aspect.GameObject.Add(entity);
            gameObject.GameObject = view.gameObject;
            
            return entity;
        }
    }
}