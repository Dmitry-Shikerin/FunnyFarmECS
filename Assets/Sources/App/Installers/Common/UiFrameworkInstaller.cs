using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Controllers.Implementation;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Controllers.Implementation.Collectors;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Controllers.Interfaces.Collectors;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure;
using Sources.Frameworks.MyLocalization.Infrastructure.Services.Implementation;
using Sources.Frameworks.MyLocalization.Infrastructure.Services.Interfaces;

namespace Sources.App.Installers.Common
{
    public class UiFrameworkInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ILocalizationService, LocalizationService>();
            
            //Soundy
            container.Bind<ISoundyService, SoundyService>();

            //SignalControllers
            container.Bind<ISignalControllersCollector, SignalControllerCollector>();
            container.Bind<ButtonsCommandSignalController>();
            container.Bind<ViewCommandSignalController>();
            
            //Buttons
            container.Bind<UnPauseButtonCommand>();
            container.Bind<PauseButtonCommand>();
            container.Bind<ShowRewardedAdvertisingButtonCommand>();
            container.Bind<NewGameCommand>();
            container.Bind<LoadGameCommand>();
            container.Bind<ShowLeaderboardCommand>();
            container.Bind<CompleteTutorialCommand>();
            container.Bind<LoadMainMenuSceneCommand>();
            container.Bind<ClearSavesButtonCommand>();
            container.Bind<SaveVolumeButtonCommand>();
            container.Bind<ShowDailyRewardViewCommand>();
            container.Bind<PlayerAccountAuthorizeButtonCommand>();

            //Views
            container.Bind<UnPauseCommand>();
            container.Bind<PauseCommand>();
            container.Bind<SaveVolumeCommand>();
            container.Bind<ClearSavesCommand>();
        }
    }
}