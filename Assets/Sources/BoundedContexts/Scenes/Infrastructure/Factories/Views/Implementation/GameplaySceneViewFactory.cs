using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Views;
using Sources.BoundedContexts.CabbagePatches.Infrastructure;
using Sources.BoundedContexts.Cats.Infrastructure;
using Sources.BoundedContexts.ChikenCorrals.Infrastructure;
using Sources.BoundedContexts.CowPens.Infrastructure;
using Sources.BoundedContexts.Dogs.Infrastructure;
using Sources.BoundedContexts.GoosePens.Infrastructure;
using Sources.BoundedContexts.Houses.Infrastructure;
using Sources.BoundedContexts.Huds.Presentations;
using Sources.BoundedContexts.Jeeps.Infrastructure;
using Sources.BoundedContexts.OnionPatches.Infrastructure;
using Sources.BoundedContexts.PigPens.Infrastructure;
using Sources.BoundedContexts.PlayerWallets.Infrastructure.Factories.Views;
using Sources.BoundedContexts.PumpkinsPatchs.Infrastructure;
using Sources.BoundedContexts.RabbitPens.Infrastructure;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Domain;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories.Domain.Implementation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.SheepPens.Infrastructure;
using Sources.BoundedContexts.Stables.Implementation;
using Sources.BoundedContexts.TomatoPatchs.Infrastructure;
using Sources.BoundedContexts.Trucks.Infrastructure;
using Sources.BoundedContexts.Upgrades.Infrastructure.Factories.Views;
using Sources.BoundedContexts.Watermills.Infrastructure;
using Sources.BoundedContexts.Woodsheds.Infrastructure;
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
        private readonly OnionPatchViewFactory _onionPatchViewFactory;
        private readonly CabbagePatchViewFactory _cabbagePatchViewFactory;
        private readonly TruckViewFactory _truckViewFactory;
        private readonly DogViewFactory _dogViewFactory;
        private readonly CatViewFactory _catViewFactory;
        private readonly HouseViewFactory _houseViewFactory;
        private readonly WoodshedViewFactory _woodshedViewFactory;
        private readonly StableViewFactory _stableViewFactory;
        private readonly PigPenViewFactory _pigPenViewFactory;
        private readonly CowPenViewFactory _cowPenViewFactory;
        private readonly RabbitPenViewFactory _rabbitPenViewFactory;
        private readonly SheepPenViewFactory _sheepPenViewFactory;
        private readonly GoosePenViewFactory _goosePenViewFactory;
        private readonly WatermillViewFactory _watermillViewFactory;

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
            JeepViewFactory jeepViewFactory,
            OnionPatchViewFactory onionPatchViewFactory,
            CabbagePatchViewFactory cabbagePatchViewFactory,
            TruckViewFactory truckViewFactory,
            DogViewFactory dogViewFactory,
            CatViewFactory catViewFactory,
            HouseViewFactory houseViewFactory,
            WoodshedViewFactory woodshedViewFactory,
            StableViewFactory stableViewFactory,
            PigPenViewFactory pigPenViewFactory,
            CowPenViewFactory cowPenViewFactory,
            RabbitPenViewFactory rabbitPenViewFactory,
            SheepPenViewFactory sheepPenViewFactory,
            GoosePenViewFactory goosePenViewFactory,
            WatermillViewFactory watermillViewFactory)
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
            _onionPatchViewFactory = onionPatchViewFactory ?? throw new ArgumentNullException(nameof(onionPatchViewFactory));
            _cabbagePatchViewFactory = cabbagePatchViewFactory ?? throw new ArgumentNullException(nameof(cabbagePatchViewFactory));
            _truckViewFactory = truckViewFactory ?? throw new ArgumentNullException(nameof(truckViewFactory));
            _dogViewFactory = dogViewFactory ?? throw new ArgumentNullException(nameof(dogViewFactory));
            _catViewFactory = catViewFactory ?? throw new ArgumentNullException(nameof(catViewFactory));
            _houseViewFactory = houseViewFactory ?? throw new ArgumentNullException(nameof(houseViewFactory));
            _woodshedViewFactory = woodshedViewFactory ?? throw new ArgumentNullException(nameof(woodshedViewFactory));
            _stableViewFactory = stableViewFactory ?? throw new ArgumentNullException(nameof(stableViewFactory));
            _pigPenViewFactory = pigPenViewFactory ?? throw new ArgumentNullException(nameof(pigPenViewFactory));
            _cowPenViewFactory = cowPenViewFactory ?? throw new ArgumentNullException(nameof(cowPenViewFactory));
            _rabbitPenViewFactory = rabbitPenViewFactory ?? throw new ArgumentNullException(nameof(rabbitPenViewFactory));
            _sheepPenViewFactory = sheepPenViewFactory ?? throw new ArgumentNullException(nameof(sheepPenViewFactory));
            _goosePenViewFactory = goosePenViewFactory ?? throw new ArgumentNullException(nameof(goosePenViewFactory));
            _watermillViewFactory = watermillViewFactory ?? throw new ArgumentNullException(nameof(watermillViewFactory));
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
            
            //FirstLocation
            _pumpkinsPatchViewFactory.Create(ModelId.FirstPumpkinsPatch, _rootGameObject.PumpkinPatchView);
            _selectableService.Add(_rootGameObject.PumpkinPatchView);
            
            _tomatoPatchViewFactory.Create(ModelId.TomatoPatch, _rootGameObject.TomatoPatchView);
            _selectableService.Add(_rootGameObject.TomatoPatchView);
            
            _chickenCorralViewFactory.Create(ModelId.ChickenCorral, _rootGameObject.ChickenCorralView);
            _selectableService.Add(_rootGameObject.ChickenCorralView);

            _onionPatchViewFactory.Create(ModelId.OnionPatch, _rootGameObject.OnionPatchView);
            _selectableService.Add(_rootGameObject.OnionPatchView);

            _cabbagePatchViewFactory.Create(ModelId.CabbagePatch, _rootGameObject.CabbagePatchView);
            _selectableService.Add(_rootGameObject.CabbagePatchView);
            
            _jeepViewFactory.Create(ModelId.Jeep, _rootGameObject.JeepView);
            _selectableService.Add(_rootGameObject.JeepView);

            _truckViewFactory.Create(ModelId.Truck, _rootGameObject.TruckView);
            _selectableService.Add(_rootGameObject.TruckView);
            
            _dogViewFactory.Create(ModelId.Dog, _rootGameObject.DogView);
            _selectableService.Add(_rootGameObject.DogView);
            
            _catViewFactory.Create(ModelId.Cat, _rootGameObject.CatView);
            _selectableService.Add(_rootGameObject.CatView);

            _houseViewFactory.Create(ModelId.House, _rootGameObject.HouseView);
            _selectableService.Add(_rootGameObject.HouseView);

            _woodshedViewFactory.Create(ModelId.Woodshed, _rootGameObject.WoodshedView);
            _selectableService.Add(_rootGameObject.WoodshedView);

            _stableViewFactory.Create(ModelId.Stable, _rootGameObject.StableView);
            _selectableService.Add(_rootGameObject.StableView);
            
            //SecondLocation
            // _pigPenViewFactory.Create(ModelId.PigPen, _rootGameObject.PigPenView);
            // _selectableService.Add(_rootGameObject.PigPenView);
            //
            // _cowPenViewFactory.Create(ModelId.CowPen, _rootGameObject.CowPenView);
            // _selectableService.Add(_rootGameObject.CowPenView);
            //
            // _rabbitPenViewFactory.Create(ModelId.RabbitPen, _rootGameObject.RabbitPenView);
            // _selectableService.Add(_rootGameObject.RabbitPenView);
            //
            // _sheepPenViewFactory.Create(ModelId.SheepPen, _rootGameObject.SheepPenView);
            // _selectableService.Add(_rootGameObject.SheepPenView);
            //
            // _goosePenViewFactory.Create(ModelId.GoosePen, _rootGameObject.GoosePenView);
            // _selectableService.Add(_rootGameObject.GoosePenView);
            //
            // _watermillViewFactory.Create(ModelId.Watermill, _rootGameObject.WatermillView);
            // _selectableService.Add(_rootGameObject.WatermillView);
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