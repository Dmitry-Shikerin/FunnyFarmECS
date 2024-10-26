using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data
{
    [Serializable]
    public class AudioData
    {
        public AudioClip AudioClip;
        
        [Range(AudioDataConst.MinWeight, AudioDataConst.MaxWeight)]
        public float Weight = AudioDataConst.DefaultWeight;

        public bool IsPlaying;
        
        public AudioData() =>
            Reset();
        
        public AudioData(AudioClip audioClip)
        {
            Reset();
            AudioClip = audioClip;
        }
        
        public AudioData(AudioClip audioClip, float weight)
        {
            Reset();
            AudioClip = audioClip;
            Weight = weight;
        }
        
        public void Reset()
        {
            AudioClip = null;
            Weight = 1;
        }
    }
}