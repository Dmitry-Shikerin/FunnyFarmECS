using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface ISoundDataBaseView : IView
    {
        void AddSoundGroup(ISoundGroupView soundGroupView);
        public void RemoveSoundGroup(ISoundGroupView soundGroupView);
        void StopAllSoundGroup();
        void SetName(string name);
        void SetAudioMixerGroup(AudioMixerGroup audioMixerGroup);
        void RenameDataBaseButtons();
        void SetSoundyDataBaseView(ISoundyDataBaseView view);
    }
}