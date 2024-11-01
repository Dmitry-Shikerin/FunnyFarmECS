using System;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public class PreviewSoundPlayerService : IInitialize, IDestroy
    {
        private readonly EditorUpdateService _updateService;
        private readonly SoundyController _soundController;
        private readonly AudioSource _audioSource;

        private Action<float> SliderValueChange;
        private Action Stopped;

        public PreviewSoundPlayerService(EditorUpdateService updateService)
        {
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _soundController = Object.FindObjectOfType<SoundyController>() ??
                               new GameObject().AddComponent<SoundyController>();
            _audioSource = _soundController.gameObject.GetComponent<AudioSource>() 
                           ?? _soundController.gameObject.AddComponent<AudioSource>();
        }

        public void Initialize() =>
            _updateService.Register(UpdateSlider);

        public void Destroy() =>
            _updateService.UnRegister(UpdateSlider);

        private void UpdateSlider(float deltaTime)
        {
            SliderValueChange?.Invoke(_audioSource.time);

            if (_audioSource.time + 0.1f >= _audioSource.clip.length)
                Stop();
        }

        public AudioSource Play(SoundGroupData soundGroupData, Action<float> sliderValueChange, Action stopAction = null)
        {
            soundGroupData.ChangeLastPlayedAudioData();
            _audioSource.clip = soundGroupData.LastPlayedAudioData.AudioClip;

            _audioSource.ignoreListenerPause = soundGroupData.IgnoreListenerPause;
            _audioSource.volume = soundGroupData.RandomVolume;
            _audioSource.pitch = soundGroupData.RandomPitch;
            _audioSource.loop = soundGroupData.Loop;
            _audioSource.spatialBlend = soundGroupData.SpatialBlend;
            Camera main = Camera.main;
            _audioSource.transform.position = main == null ? Vector3.zero : main.transform.position;
            
            //TODO апдейт слайдера
            Stopped?.Invoke();
            Stopped = stopAction;
            SliderValueChange?.Invoke(0);
            SliderValueChange = sliderValueChange;
            
            _audioSource.Play();

            return _audioSource;
        }

        public AudioSource Play(AudioClip audioClip, 
            SoundGroupData soundGroupData,
            Action<float> sliderValueChange, 
            Action stopAction = null,
            AudioMixerGroup outputAudioMixerGroup = null)
        {
            if (audioClip != null)
            {
                _audioSource.clip = audioClip;
            }
            else
            {
                soundGroupData.ChangeLastPlayedAudioData();

                if (soundGroupData.LastPlayedAudioData == null)
                    return _audioSource;

                _audioSource.clip = soundGroupData.LastPlayedAudioData.AudioClip;
            }

            _audioSource.ignoreListenerPause = soundGroupData.IgnoreListenerPause;
            _audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            _audioSource.volume = soundGroupData.RandomVolume;
            _audioSource.pitch = soundGroupData.RandomPitch;
            _audioSource.loop = soundGroupData.Loop;
            _audioSource.spatialBlend = soundGroupData.SpatialBlend;
            Camera main = Camera.main;
            _audioSource.transform.position = main == null ? Vector3.zero : main.transform.position;
            
            //TODO апдейт слайдера
            Stopped?.Invoke();
            Stopped = stopAction;
            SliderValueChange?.Invoke(0);
            SliderValueChange = sliderValueChange;
            
            _audioSource.Play();

            return _audioSource;
        }

        public void Stop()
        {
            _audioSource.Stop();
            SliderValueChange?.Invoke(0);
        }
    }
}