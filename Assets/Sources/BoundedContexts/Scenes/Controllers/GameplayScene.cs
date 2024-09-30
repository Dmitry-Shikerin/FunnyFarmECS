using System;
using MyAudios.MyUiFramework.Utils.Soundies.Domain.Constant;
using Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Interfaces;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.SaveAfterWaves.Infrastructure.Services;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.TestSignals;
using Sources.BoundedContexts.Tutorials.Services.Interfaces;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Controllers.Interfaces.Collectors;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Domain.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Views.Interfaces;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Soundies.Infrastructure.Interfaces;
using Sources.Frameworks.MyGameCreator.Achievements.Infrastructure.Services.Interfaces;
using Sources.Frameworks.MyGameCreator.SkyAndWeathers.Infrastructure.Services.Implementation;
using Sources.Frameworks.UiFramework.Core.Services.Localizations.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Advertisings.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Scenes.Controllers
{
    public class GameplayScene : IScene
    {
        private readonly SaveAfterWaveService _saveAfterWaveService;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly ISkyAndWeatherService _skyAndWeatherService;
        private readonly IAchievementService _achievementService;
        private readonly ISoundyService _soundyService;
        private readonly IEcsGameStartUp _ecsGameStartUp;
        private readonly ISceneViewFactory _gameplaySceneViewFactory;
        private readonly IFocusService _focusService;
        private readonly IAdvertisingService _advertisingService;
        private readonly ILocalizationService _localizationService;
        private readonly ITutorialService _tutorialService;
        private readonly IGameCompletedService _gameCompletedService;
        private readonly ICurtainView _curtainView;
        private readonly ISignalControllersCollector _signalControllersCollector;
        private readonly ICameraService _cameraService;
        private readonly IUpdateService _updateService;
        private readonly ISelectableService _selectableService;
        private readonly IInputServiceUpdater _inputService;
        private readonly ISignalBus _signalBus;

        public GameplayScene(
            RootGameObject rootGameObject,
            SaveAfterWaveService saveAfterWaveService,
            ICompositeAssetService compositeAssetService,
            ISkyAndWeatherService skyAndWeatherService,
            IAchievementService achievementService,
            ISoundyService soundyService,
            IEcsGameStartUp ecsGameStartUp,
            ISceneViewFactory gameplaySceneViewFactory,
            IFocusService focusService,
            IAdvertisingService advertisingService,
            ILocalizationService localizationService,
            ITutorialService tutorialService,
            IGameCompletedService gameCompletedService,
            ICurtainView curtainView,
            ISignalControllersCollector signalControllersCollector,
            ICameraService cameraService,
            IUpdateService updateService,
            ISelectableService selectableService,
            IInputServiceUpdater inputService,
            ISignalBus signalBus)
        {
            _saveAfterWaveService = saveAfterWaveService ?? throw new ArgumentNullException(nameof(saveAfterWaveService));
            _compositeAssetService = compositeAssetService ?? throw new ArgumentNullException(nameof(compositeAssetService));
            _skyAndWeatherService = skyAndWeatherService ?? throw new ArgumentNullException(nameof(skyAndWeatherService));
            _achievementService = achievementService ?? throw new ArgumentNullException(nameof(achievementService));
            _tutorialService = tutorialService ?? throw new ArgumentNullException(nameof(tutorialService));
            _soundyService = soundyService ?? throw new ArgumentNullException(nameof(soundyService));
            _ecsGameStartUp = ecsGameStartUp ?? throw new ArgumentNullException(nameof(ecsGameStartUp));
            _gameplaySceneViewFactory = gameplaySceneViewFactory ?? 
                                        throw new ArgumentNullException(nameof(gameplaySceneViewFactory));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _advertisingService = advertisingService ?? 
                                  throw new ArgumentNullException(nameof(advertisingService));
            _localizationService = localizationService ?? 
                                   throw new ArgumentNullException(nameof(localizationService));
            _gameCompletedService = gameCompletedService ??
                                    throw new ArgumentNullException(nameof(gameCompletedService));
            _curtainView = curtainView ?? throw new ArgumentNullException(nameof(curtainView));
            _signalControllersCollector = signalControllersCollector ?? 
                                          throw new ArgumentNullException(nameof(signalControllersCollector));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _signalBus = signalBus ?? throw new ArgumentNullException(nameof(signalBus));
        }

        public async void Enter(object payload = null)
        {
            _inputService.Initialize();
            _focusService.Initialize();
            await _compositeAssetService.LoadAsync();
            _localizationService.Translate();
            _gameplaySceneViewFactory.Create((IScenePayload)payload);
            _advertisingService.Initialize();
            _achievementService.Initialize();
            _signalControllersCollector.Initialize();
            _soundyService.Initialize();
            _skyAndWeatherService.Initialize();
            _ecsGameStartUp.Initialize();
            await _curtainView.HideAsync();
            _selectableService.Initialize();
            _gameCompletedService.Initialize();
            _saveAfterWaveService.Initialize();
            _signalBus.GetStream<TestSignal>()
                .Subscribe(Test);
            // _tutorialService.Initialize();
            _soundyService.PlaySequence(
                SoundyDBConst.BackgroundMusicDB, SoundyDBConst.GameplaySound);
        }

        public void Exit()
        {
            _signalControllersCollector.Destroy();
            _soundyService.StopSequence(
                SoundyDBConst.BackgroundMusicDB, SoundyDBConst.GameplaySound);
            _soundyService.Destroy();
            _skyAndWeatherService.Destroy();
            _achievementService.Destroy();
            _gameCompletedService.Destroy();
            _saveAfterWaveService.Destroy();
            _compositeAssetService.Release();
            _cameraService.Destroy();
            _ecsGameStartUp.Destroy();
            _selectableService.Destroy();
            _inputService.Destroy();
            _signalBus.Release();
        }

        public void Update(float deltaTime)
        {
            _updateService.Update(deltaTime);
            _ecsGameStartUp.Update(deltaTime);
            _inputService.Update(deltaTime);
            
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log($"Button Pressed");
                _signalBus.Handle(new TestSignal("Jopa"));
            }
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        private void Test(TestSignal signal)
        {
            Debug.Log(signal.Value);
        }
    }
}