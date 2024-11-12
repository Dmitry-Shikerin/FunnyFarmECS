using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.Vegetations.Infrastructure.Features
{
    public class VegetationFeature : EcsFeature
    {
        public VegetationFeature(
            VegetationInitializeSystem initializeSystem,
            VegetationIdleSystem idleSystem,
            VegetationGrowSystem growSystem,
            IFeatureService featureService) 
            : base(featureService)
        {
            AddSystem(initializeSystem);
            AddSystem(idleSystem);
            AddSystem(growSystem);
        }

        protected override void Register()
        {
        }
    }
}