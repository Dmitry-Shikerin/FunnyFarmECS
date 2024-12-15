using Sources.EcsBoundedContexts.Harvesters.Controllers;
using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.Harvesters.Infrastructure
{
    public class HarvesterFeature : EcsFeature
    {
        public HarvesterFeature(
            IFeatureService featureService,
            HarvesterInitializeSystem initializeSystem,
            HarvesterHomeSystem homeSystem,
            HarvesterMoveToFieldSystem moveToFieldSystem,
            HarvesterMoveToHomeSystem moveToHomeSystem) 
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