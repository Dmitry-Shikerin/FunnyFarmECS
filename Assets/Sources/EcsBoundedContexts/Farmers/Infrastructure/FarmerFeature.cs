using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.Farmers.Infrastructure
{
    public class FarmerFeature : EcsFeature
    {
        public FarmerFeature(
            IFeatureService featureService,
            FarmerInitializeSystem farmerInitializeSystem,
            FarmerIdleSystem farmerIdleSystem,
            FarmerWorkSystem farmerWorkSystem,
            FarmerMoveSystem farmerMoveSystem)
            : base(featureService)
        {
            AddSystem(farmerInitializeSystem);
            AddSystem(farmerIdleSystem);
            AddSystem(farmerWorkSystem);
            AddSystem(farmerMoveSystem);
        }

        protected override void Register()
        {
        }
    }
}