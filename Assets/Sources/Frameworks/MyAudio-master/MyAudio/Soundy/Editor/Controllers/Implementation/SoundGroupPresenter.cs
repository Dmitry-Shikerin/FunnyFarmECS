using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class SoundGroupPresenter : IPresenter
    {
        private readonly SoundGroupData _soundGroupData;
        private readonly SoundDatabase _soundDatabase;
        private readonly ISoundGroupView _view;
        private readonly AudioSource _audioSource;

        public SoundGroupPresenter(
            SoundGroupData soundGroup,
            SoundDatabase soundDatabase,
            ISoundGroupView view)
        {
            _soundGroupData = soundGroup ?? throw new ArgumentNullException(nameof(soundGroup));
            _soundDatabase = soundDatabase ?? throw new ArgumentNullException(nameof(soundDatabase));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _audioSource = Object.FindObjectOfType<AudioSource>();
        }

        public void Initialize()
        {
            _soundGroupData.IsPlaying = false;
            _view.SetSoundGroupName(_soundGroupData.SoundName);
        }

        public void Dispose()
        {
            
        }

        public void ShowSoundGroupData() =>
            SoundGroupDataEditorWindow.Open(_soundGroupData);
        
        public void StopSound()
        {
            _soundGroupData.IsPlaying = false;
            EditorApplication.update -= SetSliderValue;
            _view.SetPlayIcon();
            StopSoundPreview(_audioSource);
            _view.SetSliderValue(0);
        }

        private void PlaySound()
        {
            _view.StopAllAudioGroup();
            
            if (_audioSource == null)
                return;
            
            PlaySoundPreview(_audioSource, null);
            
            if (_audioSource.isPlaying == false)
                return;
            
            _soundGroupData.IsPlaying = true;
            EditorApplication.update += SetSliderValue;
            _view.SetStopIcon();
            _view.SetSliderMaxValue(_audioSource.clip.length);
        }

        private void SetSliderValue()
        {
            _view.SetSliderValue(_audioSource.time);
            
            if (_audioSource.time + 0.1f >= _audioSource.clip.length)
                StopSound();
        }

        public void ChangeSoundGroupState()
        {
            if (_soundGroupData.IsPlaying == false)
                PlaySound();
            else
                StopSound();
        }

        public void MouseDown(int mouseButton)
        {
            Debug.Log($"MouseDown");
        }

        public void RemoveSoundGroup()
        {
            _soundDatabase.Remove(_soundGroupData);
            _view.Dispose();
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