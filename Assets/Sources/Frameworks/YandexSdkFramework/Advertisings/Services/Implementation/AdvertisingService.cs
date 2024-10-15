using System;
using System.Threading;
using Agava.WebUtility;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Advertisings.Services.Interfaces;

namespace Sources.Frameworks.YandexSdkFramework.Advertisings.Services.Implementation
{
    public class AdvertisingService : IInterstitialAdService, IVideoAdService, IAdvertisingService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;

        private Pause _pause;
        private CancellationTokenSource _cancellationTokenSource;
        private TimeSpan _timeSpan = TimeSpan.FromSeconds(35);

        public AdvertisingService(
            IEntityRepository entityRepository,
            IStorageService storageService)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public bool IsAvailable { get; private set; } = true;

        public void Initialize()
        {
            _pause = _entityRepository.Get<Pause>(ModelId.Pause);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Destroy() =>
            _cancellationTokenSource.Cancel();
        
        public void ShowInterstitial()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (AdBlock.Enabled)
                return;

            if (IsAvailable == false)
                return;

            bool isContinue = false;
            bool isContinueSound = false;

            InterstitialAd.Show(
                () =>
                {
                    if (_pause.IsPaused == false)
                    {
                        isContinue = true;
                        _pause.PauseGame();
                    }

                    if (_pause.IsSoundPaused == false)
                    {
                        isContinueSound = true;
                        _pause.PauseSound();
                    }
                },
                _ =>
                {
                    if (isContinue)
                        _pause.ContinueGame();

                    if (isContinueSound)
                        _pause.ContinueSound();
                    
                    StartTimer(_cancellationTokenSource.Token);
                });
        }

        public void ShowVideo(Action onCloseCallback)
        {
            if (WebApplication.IsRunningOnWebGL == false)
            {
                onCloseCallback?.Invoke();

                return;
            }

            if (AdBlock.Enabled)
            {
                onCloseCallback?.Invoke();

                return;
            }
            
            bool isContinue = false;
            bool isContinueSound = false;

            VideoAd.Show(
                () =>
                {
                    if (_pause.IsPaused == false)
                    {
                        isContinue = true;
                        _pause.PauseGame();
                    }

                    if (_pause.IsSoundPaused == false)
                    {
                        isContinueSound = true;
                        _pause.PauseSound();
                    }
                },
                () =>
                {
                    // _healthBooster.Amount += HealthBoosterConst.BoosterAmount;
                    // _loadService.Save(ModelId.HealthBooster);
                },
                () =>
                {
                    if (isContinue)
                        _pause.ContinueGame();

                    if (isContinueSound)
                        _pause.ContinueSound();

                    onCloseCallback?.Invoke();
                });
        }

        private async void StartTimer(CancellationToken cancellationToken)
        {
            try
            {
                IsAvailable = false;
                await UniTask.Delay(_timeSpan, cancellationToken: cancellationToken);
                IsAvailable = true;
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}