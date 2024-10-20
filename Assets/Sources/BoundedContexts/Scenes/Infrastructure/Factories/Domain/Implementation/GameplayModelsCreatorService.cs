﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sources.BoundedContexts.CabbagePatches.Domain;
using Sources.BoundedContexts.Cats.Domain;
using Sources.BoundedContexts.ChikenCorrals.Domain;
using Sources.BoundedContexts.CowPens.Domain;
using Sources.BoundedContexts.Dogs.Domain;
using Sources.BoundedContexts.HealthBoosters.Domain;
using Sources.BoundedContexts.Houses.Domain;
using Sources.BoundedContexts.Inventories.Domain;
using Sources.BoundedContexts.Jeeps.Domain;
using Sources.BoundedContexts.OnionPatches.Domain;
using Sources.BoundedContexts.PigPens.Domain;
using Sources.BoundedContexts.PlayerWallets.Domain.Models;
using Sources.BoundedContexts.PumpkinsPatchs.Domain;
using Sources.BoundedContexts.RabbitPens.Domain;
using Sources.BoundedContexts.Scenes.Domain;
using Sources.BoundedContexts.Stables.Domain;
using Sources.BoundedContexts.TomatoPatchs.Domain;
using Sources.BoundedContexts.Trucks.Domain;
using Sources.BoundedContexts.Tutorials.Domain.Models;
using Sources.BoundedContexts.Upgrades.Domain.Configs;
using Sources.BoundedContexts.Upgrades.Domain.Data;
using Sources.BoundedContexts.Upgrades.Domain.Models;
using Sources.BoundedContexts.Woodsheds.Domain;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Domain.Interfaces;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Models;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories.Domain.Implementation
{
    public class GameplayModelsCreatorService : IGameplayModelsLoaderService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;
        private readonly IAssetCollector _assetCollector;

        public GameplayModelsCreatorService(
            IEntityRepository entityRepository,
            IStorageService storageService,
            IAssetCollector assetCollector)
        {
            _entityRepository = entityRepository ?? 
                                throw new ArgumentNullException(nameof(entityRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _assetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
        }

        public GameplayModel Load()
        {
            //FirstLocation
            PumpkinPatch pumpkinPatch = new PumpkinPatch { Id = ModelId.FirstPumpkinsPatch, };
            _entityRepository.Add(pumpkinPatch);
            TomatoPatch tomatoPatch = new TomatoPatch { Id = ModelId.TomatoPatch, };
            _entityRepository.Add(tomatoPatch);
            ChickenCorral chickenCorral = new ChickenCorral { Id = ModelId.ChickenCorral, };
            _entityRepository.Add(chickenCorral);
            CabbagePatch cabbagePatch = new CabbagePatch { Id = ModelId.CabbagePatch, };
            _entityRepository.Add(cabbagePatch);
            OnionPatch onionPatch = new OnionPatch { Id = ModelId.OnionPatch, };
            _entityRepository.Add(onionPatch);
            Woodshed woodshed = new Woodshed { Id = ModelId.Woodshed, };
            _entityRepository.Add(woodshed);
            House house = new House { Id = ModelId.House, };
            _entityRepository.Add(house);
            Stable stable = new Stable { Id = ModelId.Stable, };
            _entityRepository.Add(stable);
            Truck truck = new Truck { Id = ModelId.Truck, };
            _entityRepository.Add(truck);
            Jeep jeep = new Jeep { Id = ModelId.Jeep, };
            _entityRepository.Add(jeep);
            Dog dog = new Dog { Id = ModelId.Dog, };
            _entityRepository.Add(dog);
            Cat cat = new Cat { Id = ModelId.Cat, };
            _entityRepository.Add(cat);
            
            //SecondLocation
            PigPen pigPen = new PigPen { Id = ModelId.PigPen, };
            _entityRepository.Add(pigPen);
            CowPen cowPen = new CowPen { Id = ModelId.CowPen, };
            _entityRepository.Add(cowPen);
            RabbitPen rabbitPen = new RabbitPen { Id = ModelId.RabbitPen, };
            _entityRepository.Add(rabbitPen);
            
            //Inventory
            Inventory inventory = new Inventory()
            {
                Id = ModelId.Inventory,
                Items = new Dictionary<string, int>()
                {
                    [ModelId.Pumpkin] = 0,
                },
            };
            _entityRepository.Add(inventory);
            
            //PlayerWallet
            PlayerWallet playerWallet = new PlayerWallet()
            {
                Coins = 15,
                Id = ModelId.PlayerWallet,
            };
            _entityRepository.Add(playerWallet);
            
            //Pause
            Pause pause = new Pause();
            _entityRepository.Add(pause);
            
            //Volume
            Volume musicVolume = LoadVolume(ModelId.MusicVolume);
            Volume soundsVolume = LoadVolume(ModelId.SoundsVolume);
            
            //Achievements
            List<Achievement> achievements = LoadAchievements();
            
            //Tutorial
            Tutorial tutorial = CreateTutorial(ModelId.Tutorial);
            
            return new GameplayModel(
                playerWallet,
                musicVolume,
                soundsVolume,
                achievements,
                tutorial);
        }

        private Volume LoadVolume(string key)
        {
            if (_storageService.HasKey(key))
                return _storageService.Load<Volume>(key);

            Volume volume = new Volume()
            {
                Id = key,
            };
            _entityRepository.Add(volume);
            
            return volume;
        }

        private HealthBooster LoadBooster(string key)
        {
            if (_storageService.HasKey(key))
                return _storageService.Load<HealthBooster>(key);

            HealthBooster healthBooster = new HealthBooster()
            {
                Id = key,
            };
            _entityRepository.Add(healthBooster);

            return healthBooster;
        }

        private Upgrade CreateUpgrade(string id)
        {
            UpgradeConfig config = _assetCollector.Get<UpgradeConfigContainer>()
                .UpgradeConfigs
                .First(config => config.Id == id);

            List<RuntimeUpgradeLevel> levels = new List<RuntimeUpgradeLevel>();
            RuntimeUpgradeConfig runtimeConfig = new RuntimeUpgradeConfig()
            {
                Id = config.Id,
                Levels = levels,
            };
            config.Levels.ForEach(level => levels.Add(new RuntimeUpgradeLevel()
            {
                Id = level.Id,
                MoneyPerUpgrade = level.MoneyPerUpgrade,
                CurrentAmount = level.CurrentAmount,
            }));
            Upgrade upgrade = new Upgrade()
            {
                Config = runtimeConfig,
                Levels = levels,
                Id = config.Id,
            };
            _entityRepository.Add(upgrade);

            return upgrade;
        }
        
        private List<Achievement> LoadAchievements()
        {
            List<Achievement> achievements = new List<Achievement>();

            if (_storageService.HasKey(ModelId.FirstUpgradeAchievement))
            {
                foreach (string id in ModelId.GetIds<Achievement>())
                {
                    Achievement achievement = _storageService.Load<Achievement>(id);
                    achievements.Add(achievement);
                }
                
                return achievements;
            }
            
            foreach (string id in ModelId.GetIds<Achievement>())
            {
                Achievement achievement = new Achievement(id);
                _entityRepository.Add(achievement);
                achievements.Add(achievement);
            }
            
            _storageService.Save(ModelId.GetIds<Achievement>());
            
            return achievements;
        }

        private Tutorial CreateTutorial(string key)
        {
            if (_storageService.HasKey(key))
                return _storageService.Load<Tutorial>(key);

            Tutorial tutorial = new Tutorial()
            {
                Id = key,
            };
            _entityRepository.Add(tutorial);
            
            return tutorial;
        }
    }
}