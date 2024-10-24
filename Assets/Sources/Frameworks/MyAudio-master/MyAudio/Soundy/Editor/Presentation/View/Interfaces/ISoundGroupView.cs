namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface ISoundGroupView : IView
    {
        void SetPlayIcon();
        void SetStopIcon();
        void SetSliderValue(float audioSourceTime);
        void SetSliderMaxValue(float maxValue);
        void StopAllAudioGroup();
        void SetDataBase(ISoundDataBaseView soundDataBaseView);
        void StopPlaySound();
        void SetSoundGroupName(string soundGroupName);
        void StopAllAudioData();
    }
}