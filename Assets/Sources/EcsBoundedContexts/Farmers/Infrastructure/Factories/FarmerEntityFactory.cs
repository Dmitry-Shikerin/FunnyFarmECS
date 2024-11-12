using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.Farmers.Presentation;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.Farmers.Infrastructure
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
            
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            
            return new ProtoEntity();
        }
    }
}