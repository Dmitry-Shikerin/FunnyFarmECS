using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public interface IPreviewSoundPlayerService
    {
        void Initialize();
        void Destroy();
        AudioSource Play(SoundGroupData soundGroupData, Action<float> sliderValueChange, Action stopAction = null);
        AudioSource Play(AudioClip audioClip,
            SoundGroupData soundGroupData,
            Action<float> sliderValueChange,
            Action stopAction = null,
            AudioMixerGroup outputAudioMixerGroup = null);
        void Stop();
    }
}