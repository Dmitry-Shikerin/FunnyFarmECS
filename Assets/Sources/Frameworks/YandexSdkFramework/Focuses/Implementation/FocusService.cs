using System;
using Agava.WebUtility;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.YandexSdkFramework.Focuses.Implementation
{
    public class FocusService : IFocusService
    {
        private readonly IEntityRepository _entityRepository;
        private Pause _pause;

        public FocusService(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
        }
        
        public void Initialize()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            _pause = _entityRepository.Get<Pause>(ModelId.Pause);
            OnInBackgroundChangeWeb(WebApplication.InBackground);
            OnInBackgroundChangeApp(Application.isFocused);
            
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        public void Destroy()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;
            
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (inApp == false)
            {
                _pause.PauseGame();
                _pause.PauseSound();

                return;
            }

            if (_pause.IsPaused)
                _pause.ContinueGame();

            if (_pause.IsSoundPaused)
                _pause.ContinueSound();
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (isBackground)
            {
                _pause.PauseGame();
                _pause.PauseSound();

                return;
            }

            if (_pause.IsPaused)
                _pause.ContinueGame();

            if (_pause.IsSoundPaused)
                _pause.ContinueSound();
        }
    }
}