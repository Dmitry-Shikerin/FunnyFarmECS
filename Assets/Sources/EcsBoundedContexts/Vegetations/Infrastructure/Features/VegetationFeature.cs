using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.Vegetations.Infrastructure.Features
{
    public class VegetationFeature : EcsFeature
    {
        public VegetationFeature(
            VegetationInitializeSystem initializeSystem,
            ChangeVegetationStateSystem changeVegetationStateSystem,
            VegetationIdleSystem idleSystem,
            VegetationGrowSystem growSystem,
            IFeatureService featureService) 
            : base(featureService)
        {
            AddSystem(initializeSystem);
            AddSystem(changeVegetationStateSystem);
            AddSystem(idleSystem);
            AddSystem(growSystem);
        }

        protected override void Register()
        {
        }
    }
}