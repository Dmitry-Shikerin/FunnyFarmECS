namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public interface ISoundyPrefsStorage
    {
        public const string SoundySettings = "SoundySettings";

        public void SaveLastSoundGroupData(string name);
        public string GetLastSoundGroupData();
        public string GetLastDataTab();
        public void SaveLastDataTab(string name);
    }
}