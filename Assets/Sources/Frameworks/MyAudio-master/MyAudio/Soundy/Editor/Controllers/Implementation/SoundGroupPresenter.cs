using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class SoundGroupPresenter : IPresenter
    {
        private readonly SoundGroupData _soundGroupData;
        private readonly SoundDataBase _soundDatabase;
        private readonly ISoundGroupView _view;
        private readonly EditorUpdateService _editorUpdateService;
        private readonly PreviewSoundPlayerService _previewSoundPlayerService;

        public SoundGroupPresenter(
            SoundGroupData soundGroup,
            SoundDataBase soundDatabase,
            ISoundGroupView view,
            EditorUpdateService editorUpdateService,
            PreviewSoundPlayerService previewSoundPlayerService)
        {
            _soundGroupData = soundGroup ?? throw new ArgumentNullException(nameof(soundGroup));
            _soundDatabase = soundDatabase ?? throw new ArgumentNullException(nameof(soundDatabase));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _editorUpdateService = editorUpdateService ?? throw new ArgumentNullException(nameof(editorUpdateService));
            _previewSoundPlayerService = previewSoundPlayerService ?? throw new ArgumentNullException(nameof(previewSoundPlayerService));
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
            SoundGroupDataEditorWindow.Open(
                _soundGroupData, _editorUpdateService, _previewSoundPlayerService);
        
        public void StopSound()
        {
            _soundGroupData.IsPlaying = false;
            _view.SetPlayIcon();
            _previewSoundPlayerService.Stop();
            _view.SetSliderValue(0);
        }

        private void PlaySound()
        {
            var audioSource = _previewSoundPlayerService.Play(_soundGroupData, SetSliderValue, StopSound);
            _soundGroupData .IsPlaying = true;
            _view.SetSliderMaxValue(audioSource.clip.length);
            _view.SetStopIcon();
        }
        
        private void SetSliderValue(float value) =>
            _view.SetSliderValue(value);

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