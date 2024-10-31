using Doozy.Editor.EditorUI.Components;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation.Base;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation
{
    public class SoundySettingsView : EditorPresentableView<SoundySettingsPresenter, SoundySettingsVisualElement>, ISoundySettingsView
    {
        protected override void Initialize()
        {
            Root.KillDurationRow.Slider.slider.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                Root.KillDurationRow.IntegerField.value = eve.newValue;
                Presenter.ChangeKillDuration(eve.newValue);
            });
            Root.KillDurationRow.IntegerField.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                Root.KillDurationRow.Slider.slider.value = eve.newValue;
                Presenter.ChangeKillDuration(eve.newValue);
            });
            Root.KillDurationRow.ResetButton.SetOnClick(() => Presenter.ResetKillDuration());
            
            Root.IdleCheckRow.Slider.slider.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                Root.IdleCheckRow.IntegerField.value = eve.newValue;
                Presenter.ChangeIdleCheckInterval(eve.newValue);
            });
            Root.IdleCheckRow.IntegerField.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                Root.IdleCheckRow.Slider.slider.value = eve.newValue;
                Presenter.ChangeIdleCheckInterval(eve.newValue);
            });
            Root.IdleCheckRow.ResetButton.SetOnClick(() => Presenter.ResetIdleCheckInterval());
            
            Root.MinControllersRow.Slider.slider.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                Root.MinControllersRow.IntegerField.value = eve.newValue;
                Presenter.ChangeMinControllersCount(eve.newValue);
            });
            Root.MinControllersRow.IntegerField.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                Root.MinControllersRow.Slider.slider.value = eve.newValue;
                Presenter.ChangeMinControllersCount(eve.newValue);
            });
            Root.MinControllersRow.ResetButton.SetOnClick(() => Presenter.ResetMinControllersCount());
        }

        protected override void DisposeView()
        {
        }

        public void SetAutoKillIdleControllers(bool autoKillIdleControllers) =>
            Root.AutoKillToggle.isOn = autoKillIdleControllers;

        public void SetControllerAutoKillDuration(Vector2Int minMaxValue, int value)
        {
            Root.KillDurationRow.Slider.slider.value = value;
            Root.KillDurationRow.Slider.slider.lowValue = minMaxValue.x;
            Root.KillDurationRow.Slider.slider.highValue = minMaxValue.y;
            Root.KillDurationRow.IntegerField.value = value;
        }

        public void SetIdleCheckInterval(Vector2Int minMaxValue, int value)
        {
            Root.IdleCheckRow.Slider.slider.value = value;
            Root.IdleCheckRow.Slider.slider.lowValue = minMaxValue.x;
            Root.IdleCheckRow.Slider.slider.highValue = minMaxValue.y;
            Root.IdleCheckRow.IntegerField.value = value;
        }

        public void SetMinimumNumberOfControllers(Vector2Int minMaxValue, int value)
        {
            Root.MinControllersRow.Slider.slider.value = value;
            Root.MinControllersRow.Slider.slider.lowValue = minMaxValue.x;
            Root.MinControllersRow.Slider.slider.highValue = minMaxValue.y;
            Root.MinControllersRow.IntegerField.value = value;
        }
    }
}