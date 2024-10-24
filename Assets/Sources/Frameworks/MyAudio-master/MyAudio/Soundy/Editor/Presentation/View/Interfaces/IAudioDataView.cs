using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface IAudioDataView : IView
    {
        void StopPlaySound();
        void SetSliderValue(float value);        
        void SetSliderValue(Vector2 minMaxValue, float value);
        void SetStopIcon();
        void SetPlayIcon();
        void SetAudioClip(AudioClip audioClip);
        void StopAllSounds();
        void SetSoundGroupData(ISoundGroupDataView soundGroupDataView);
    }
}