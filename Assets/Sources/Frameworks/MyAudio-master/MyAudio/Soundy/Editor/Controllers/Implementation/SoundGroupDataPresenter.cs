﻿using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class SoundGroupDataPresenter : IPresenter
    {
        private readonly SoundGroupData _soundGroupData;
        private readonly SoundyDataBase _soundyDataBase;
        private readonly ISoundGroupDataView _view;
        private readonly IAudioDataViewFactory _audioDataViewFactory;

        public SoundGroupDataPresenter(
            SoundGroupData soundGroupData,
            SoundyDataBase soundyDataBase,
            ISoundGroupDataView view,
            IAudioDataViewFactory audioDataViewFactory)
        {
            _soundGroupData = soundGroupData ?? throw new ArgumentNullException(nameof(soundGroupData));
            _soundyDataBase = soundyDataBase ?? throw new ArgumentNullException(nameof(soundyDataBase));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _audioDataViewFactory = audioDataViewFactory ?? throw new ArgumentNullException(nameof(audioDataViewFactory));
        }

        public void Initialize()
        {
            AddAudioDatas();
            _view.SetLoop(_soundGroupData.Loop);
            _view.SetSoundName(_soundGroupData.SoundName);
            switch (_soundGroupData.Mode)
            {
                case SoundPlayMode.Random:
                    _view.SetRandomIsOn();
                    break;
                case SoundPlayMode.Sequence:
                    _view.SetSequenceIsOn();
                    break;
            }
            _view.SetVolume(
                new Vector2(_soundGroupData.Volume.MinValue, _soundGroupData.Volume.MaxValue),
                new Vector2(SoundGroupDataConst.MinVolume, SoundGroupDataConst.MaxVolume));
            _view.SetPitch(
                new Vector2(_soundGroupData.Pitch.MinValue, _soundGroupData.Pitch.MaxValue),
                new Vector2(SoundGroupDataConst.MinPitch, SoundGroupDataConst.MaxPitch));
            _view.SetSpatialBlend(
                _soundGroupData.SpatialBlend,
                new Vector2(SoundGroupDataConst.MinSpatialBlend, SoundGroupDataConst.MaxSpatialBlend));
        }

        public void Dispose()
        {
        }

        private void AddAudioDatas()
        {
            foreach (AudioData audioData in _soundGroupData.Sounds)
            {
                IAudioDataView view = _audioDataViewFactory.Create(
                    audioData, _soundGroupData, _soundyDataBase);
                _view.AddAudioData(view);
            }
        }
        
        public void SetRandomPlayMode() =>
            _soundGroupData.Mode = SoundPlayMode.Random;
        
        public void SetSequencePlayMode() =>
            _soundGroupData.Mode = SoundPlayMode.Sequence;

        public void CreateAudioData()
        {
            AudioData audioData = _soundGroupData.AddAudioData();
            IAudioDataView view = _audioDataViewFactory.Create(
                audioData, _soundGroupData, _soundyDataBase);
            _view.AddAudioData(view);
        }

        public void ChangeVolume(Vector2 volume)
        {
            _soundGroupData.Volume.MinValue = volume.x;
            _soundGroupData.Volume.MaxValue = volume.y;
        }

        public void ChangePitch(Vector2 pitch)
        {
            _soundGroupData.Pitch.MinValue = pitch.x;
            _soundGroupData.Pitch.MaxValue = pitch.y;
        }

        public void ChangeSpatialBlend(float value) =>
            _soundGroupData.SpatialBlend = value;

        public void ChangeLoopState(bool value) =>
            _soundGroupData.Loop = value;
    }
}