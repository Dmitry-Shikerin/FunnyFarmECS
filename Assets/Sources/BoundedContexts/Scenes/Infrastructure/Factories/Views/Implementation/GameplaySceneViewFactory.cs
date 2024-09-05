using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Views;
using Sources.BoundedContexts.ChikenCorrals.Infrastructure;
using Sources.BoundedContexts.Huds.Presentations;
using Sources.BoundedContexts.Jeeps.Infrastructure;
using Sources.BoundedContexts.PlayerWallets.Infrastructure.Factories.Views;
using Sources.BoundedContexts.PumpkinsPatchs.Infrastructure;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Domain;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories.Domain.Implementation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.TomatoPatchs.Infrastructure;
using Sources.BoundedContexts.Upgrades.Infrastructure.Factories.Views;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Domain.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Views.Interfaces;
using Sources.Frameworks.GameServices.Volumes.Infrastucture.Factories;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Configs;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Models;
using Sources.Frameworks.UiFramework.Collectors;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories.Views.Implementation
{
    public class GameplaySceneViewFactory : ISceneViewFactory
    {
        private readonly ILoadService _loadService;
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _entityRepository;
        private readonly GameplayHud _gameplayHud;
        private readonly UiCollectorFactory _uiCollectorFactory;
        private readonly RootGameObject _rootGameObject;
        private readonly AbilityApplierViewFactory _abilityApplierViewFactory;
        private readonly UpgradeViewFactory _upgradeViewFactory;
        private readonly GameplayModelsCreatorService _gameplayModelsCreatorService;
        private readonly GameplayModelsLoaderService _gameplayModelsLoaderService;
        private readonly PlayerWalletViewFactory _playerWalletViewFactory;
        private readonly VolumeViewFactory _volumeViewFactory;
        private readonly PumpkinsPatchViewFactory _pumpkinsPatchViewFactory;
        private readonly ISelectableService _selectableService;
        private readonly TomatoPatchViewFactory _tomatoPatchViewFactory;
        private readonly ChickenCorralViewFactory _chickenCorralViewFactory;
        private readonly JeepViewFactory _jeepViewFactory;

        public GameplaySceneViewFactory(
            ILoadService loadService,
            IAssetCollector assetCollector,
            IEntityRepository entityRepository,
            GameplayHud gameplayHud,
            UiCollectorFactory uiCollectorFactory,
            RootGameObject rootGameObject,
            AbilityApplierViewFactory abilityApplierViewFactory,
            UpgradeViewFactory upgradeViewFactory,
            GameplayModelsCreatorService gameplayModelsCreatorService,
            GameplayModelsLoaderService gameplayModelsLoaderService,
            PlayerWalletViewFactory playerWalletViewFactory,
            VolumeViewFactory volumeViewFactory,
            PumpkinsPatchViewFactory pumpkinsPatchViewFactory,
            ISelectableService selectableService,
            TomatoPatchViewFactory tomatoPatchViewFactory,
            ChickenCorralViewFactory chickenCorralViewFactory,
            JeepViewFactory jeepViewFactory)
        {
            _loadService = loadService ?? throw new ArgumentNullException(nameof(loadService));
            _assetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _gameplayHud = gameplayHud ?? throw new ArgumentNullException(nameof(gameplayHud));
            _uiCollectorFactory = uiCollectorFactory ?? throw new ArgumentNullException(nameof(uiCollectorFactory));
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
            _abilityApplierViewFactory = abilityApplierViewFactory ?? 
                                         throw new ArgumentNullException(nameof(abilityApplierViewFactory));
            _upgradeViewFactory = upgradeViewFactory ?? throw new ArgumentNullException(nameof(upgradeViewFactory));
            _gameplayModelsCreatorService = gameplayModelsCreatorService ?? 
                                            throw new ArgumentNullException(nameof(gameplayModelsCreatorService));
            _gameplayModelsLoaderService = gameplayModelsLoaderService ?? 
                                           throw new ArgumentNullException(nameof(gameplayModelsLoaderService));
            _playerWalletViewFactory = playerWalletViewFactory ??
                                       throw new ArgumentNullException(nameof(playerWalletViewFactory));
            _volumeViewFactory = volumeViewFactory ??
                                       throw new ArgumentNullException(nameof(volumeViewFactory));
            _pumpkinsPatchViewFactory = pumpkinsPatchViewFactory ?? throw new ArgumentNullException(nameof(pumpkinsPatchViewFactory));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
            _tomatoPatchViewFactory = tomatoPatchViewFactory ?? throw new ArgumentNullException(nameof(tomatoPatchViewFactory));
            _chickenCorralViewFactory = chickenCorralViewFactory ?? throw new ArgumentNullException(nameof(chickenCorralViewFactory));
            _jeepViewFactory = jeepViewFactory ?? throw new ArgumentNullException(nameof(jeepViewFactory));
        }

        public void Create(IScenePayload payload)
        {
            GameplayModel gameplayModel = Load(payload);
            
            //PlayerWallet
            _playerWalletViewFactory.Create(_gameplayHud.PlayerWalletView);

            //UiCollector
            _uiCollectorFactory.Create();
            
            //Volume
            _volumeViewFactory.Create(gameplayModel.MusicVolume, _gameplayHud.MusicVolumeView);
            _volumeViewFactory.Create(gameplayModel.SoundsVolume, _gameplayHud.SoundVolumeView);

            //Achievements
            List<Achievement> achievements = _entityRepository
                .GetAll<Achievement>(ModelId.GetIds<Achievement>())
                .ToList();
            
            if (achievements.Count != _gameplayHud.AchievementViews.Count)
                throw new IndexOutOfRangeException(nameof(achievements));
            
            for (int i = 0; i < achievements.Count; i++)
            {
                AchievementConfig config = _assetCollector
                    .Get<AchievementConfigCollector>()
                    .Configs
                    .First(config => config.Id == achievements[i].Id);
                _gameplayHud.AchievementViews[i].Construct(achievements[i], config);
            }
            
            //Pumpkins
            _pumpkinsPatchViewFactory.Create(ModelId.FirstPumpkinsPatch, _rootGameObject.PumpkinPatchView);
            _selectableService.Add(_rootGameObject.PumpkinPatchView);
            
            //Tomatoes
            _tomatoPatchViewFactory.Create(ModelId.TomatoPatch, _rootGameObject.TomatoPatchView);
            _selectableService.Add(_rootGameObject.TomatoPatchView);
            
            //ChickenCorral
            _chickenCorralViewFactory.Create(ModelId.ChickenCorral, _rootGameObject.ChickenCorralView);
            _selectableService.Add(_rootGameObject.ChickenCorralView);
            
            //Jeep
            _jeepViewFactory.Create(ModelId.Jeep, _rootGameObject.JeepView);
            _selectableService.Add(_rootGameObject.JeepView);
        }

        private GameplayModel Load(IScenePayload payload)
        {
            // if (payload != null && payload.CanLoad)
            //     return _gameplayModelsLoaderService.Load();
            if (_loadService.HasKey(ModelId.PlayerWallet))
                return _gameplayModelsLoaderService.Load();
            
            return _gameplayModelsCreatorService.Load();
        }
    }
}