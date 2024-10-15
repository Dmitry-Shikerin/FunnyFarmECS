using System;
using System.Collections.Generic;
using Sources.BoundedContexts.PlayerWallets.Domain.Models;
using Sources.BoundedContexts.Scenes.Domain;
using Sources.BoundedContexts.Tutorials.Domain.Models;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Domain.Interfaces;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Models;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories.Domain.Implementation
{
    public class GameplayModelsLoaderService : IGameplayModelsLoaderService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;

        public GameplayModelsLoaderService(
            IEntityRepository entityRepository,
            IStorageService storageService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public GameplayModel Load()
        {
            //PlayerWallet
            PlayerWallet playerWallet = _storageService.Load<PlayerWallet>(ModelId.PlayerWallet);
            
            //Volumes
            Volume musicVolume = _storageService.Load<Volume>(ModelId.MusicVolume);
            Volume soundsVolume = _storageService.Load<Volume>(ModelId.SoundsVolume);
            
            //Achievements
            List<Achievement> achievements = new List<Achievement>();
            
            foreach (string id in ModelId.GetIds<Achievement>())
            {
                Achievement achievement = _storageService.Load<Achievement>(id);
                achievements.Add(achievement);
            }
            
            //Tutorial
            Tutorial tutorial = _storageService.Load<Tutorial>(ModelId.Tutorial);
            
            return new GameplayModel(
                playerWallet,
                musicVolume,
                soundsVolume,
                achievements,
                tutorial);
        }
    }
}