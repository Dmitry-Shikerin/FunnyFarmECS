﻿using System;
using Cysharp.Threading.Tasks;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.SaveAfterWaves.Infrastructure.Services;
using Sources.BoundedContexts.Scenes.Controllers;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.Tutorials.Services.Interfaces;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Controllers.Interfaces.Collectors;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Views.Interfaces;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure;
using Sources.Frameworks.MyGameCreator.SkyAndWeathers.Infrastructure.Services.Implementation;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Interfaces;
using Sources.Frameworks.MyLocalization.Infrastructure.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Advertisings.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories.Controllers.Implementation
{
    public class GameplaySceneFactory : ISceneFactory
    {
        private readonly RootGameObject _rootGameObject;
        private readonly SaveAfterWaveService _saveAfterWaveService;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly ISkyAndWeatherService _skyAndWeatherService;
        //private readonly IAchievementService _achievementService;
        private readonly ISoundyService _soundyService;
        private readonly IEcsGameStartUp _ecsGameStartUp;
        private readonly ISceneViewFactory _sceneViewFactory;
        private readonly IFocusService _focusService;
        private readonly IAdvertisingService _advertisingService;
        private readonly ILocalizationService _localizationService;
        private readonly ITutorialService _tutorialService;
        private readonly ICurtainView _curtainView;
        private readonly ISignalControllersCollector _signalControllersCollector;
        private readonly ICameraService _cameraService;
        private readonly IUpdateService _updateService;
        private readonly ISelectableService _selectableService;
        private readonly IInputServiceUpdater _inputService;
        private readonly ISignalBus _signalBus;
        private readonly IEventBuffer _eventBuffer;

        public GameplaySceneFactory(
            RootGameObject rootGameObject,
            SaveAfterWaveService saveAfterWaveService,
            ICompositeAssetService compositeAssetService,
            ISkyAndWeatherService skyAndWeatherService,
            //IAchievementService achievementService,
            ISoundyService soundyService,
            IEcsGameStartUp ecsGameStartUp,
            ISceneViewFactory gameplaySceneViewFactory,
            IFocusService focusService,
            IAdvertisingService advertisingService,
            ILocalizationService localizationService,
            ITutorialService tutorialService,
            ICurtainView curtainView,
            ISignalControllersCollector signalControllersCollector,
            ICameraService cameraService,
            IUpdateService updateService,
            ISelectableService selectableService,
            IInputServiceUpdater inputService,
            ISignalBus signalBus,
            IEventBuffer eventBuffer)
        {
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
            _saveAfterWaveService = saveAfterWaveService ?? throw new ArgumentNullException(nameof(saveAfterWaveService));
            _compositeAssetService = compositeAssetService ?? 
                                     throw new ArgumentNullException(nameof(compositeAssetService));
            _skyAndWeatherService = skyAndWeatherService ?? throw new ArgumentNullException(nameof(skyAndWeatherService));
            //_achievementService = achievementService ?? throw new ArgumentNullException(nameof(achievementService));
            _tutorialService = tutorialService ?? throw new ArgumentNullException(nameof(tutorialService));
            _soundyService = soundyService ?? throw new ArgumentNullException(nameof(soundyService));
            _ecsGameStartUp = ecsGameStartUp ?? throw new ArgumentNullException(nameof(ecsGameStartUp));
            _sceneViewFactory = gameplaySceneViewFactory ?? 
                                throw new ArgumentNullException(nameof(gameplaySceneViewFactory));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _advertisingService = advertisingService ?? throw new ArgumentNullException(nameof(advertisingService));
            _localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
            _curtainView = curtainView ?? throw new ArgumentNullException(nameof(curtainView));
            _signalControllersCollector = signalControllersCollector ?? 
                                          throw new ArgumentNullException(nameof(signalControllersCollector));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _signalBus = signalBus ?? throw new ArgumentNullException(nameof(signalBus));
            _eventBuffer = eventBuffer;
        }

        public UniTask<IScene> Create(object payload)
        {
            IScene gameplayScene = new GameplayScene(
                _rootGameObject,
                _saveAfterWaveService,
                _compositeAssetService,
                _skyAndWeatherService,
                //_achievementService,
                _soundyService,
                _ecsGameStartUp,
                _sceneViewFactory,
                _focusService,
                _advertisingService,
                _localizationService,
                _tutorialService,
                _curtainView,
                _signalControllersCollector,
                _cameraService,
                _updateService,
                _selectableService,
                _inputService,
                _signalBus,
                _eventBuffer);

            return UniTask.FromResult(gameplayScene);
        }
    }
}