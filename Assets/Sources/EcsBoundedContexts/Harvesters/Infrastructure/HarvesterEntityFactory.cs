using System.Linq;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Harvesters.Domain;
using Sources.EcsBoundedContexts.Harvesters.Presentation;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.Harvesters.Infrastructure
{
    public class HarvesterEntityFactory : EntityFactory
    {
        public HarvesterEntityFactory(
            ProtoWorld world, 
            MainAspect aspect) 
            : base(world, aspect)
        {
        }

        public ProtoEntity Create(HarvesterView view)
        {
            ref HarvesterEnumStateComponent state = ref Aspect.HarvesterState.NewEntity(out ProtoEntity entity);
            state.State = HarvesterState.Home;
            
            ref HarvesterMovePointComponent movePoint = ref Aspect.HarvesterMovePoint.Add(entity);
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