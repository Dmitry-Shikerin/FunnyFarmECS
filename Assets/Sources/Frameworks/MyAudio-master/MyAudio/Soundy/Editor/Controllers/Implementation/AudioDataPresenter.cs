using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class AudioDataPresenter : IPresenter
    {
        private AudioData _audioData;
        private SoundGroupData _soundGroupData;
        private IAudioDataView _view;
        private readonly PreviewSoundPlayerService _previewSoundPlayerService;

        public AudioDataPresenter(
            AudioData audioData,
            SoundGroupData soundGroupData,
            IAudioDataView view,
            PreviewSoundPlayerService previewSoundPlayerService)
        {
            _audioData = audioData ?? throw new ArgumentNullException(nameof(audioData));
            _soundGroupData = soundGroupData ?? throw new ArgumentNullException(nameof(soundGroupData));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _previewSoundPlayerService = previewSoundPlayerService ?? throw new ArgumentNullException(nameof(previewSoundPlayerService));
        }

        public void Initialize()
        {
            _audioData.IsPlaying = false;
            _view.SetAudioClip(_audioData.AudioClip);
            InitializeSliderValue();
        }

        public void Dispose()
        {
            _audioData = null;
            _soundGroupData = null;
            _view = null;
        }

        private void PlaySound()
        {
            _audioData.IsPlaying = true;
            _view.SetStopIcon();
            _previewSoundPlayerService.Play(
                _audioData.AudioClip, _soundGroupData, _view.SetSliderValue, StopSound);
        }

        private void StopSound()
        {
            _audioData.IsPlaying = false;
            _view.SetPlayIcon();
            _view.SetSliderValue(0);
            _previewSoundPlayerService.Stop();
        }

        public void DeleteAudioData()
        {
            _soundGroupData.Remove(_audioData);
            _view.Dispose();
        }

        public void SetAudioClip(AudioClip audioClip) =>
            _audioData.AudioClip = audioClip;

        public void ChangeSoundGroupState()
        {
            if (_audioData.IsPlaying == false)
                PlaySound();
            else
                StopSound();
        }

        private void InitializeSliderValue()
        {
            float maxValue = 0;
            
            if (_audioData.AudioClip != null)
                maxValue = _audioData.AudioClip.length;
            
            _view.SetSliderValue(new Vector2(0, maxValue), 0);
        }
    }
}