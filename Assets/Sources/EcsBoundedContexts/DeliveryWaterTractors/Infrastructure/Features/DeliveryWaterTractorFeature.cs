using Sources.EcsBoundedContexts.DeliveryWaterTractors.Controllers;
using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Infrastructure.Features
{
    public class DeliveryWaterTractorFeature : EcsFeature
    {
        public DeliveryWaterTractorFeature(
            IFeatureService featureService,
            DeliveryWaterTractorInitializeSystem initializeSystem,
            DeliveryWaterTractorMoveToPondSystem moveToPondSystem,
            DeliveryWaterTractorPondSystem pondSystem,
            DeliveryWaterTractorMoveToHomeSystem moveToHomeSystem,
            DeliveryWaterTractorHomeSystem homeSystem) 
            : base(featureService)
        {
            AddSystem(initializeSystem);
            AddSystem(moveToPondSystem);
            AddSystem(pondSystem);
            AddSystem(moveToHomeSystem);
            AddSystem(homeSystem);
        }

        protected override void Register()
        {
        }
    }
}