using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEditor;
using UnityEngine;
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
            _soundGroupData.StopSoundPreview(_audioSource);
            _view.SetSliderValue(0);
        }

        private void PlaySound()
        {
            _view.StopAllAudioGroup();
            
            if (_audioSource == null)
                return;
            
            _soundGroupData.PlaySoundPreview(_audioSource, null);
            
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
    }
}