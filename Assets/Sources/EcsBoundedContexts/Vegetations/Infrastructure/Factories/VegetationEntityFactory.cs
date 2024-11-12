using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Scales;
using Sources.EcsBoundedContexts.Vegetations.Domain;
using Sources.EcsBoundedContexts.Vegetations.Presentation;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.Vegetations.Infrastructure.Factories
{
    public class VegetationEntityFactory : EntityFactory
    {
        public VegetationEntityFactory(
            ProtoWorld world, 
            MainAspect aspect) 
            : base(world, aspect)
        {
        }

        public ProtoEntity Create(VegetationView view)
        {
            ref VegetationEnumStateComponent vegetationState = ref Aspect.VegetationState.NewEntity(out ProtoEntity entity);
            vegetationState.State = view.State;
            vegetationState.Type = view.Type;
            view.EntityLink.Construct(entity, Aspect, World);
            
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            ref ScaleComponent scale = ref Aspect.Scale.Add(entity);
            scale.TargetScale = view.gameObject.transform.localScale;
            
            return new ProtoEntity();
        }
    }
}