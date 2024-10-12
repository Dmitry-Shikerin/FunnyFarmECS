using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
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
            container.Bind<ILeaderboardInitializeService, YandexLeaderboardInitializeService>();
            container.Bind<ILeaderBoardScoreSetter, YandexLeaderBoardScoreSetter>();
            container.Bind<IPlayerAccountAuthorizeService, PlayerAccountAuthorizeService>();
            container.Bind<ISdkInitializeService, SdkInitializeService>();
            container.Bind<IStickyService, StickyService>();
            container.BindInterfaces<AdvertisingService>();
            container.Bind<LeaderBoardElementViewFactory>();
            container.Bind<LeaderBoardElementPresenterFactory>();
            //Todo раскоментировать после релиза
            // Container.Bind<ITimeService>().To<NetworkTimeService>().AsSingle();
            container.Bind<ITimeService, DayTimeService>();
        }
    }
}