using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.Domain.Interfaces.Entities;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Pauses.Domain.Constants;
using UnityEngine;

namespace Sources.Frameworks.GameServices.Pauses.Services.Implementation
{
    public class Pause : IEntity
    {
        public event Action<bool> PauseChanged;
        public event Action<bool> PauseSoundChanged;

        public string Id => ModelId.Pause;
        public Type Type => GetType();
        
        public int PauseListenersCount { get; private set; }
        public int SoundPauseListenersCount { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsSoundPaused { get; private set; }
        
        public void ContinueSound()
        {
            SoundPauseListenersCount--;

            if (SoundPauseListenersCount > 0)
                return;

            if (SoundPauseListenersCount < 0)
                throw new IndexOutOfRangeException(nameof(SoundPauseListenersCount));

            IsSoundPaused = false;
            PauseSoundChanged?.Invoke(IsSoundPaused);
        }

        public void ContinueGame()
        {
            PauseListenersCount--;

            if (PauseListenersCount > 0)
                return;

            if (PauseListenersCount < 0)
                throw new IndexOutOfRangeException(nameof(PauseListenersCount));

            IsPaused = false;
            PauseChanged?.Invoke(IsPaused);
            Time.timeScale = TimeScaleConst.Max;
        }

        public void PauseSound()
        {
            SoundPauseListenersCount++;
            
            if (SoundPauseListenersCount < 0)
                throw new IndexOutOfRangeException(nameof(SoundPauseListenersCount));

            IsSoundPaused = true;
            PauseSoundChanged?.Invoke(IsSoundPaused);
        }

        public void PauseGame()
        {
            PauseListenersCount++;

            if (PauseListenersCount < 0)
                throw new IndexOutOfRangeException(nameof(PauseListenersCount));

            IsPaused = true;
            PauseChanged?.Invoke(IsPaused);
            Time.timeScale = TimeScaleConst.Min;
        }

        public async UniTask PauseYield(CancellationToken cancellationToken)
        {
            do
            {
                await UniTask.Yield(cancellationToken);
            }
            while (IsPaused);
        }
        
        public async UniTask SoundPauseYield(CancellationToken cancellationToken)
        {
            do
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(0.05f), ignoreTimeScale: true, cancellationToken: cancellationToken);
            }
            while (IsSoundPaused);
        }
    }
}