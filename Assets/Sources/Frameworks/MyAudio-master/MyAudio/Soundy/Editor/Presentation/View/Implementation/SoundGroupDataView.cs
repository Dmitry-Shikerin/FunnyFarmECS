using System;
using System.Collections.Generic;
using Doozy.Editor.EditorUI.Components;
using Doozy.Editor.EditorUI.Components.Internal;
using Doozy.Editor.EditorUI.Events;
using Doozy.Runtime.UIElements.Extensions;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation.Base;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation
{
    public class SoundGroupDataView : EditorPresentableView<SoundGroupDataPresenter, SoundGroupDataVisualElement>, ISoundGroupDataView
    {
        private readonly List<IAudioDataView> _audioDataViews = new ();

        public IReadOnlyList<IAudioDataView> AudioDataViews => _audioDataViews;

        protected override void Initialize()
        {
            Root.RandomButtonTab.SetOnClick(() => 
                Presenter.SetPlayMode(SoundPlayMode.Random));
            Root.SequenceButtonTab.SetOnClick(() => 
                Presenter.SetPlayMode(SoundPlayMode.Sequence));
            Root.LoopToggle.OnValueChanged += ChangeLoop;
            Root.NewSoundContentVisualElement.CreateButton.SetOnClick(
               () => Presenter.CreateAudioData());
            Root.VolumeSlider.slider.RegisterValueChangedCallback((value) => 
                Presenter.ChangeVolume(value.newValue));
            Root.PitchSlider.slider.RegisterValueChangedCallback((value) => 
                Presenter.ChangePitch(value.newValue));
            Root.SpatialBlendSlider.slider.RegisterValueChangedCallback((value)
               => Presenter.ChangeSpatialBlend(value.newValue));
           // _visualElement.HeaderVisualElement.PingAssetButton.SetOnClick(() =>
           //     Selection.activeObject = _presenter.GetSoundGroupData());
        }

        protected override void DisposeView()
        {
            Root.LoopToggle.OnValueChanged -= ChangeLoop;
        }

        private void ChangeLoop(FluidBoolEvent fluidBoolEvent) =>
            Presenter.ChangeLoopState(fluidBoolEvent.newValue);
        
        public void SetVolume(Vector2 volume, Vector2 minMaxVolume)
        {
            Root.VolumeSlider.slider.value = volume;
            Root.VolumeSlider.slider.lowLimit = minMaxVolume.x;
            Root.VolumeSlider.slider.highLimit = minMaxVolume.y;
        }

        public void SetPitch(Vector2 volume, Vector2 minMaxVolume)
        {
            Root.PitchSlider.slider.value = volume;
            Root.PitchSlider.slider.lowLimit = minMaxVolume.x;
            Root.PitchSlider.slider.highLimit = minMaxVolume.y;
        }

        public void SetSpatialBlend(float spatialBlend, Vector2 minMaxSpatialBlend)
        {
            Root.SpatialBlendSlider.slider.value = spatialBlend;
            Root.SpatialBlendSlider.slider.lowValue = minMaxSpatialBlend.x;
            Root.SpatialBlendSlider.slider.highValue = minMaxSpatialBlend.y;
        }

        public void SetSoundName(string name)
        {
            Root.HeaderVisualElement.SoundGroupTextField.value = name;
        }

        public void StopAllAudioData()
        {
            foreach (IAudioDataView audioDataView in _audioDataViews)
                audioDataView.StopPlaySound();
        }

        public void SetLoop(bool loop) =>
            Root.LoopToggle.isOn = loop;

        public void SetIsOnButtonTab(SoundPlayMode playMode)
        {
            Action changePlayMode = playMode switch
            {
                SoundPlayMode.Random => () => Root.RandomButtonTab.isOn = true,
                SoundPlayMode.Sequence => () => Root.SequenceButtonTab.isOn = true,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            changePlayMode?.Invoke();
        }
        
        public void AddAudioData(IAudioDataView audioDataView)
        {
            Root
                .AudioDataContent
                .AddChild(audioDataView.Root)
                .AddSpace(2);
            _audioDataViews.Add(audioDataView);
        }
    }
}