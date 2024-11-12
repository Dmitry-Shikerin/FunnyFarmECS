using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.Farmers.Infrastructure
{
    public class FarmerFeature : EcsFeature
    {
        public FarmerFeature(
            IFeatureService featureService) 
            : base(featureService)
        {
        }

        protected override void Register()
        {
        }
    }
}