using Sources.EcsBoundedContexts.MoveAlongPaths.Controllers;
using Sources.EcsBoundedContexts.NavMeshes.Controllers;
using Sources.EcsBoundedContexts.Timers.Infrastructure;
using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.Commons
{
    public class CommonFeature : EcsFeature
    {
        public CommonFeature(
            IFeatureService featureService,
            TimerSystem timerSystem,
            MoveAlongPathSystem moveAlongPathSystem,
            NavMeshMoveSystem navMeshMoveSystem)
            : base(featureService)
        {
            AddSystem(timerSystem);
            AddSystem(moveAlongPathSystem);
            AddSystem(navMeshMoveSystem);
        }

        protected override void Register()
        {
        }
    }
}