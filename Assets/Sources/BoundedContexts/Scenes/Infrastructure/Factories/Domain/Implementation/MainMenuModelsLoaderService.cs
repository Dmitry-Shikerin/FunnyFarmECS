using System;
using System.Collections.Generic;
using Sources.BoundedContexts.HealthBoosters.Domain;
using Sources.BoundedContexts.Scenes.Domain;
using Sources.Frameworks.GameServices.DailyRewards.Domain;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Models;
using UnityEngine;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories.Domain.Implementation
{
    public class MainMenuModelsLoaderService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;

        public MainMenuModelsLoaderService(
            IEntityRepository entityRepository,
            IStorageService storageService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public MainMenuModel Load()
        {
            //Achievements
            List<Achievement> achievements = new List<Achievement>();
            
            foreach (string id in ModelId.GetIds<Achievement>())
            {
                Achievement achievement = _storageService.Load<Achievement>(id);
                achievements.Add(achievement);
            }
            
            //Volumes
            Volume musicVolume = _storageService.Load<Volume>(ModelId.MusicVolume);
            Volume soundsVolume = _storageService.Load<Volume>(ModelId.SoundsVolume);
            
            //DailyReward
            DailyReward dailyReward = _storageService.Load<DailyReward>(ModelId.DailyReward);
            
            //HealthBooster
            HealthBooster healthBooster = _storageService.Load<HealthBooster>(ModelId.HealthBooster);
            
            Debug.Log($"Load models");
            
            return new MainMenuModel(
                musicVolume, 
                soundsVolume,
                dailyReward,
                achievements,
                healthBooster);
        }
    }
}