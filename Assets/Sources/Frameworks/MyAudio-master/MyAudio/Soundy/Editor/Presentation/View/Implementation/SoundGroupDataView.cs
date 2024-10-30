﻿using System;
using System.Collections.Generic;
using Doozy.Editor.EditorUI.Components;
using Doozy.Editor.EditorUI.Components.Internal;
using Doozy.Editor.EditorUI.Events;
using Doozy.Runtime.UIElements.Extensions;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation
{
    public class SoundGroupDataView : ISoundGroupDataView
    {
        private SoundGroupDataPresenter _presenter;
        private SoundGroupDataVisualElement _visualElement;
        private SerializedProperty _sequenceResetTime;
        private List<IAudioDataView> _audioDataViews = new List<IAudioDataView>();
        public VisualElement Root { get; private set; }

        public IReadOnlyList<IAudioDataView> AudioDataViews => _audioDataViews;

        public void Construct(SoundGroupDataPresenter presenter)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            CreateView();
            Initialize();
        }

        public void CreateView()
        {
            _visualElement = new SoundGroupDataVisualElement();
            Root = _visualElement;
        }

        public void Initialize()
        {
            _visualElement.RandomButtonTab.SetOnClick(() => 
                _presenter.SetPlayMode(SoundPlayMode.Random));
           _visualElement.SequenceButtonTab.SetOnClick(() => 
               _presenter.SetPlayMode(SoundPlayMode.Sequence));
           _visualElement.LoopToggle.OnValueChanged += ChangeLoop;
           _visualElement.NewSoundContentVisualElement.CreateButton.SetOnClick(
               () => _presenter.CreateAudioData());
           _visualElement.VolumeSlider.slider.RegisterValueChangedCallback((value) => 
               _presenter.ChangeVolume(value.newValue));
           _visualElement.PitchSlider.slider.RegisterValueChangedCallback((value) => 
               _presenter.ChangePitch(value.newValue));
           _visualElement.SpatialBlendSlider.slider.RegisterValueChangedCallback((value)
               => _presenter.ChangeSpatialBlend(value.newValue));
           // _visualElement.HeaderVisualElement.PingAssetButton.SetOnClick(() =>
           //     Selection.activeObject = _presenter.GetSoundGroupData());
           
           _presenter.Initialize();
        }

        private void ChangeLoop(FluidBoolEvent fluidBoolEvent) =>
            _presenter.ChangeLoopState(fluidBoolEvent.newValue);

        public void Dispose()
        {
            _visualElement.LoopToggle.OnValueChanged -= ChangeLoop;
            _presenter.Dispose();
        }
        
        public void SetVolume(Vector2 volume, Vector2 minMaxVolume)
        {
            _visualElement.VolumeSlider.slider.value = volume;
            _visualElement.VolumeSlider.slider.lowLimit = minMaxVolume.x;
            _visualElement.VolumeSlider.slider.highLimit = minMaxVolume.y;
        }

        public void SetPitch(Vector2 volume, Vector2 minMaxVolume)
        {
            _visualElement.PitchSlider.slider.value = volume;
            _visualElement.PitchSlider.slider.lowLimit = minMaxVolume.x;
            _visualElement.PitchSlider.slider.highLimit = minMaxVolume.y;
        }

        public void SetSpatialBlend(float spatialBlend, Vector2 minMaxSpatialBlend)
        {
            _visualElement.SpatialBlendSlider.slider.value = spatialBlend;
            _visualElement.SpatialBlendSlider.slider.lowValue = minMaxSpatialBlend.x;
            _visualElement.SpatialBlendSlider.slider.highValue = minMaxSpatialBlend.y;
        }

        public void SetSoundName(string name)
        {
            _visualElement.HeaderVisualElement.SoundGroupTextField.value = name;
        }

        public void StopAllAudioData()
        {
            foreach (IAudioDataView audioDataView in _audioDataViews)
                audioDataView.StopPlaySound();
        }

        public void SetLoop(bool loop) =>
            _visualElement.LoopToggle.isOn = loop;

        public void SetIsOnButtonTab(SoundPlayMode playMode)
        {
            Action changePlayMode = playMode switch
            {
                SoundPlayMode.Random => () => _visualElement.RandomButtonTab.isOn = true,
                SoundPlayMode.Sequence => () => _visualElement.SequenceButtonTab.isOn = true,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            changePlayMode?.Invoke();
        }
        
        public void AddAudioData(IAudioDataView audioDataView)
        {
            _visualElement
                .AudioDataContent
                .AddChild(audioDataView.Root)
                .AddSpace(2);
            _audioDataViews.Add(audioDataView);
        }
    }
}