using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface IAudioDataView : IView<AudioDataVisualElement>
    {
        void SetSliderValue(float value);        
        void SetSliderValue(Vector2 minMaxValue, float value);
        void SetStopIcon();
        void SetPlayIcon();
        void SetAudioClip(AudioClip audioClip);
    }
}