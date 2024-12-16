using Sources.EcsBoundedContexts.DeliveryCars.Controllers;
using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.DeliveryCars.Infrastructure.Features
{
    public class DeliveryCarFeature : EcsFeature
    {
        public DeliveryCarFeature(
            IFeatureService featureService,
            DeliveryCarInitializeSystem initializeSystem,
            DeliveryCarMoveToExitSystem moveToExitSystem,
            DeliveryCarMoveToHomeSystem moveToHomeSystem,
            DeliveryCarExitIdleSystem exitIdleSystem,
            DeliveryCarHomeIdleSystem homeIdleSystem)
        
            : base(featureService)
        {
            AddSystem(initializeSystem);
            AddSystem(moveToExitSystem);
            AddSystem(moveToHomeSystem);
            AddSystem(exitIdleSystem);
            AddSystem(homeIdleSystem);
        }

        protected override void Register()
        {
        }
    }
}