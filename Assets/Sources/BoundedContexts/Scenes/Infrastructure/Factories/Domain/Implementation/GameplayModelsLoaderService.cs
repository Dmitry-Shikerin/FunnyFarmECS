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
        private readonly ILoadService _loadService;

        public GameplayModelsLoaderService(
            IEntityRepository entityRepository,
            ILoadService loadService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _loadService = loadService ?? throw new ArgumentNullException(nameof(loadService));
        }

        public GameplayModel Load()
        {
            //PlayerWallet
            PlayerWallet playerWallet = _loadService.Load<PlayerWallet>(ModelId.PlayerWallet);
            
            //Volumes
            Volume musicVolume = _loadService.Load<Volume>(ModelId.MusicVolume);
            Volume soundsVolume = _loadService.Load<Volume>(ModelId.SoundsVolume);
            
            //Achievements
            List<Achievement> achievements = new List<Achievement>();
            
            foreach (string id in ModelId.GetIds<Achievement>())
            {
                Achievement achievement = _loadService.Load<Achievement>(id);
                achievements.Add(achievement);
            }
            
            //Tutorial
            Tutorial tutorial = _loadService.Load<Tutorial>(ModelId.Tutorial);
            
            return new GameplayModel(
                playerWallet,
                musicVolume,
                soundsVolume,
                achievements,
                tutorial);
        }
    }
}