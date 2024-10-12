using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using MyDependencies.Sources.Lifetimes;
using Sources.Frameworks.GameServices.ServerTimes.Services.Implementation;
using Sources.Frameworks.GameServices.ServerTimes.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Advertisings.Services.Implementation;
using Sources.Frameworks.YandexSdkFramework.Infrastructure.Factories.Controllers;
using Sources.Frameworks.YandexSdkFramework.Infrastructure.Factories.Views;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Implementation;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.PlayerAccounts.Implementation;
using Sources.Frameworks.YandexSdkFramework.PlayerAccounts.Interfaces;
using Sources.Frameworks.YandexSdkFramework.SdcInitializes.Implementation;
using Sources.Frameworks.YandexSdkFramework.SdcInitializes.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Stickies.Implementation;
using Sources.Frameworks.YandexSdkFramework.Stickies.Interfaces;

namespace Sources.App.Installers.Common
{
    public class SdkServicesInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ILeaderboardInitializeService, YandexLeaderboardInitializeService>(LifeTime.Single);
            container.Bind<ILeaderBoardScoreSetter, YandexLeaderBoardScoreSetter>(LifeTime.Single);
            container.Bind<IPlayerAccountAuthorizeService, PlayerAccountAuthorizeService>(LifeTime.Single);
            container.Bind<ISdkInitializeService, SdkInitializeService>(LifeTime.Single);
            container.Bind<IStickyService, StickyService>(LifeTime.Single);
            container.BindInterfaces<AdvertisingService>(LifeTime.Single);
            container.Bind<LeaderBoardElementViewFactory>(LifeTime.Single);
            container.Bind<LeaderBoardElementPresenterFactory>(LifeTime.Single);
            //Todo раскоментировать после релиза
            // Container.Bind<ITimeService>().To<NetworkTimeService>().AsSingle();
            container.Bind<ITimeService, DayTimeService>(LifeTime.Single);
        }
    }
}