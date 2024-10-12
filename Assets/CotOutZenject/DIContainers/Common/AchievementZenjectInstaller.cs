using Zenject;

namespace Sources.App.DIContainers.Common
{
    public class AchievementZenjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.Bind<IAchievementService>().To<AchievementService>().AsSingle();
            
            //Commands
            //Container.Bind<FirstUpgradeAchievementCommand>().AsSingle();
            //Container.Bind<FirstHealthBoosterUsageAchievementCommand>().AsSingle();
            //Container.Bind<ScullsDiggerAchievementCommand>().AsSingle();
            //Container.Bind<MaxUpgradeAchievementCommand>().AsSingle();
        }
    }
}