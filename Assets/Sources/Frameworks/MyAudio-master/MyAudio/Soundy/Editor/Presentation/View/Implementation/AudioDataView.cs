using System;
using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation.Base;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation
{
    public class AudioDataView : EditorPresentableView<AudioDataPresenter, AudioDataVisualElement>, IAudioDataView
    {
        private FluidButton _playButton;
        private FluidButton _deleteButton;
        private FluidRangeSlider _topSlider;
        private AudioClip _audioClip;
        private ObjectField _objectField;
        private ISoundGroupDataView _soundGroupDataView;

        protected override void Initialize()
        {
            _deleteButton = Root.DeleteButton;
            _playButton = Root.PlayButton;
            _topSlider = Root.Slider;
            _objectField = Root.ObjectField;
            _playButton.SetOnClick(ChangeSoundGroupState);
            _deleteButton.SetOnClick(Presenter.DeleteAudioData);
            _objectField.RegisterValueChangedCallback((value) => 
                Presenter.SetAudioClip(value.newValue as AudioClip));
            Presenter.Initialize();
        }

        protected override void DisposeView()
        {
        }

        private void ChangeSoundGroupState() =>
            Presenter.ChangeSoundGroupState();

        public void SetSliderValue(float value) =>
            _topSlider.slider.value = value;

        public void SetSliderValue(Vector2 minMaxValue, float value)
        {
            _topSlider.slider.lowValue = minMaxValue.x;
            _topSlider.slider.highValue = minMaxValue.y;
            _topSlider.slider.value = value;
        }

        public void SetStopIcon() =>
            _playButton.SetIcon(EditorSpriteSheets.EditorUI.Icons.Stop);

        public void SetPlayIcon() =>
            _playButton.SetIcon(EditorSpriteSheets.EditorUI.Icons.Play);

        public void SetAudioClip(AudioClip audioClip) =>
            _objectField.SetValueWithoutNotify(audioClip);
    }
}