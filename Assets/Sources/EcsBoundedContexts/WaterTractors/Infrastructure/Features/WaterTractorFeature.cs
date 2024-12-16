using Sources.EcsBoundedContexts.WaterTractors.Controllers;
using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.WaterTractors.Infrastructure.Features
{
    public class WaterTractorFeature : EcsFeature
    {
        public WaterTractorFeature(
            IFeatureService featureService,
            WaterTractorInitializeSystem initializeSystem,
            WaterTractorHomeSystem homeSystem,
            WaterTractorMoveToFieldSystem moveToFieldSystem,
            WaterTractorMoveToHomeSystem moveToHomeSystem) 
            : base(featureService)
        {
            AddSystem(initializeSystem);
            AddSystem(homeSystem);
            AddSystem(moveToFieldSystem);
            AddSystem(moveToHomeSystem);
        }

        protected override void Register()
        {
        }
    }
}