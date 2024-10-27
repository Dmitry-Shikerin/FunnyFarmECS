using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class AudioDataPresenter : IPresenter
    {
        private AudioData _audioData;
        private SoundGroupData _soundGroupData;
        private IAudioDataView _view;
        private AudioSource _audioSource;

        public AudioDataPresenter(
            AudioData audioData,
            SoundGroupData soundGroupData,
            IAudioDataView view)
        {
            _audioData = audioData ?? throw new ArgumentNullException(nameof(audioData));
            _soundGroupData = soundGroupData ?? throw new ArgumentNullException(nameof(soundGroupData));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _audioSource = Object.FindObjectOfType<AudioSource>();
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
            EditorApplication.update -= SetSliderValue;
        }

        public void PlaySound()
        {
            _view.StopAllSounds();
            
            _audioData.IsPlaying = true;
            _soundGroupData.IsPlaying = true;
            EditorApplication.update += SetSliderValue;
            _view.SetStopIcon();
            PlaySoundPreview(_audioSource, null, _audioData.AudioClip);
        }

        public void StopSound()
        {
            _audioData.IsPlaying = false;
            _soundGroupData.IsPlaying = false;
            EditorApplication.update -= SetSliderValue;
            _view.SetPlayIcon();
            StopSoundPreview(_audioSource);
            _view.SetSliderValue(0);
        }

        public void DeleteAudioData()
        {
            _soundGroupData.RemoveAudioData(_audioData);
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

        private void SetSliderValue()
        {
            _view.SetSliderValue(_audioSource.time);
        }

        private void InitializeSliderValue()
        {
            float maxValue = 0;
            
            if (_audioData.AudioClip != null)
                maxValue = _audioData.AudioClip.length;
            
            _view.SetSliderValue(new Vector2(0, maxValue), 0);
        }
        
        public void PlaySoundPreview(AudioSource audioSource, AudioMixerGroup outputAudioMixerGroup, AudioClip audioClip)
        {
            if (audioSource == null)
                return;
            
            if (audioClip != null)
            {
                audioSource.clip = audioClip;
            }
            else
            {
                _soundGroupData.ChangeLastPlayedAudioData();
                
                if (_soundGroupData.LastPlayedAudioData == null)
                    return;
                
                audioSource.clip = _soundGroupData.LastPlayedAudioData.AudioClip;
            }

            audioSource.ignoreListenerPause = _soundGroupData.IgnoreListenerPause;
            audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            audioSource.volume = _soundGroupData.RandomVolume;
            audioSource.pitch = _soundGroupData.RandomPitch;
            audioSource.loop = _soundGroupData.Loop;
            audioSource.spatialBlend = _soundGroupData.SpatialBlend;
            Camera main = Camera.main;
            audioSource.transform.position = main == null ? Vector3.zero : main.transform.position;
            audioSource.Play();
        }
        
        public void PlaySoundPreview(AudioSource audioSource, AudioMixerGroup outputAudioMixerGroup) =>
            PlaySoundPreview(audioSource, outputAudioMixerGroup, null);
        
        public void StopSoundPreview(AudioSource audioSource)
        {
            if (audioSource == null)
                return;
            
            audioSource.Stop();
        }
    }
}