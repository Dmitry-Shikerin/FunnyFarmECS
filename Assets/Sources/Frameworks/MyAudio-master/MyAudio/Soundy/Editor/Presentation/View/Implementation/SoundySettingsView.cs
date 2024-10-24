using System;
using Doozy.Editor.EditorUI.Components;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation
{
    public class SoundySettingsView : ISoundySettingsView
    {
        private SoundySettingsPresenter _presenter;
        private SoundySettingsVisualElement _visualElement;

        public VisualElement Root { get; private set; }

        public void Construct(SoundySettingsPresenter presenter)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            
            CreateView();
            Initialize();
        }

        public void CreateView()
        {
            _visualElement = new SoundySettingsVisualElement();
            Root = _visualElement;
        }

        public void Initialize()
        {
            _visualElement.KillDurationRow.Slider.slider.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                _visualElement.KillDurationRow.IntegerField.value = eve.newValue;
                _presenter.ChangeKillDuration(eve.newValue);
            });
            _visualElement.KillDurationRow.IntegerField.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                _visualElement.KillDurationRow.Slider.slider.value = eve.newValue;
                _presenter.ChangeKillDuration(eve.newValue);
            });
            _visualElement.KillDurationRow.ResetButton.SetOnClick(() => _presenter.ResetKillDuration());
            
            _visualElement.IdleCheckRow.Slider.slider.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                _visualElement.IdleCheckRow.IntegerField.value = eve.newValue;
                _presenter.ChangeIdleCheckInterval(eve.newValue);
            });
            _visualElement.IdleCheckRow.IntegerField.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                _visualElement.IdleCheckRow.Slider.slider.value = eve.newValue;
                _presenter.ChangeIdleCheckInterval(eve.newValue);
            });
            _visualElement.IdleCheckRow.ResetButton.SetOnClick(() => _presenter.ResetIdleCheckInterval());
            
            _visualElement.MinControllersRow.Slider.slider.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                _visualElement.MinControllersRow.IntegerField.value = eve.newValue;
                _presenter.ChangeMinControllersCount(eve.newValue);
            });
            _visualElement.MinControllersRow.IntegerField.RegisterCallback<ChangeEvent<int>>((eve) =>
            {
                _visualElement.MinControllersRow.Slider.slider.value = eve.newValue;
                _presenter.ChangeMinControllersCount(eve.newValue);
            });
            _visualElement.MinControllersRow.ResetButton.SetOnClick(() => _presenter.ResetMinControllersCount());
            
            _presenter.Initialize();
        }

        public void SetAutoKillIdleControllers(bool autoKillIdleControllers) =>
            _visualElement.AutoKillToggle.isOn = autoKillIdleControllers;

        public void SetControllerAutoKillDuration(Vector2Int minMaxValue, int value)
        {
            _visualElement.KillDurationRow.Slider.slider.value = value;
            _visualElement.KillDurationRow.Slider.slider.lowValue = minMaxValue.x;
            _visualElement.KillDurationRow.Slider.slider.highValue = minMaxValue.y;
            _visualElement.KillDurationRow.IntegerField.value = value;
        }

        public void SetIdleCheckInterval(Vector2Int minMaxValue, int value)
        {
            _visualElement.IdleCheckRow.Slider.slider.value = value;
            _visualElement.IdleCheckRow.Slider.slider.lowValue = minMaxValue.x;
            _visualElement.IdleCheckRow.Slider.slider.highValue = minMaxValue.y;
            _visualElement.IdleCheckRow.IntegerField.value = value;
        }

        public void SetMinimumNumberOfControllers(Vector2Int minMaxValue, int value)
        {
            _visualElement.MinControllersRow.Slider.slider.value = value;
            _visualElement.MinControllersRow.Slider.slider.lowValue = minMaxValue.x;
            _visualElement.MinControllersRow.Slider.slider.highValue = minMaxValue.y;
            _visualElement.MinControllersRow.IntegerField.value = value;
        }

        public void Dispose()
        {
            Root.RemoveFromHierarchy();
            _presenter.Dispose();
        }
    }
}