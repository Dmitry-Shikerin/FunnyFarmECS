using System;
using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation.Base;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation
{
    public class SoundGroupView : EditorPresentableView<SoundGroupPresenter, SoundGroupVisualElement>, ISoundGroupView
    {
        private ISoundDataBaseView _soundDataBaseView;
        
        protected override void Initialize()
        {
            Root.PlayButton
                .SetOnClick(() => Presenter.ChangeSoundGroupState());
            Root.DeleteButton.SetOnClick(() => Presenter.RemoveSoundGroup());
            Root.SoundGroupDataButton.SetOnClick(() => Presenter.ShowSoundGroupData());
            Root.Slider.sliderTracker.RegisterCallback<MouseDownEvent>((mouse) => Presenter.MouseDown(mouse.button)); ;
        }

        protected override void DisposeView()
        {
        }

        public void SetPlayIcon() =>
            Root.PlayButton.SetIcon(EditorSpriteSheets.EditorUI.Icons.Play);

        public void SetStopIcon() =>
            Root.PlayButton.SetIcon(EditorSpriteSheets.EditorUI.Icons.Stop);

        public void SetSliderValue(float audioSourceTime) =>
            Root.Slider.SetSliderValue(audioSourceTime);

        public void SetSliderMaxValue(float maxValue)
        {
            Root.Slider.slider.value = 0;
            Root.Slider.slider.lowValue = 0;
            Root.Slider.slider.highValue = maxValue;
        }

        public void SetDataBase(ISoundDataBaseView soundDataBaseView) =>
            _soundDataBaseView = soundDataBaseView;

        public void StopPlaySound() =>
            Presenter.StopSound();

        public void SetSoundGroupName(string soundGroupName)
        {
            Root
                .SoundGroupDataButton
                .SetLabelText(soundGroupName);
        }

        public void StopAllAudioData()
        {
        }
    }
}