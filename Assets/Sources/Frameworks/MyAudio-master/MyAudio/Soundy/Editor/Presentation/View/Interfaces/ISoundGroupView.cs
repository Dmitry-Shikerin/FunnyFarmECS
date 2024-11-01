using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface ISoundGroupView : IView<SoundGroupVisualElement>
    {
        void SetPlayIcon();
        void SetStopIcon();
        void SetSliderValue(float audioSourceTime);
        void SetSliderMaxValue(float maxValue);
        void SetSoundGroupName(string soundGroupName);
    }
}