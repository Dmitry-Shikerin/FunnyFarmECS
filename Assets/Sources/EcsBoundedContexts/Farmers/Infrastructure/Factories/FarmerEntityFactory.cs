using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.EcsBoundedContexts.Farmers.Presentation;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.Farmers.Infrastructure.Factories
{
    public class FarmerEntityFactory : EntityFactory
    {
        public FarmerEntityFactory(
            ProtoWorld world,
            MainAspect aspect) 
            : base(world, aspect)
        {
        }
        
        public ProtoEntity Create(FarmerView view)
        {
            ref FarmerEnumStateComponent state = ref Aspect.FarmerState.NewEntity(out ProtoEntity entity);
            state.State = FarmerState.Idle;
            
            ref FarmerMovePointComponent movePoint = ref Aspect.FarmerMovePoint.Add(entity);
            movePoint.Points = view.MovePoints.ToArray();
            movePoint.TargetPoint = movePoint.Points[0];
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            ref AnimancerEcsComponent animancer = ref Aspect.Animancer.Add(entity);
            animancer.Animancer = view.Animancer;
            ref NavMeshComponent navMesh = ref Aspect.NavMesh.Add(entity);
            navMesh.Value = view.Agent; 
            
            return new ProtoEntity();
        }
    }
}