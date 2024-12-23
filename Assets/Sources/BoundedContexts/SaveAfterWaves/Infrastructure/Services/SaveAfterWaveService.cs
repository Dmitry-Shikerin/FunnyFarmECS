﻿using System;
using Doozy.Runtime.Signals;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.BoundedContexts.SaveAfterWaves.Infrastructure.Services
{
    public class SaveAfterWaveService : IInitialize, IDestroy
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;
        private SignalStream _stream;

        public SaveAfterWaveService(
            IEntityRepository entityRepository,
            IStorageService storageService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public void Initialize()
        {
            // _enemySpawner = _entityRepository.Get<EnemySpawner>(ModelId.EnemySpawner);
            // _stream = SignalStream.Get(StreamConst.Gameplay, StreamConst.Saving);
            // _enemySpawner.WaveChanged += OnSave;
        }

        public void Destroy()
        {
            // _enemySpawner.WaveChanged -= OnSave;
        }

        private void OnSave()
        {
            _stream.SendSignal(true);
            _storageService.SaveAll();
        }
    }
}