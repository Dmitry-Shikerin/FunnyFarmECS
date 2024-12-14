using Sources.EcsBoundedContexts.Timers.Infrastructure;
using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.Commons
{
    public class CommonFeature : EcsFeature
    {
        public CommonFeature(
            IFeatureService featureService,
            TimerSystem timerSystem)
            : base(featureService)
        {
            AddSystem(timerSystem);
        }

        protected override void Register()
        {
        }
    }
}