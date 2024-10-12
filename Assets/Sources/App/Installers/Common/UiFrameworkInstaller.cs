using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sirenix.OdinInspector;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Controllers.Implementation;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Controllers.Implementation.Collectors;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Controllers.Interfaces.Collectors;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Soundies.Infrastructure.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Soundies.Infrastructure.Interfaces;
using Sources.Frameworks.UiFramework.Collectors;
using Sources.Frameworks.UiFramework.Core.Services.Forms.Implementation;
using Sources.Frameworks.UiFramework.Core.Services.Localizations.Implementation;
using Sources.Frameworks.UiFramework.Core.Services.Localizations.Interfaces;
using Sources.Frameworks.UiFramework.Views.Presentations.Implementation;
using UnityEngine;

namespace Sources.App.Installers.Common
{
    public class UiFrameworkInstaller : MonoInstaller
    {
        [Required] [SerializeField] private UiCollector _uiCollector;
        
        public override void InstallBindings(DiContainer container)
        {
            container.Bind(_uiCollector);
            container.BindInterfacesAndSelf<FormService>();
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