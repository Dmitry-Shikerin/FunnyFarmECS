﻿using System;
using Doozy.Runtime.Signals;
using Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Implementation
{
    public class GameCompletedService : IGameCompletedService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;

        private SignalStream _signalStream;
        //private EnemySpawner _enemySpawner;
        private bool _isCompleted;

        public GameCompletedService(IEntityRepository entityRepository, IStorageService storageService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public event Action GameCompleted;

        public void Initialize()
        {
            // _enemySpawner = _entityRepository.Get<EnemySpawner>(ModelId.EnemySpawner) ?? 
            //                 throw new NullReferenceException(nameof(_enemySpawner));
            // _signalStream = SignalStream.Get(StreamConst.Gameplay, StreamConst.GameCompleted);
            // _enemySpawner.WaveKilled += OnCompleted;
        }

        public void Destroy()
        {
            // _enemySpawner.WaveKilled -= OnCompleted;
        }

        private void OnCompleted()
        {
            if (_isCompleted)
                return;

            // if (_enemySpawner.CurrentWaveNumber != 99) //todo поменять на константу
            //     return;

            _storageService.ClearAll();
            _signalStream.SendSignal(true);
            _isCompleted = true;
            GameCompleted?.Invoke();
        }
    }
}