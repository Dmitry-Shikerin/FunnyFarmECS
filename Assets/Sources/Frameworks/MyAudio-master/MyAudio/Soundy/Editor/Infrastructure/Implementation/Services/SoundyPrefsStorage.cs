using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public class SoundyPrefsStorage : ISoundyPrefsStorage
    {
        public const string SoundySettings = "SoundySettings";
        
        private const string LastSoundGroupDataKey = "LastSoundGroupData";
        private const string LastDataTabKey = "LastDataTab";
        
        public void SaveLastSoundGroupData(string name) => 
            PlayerPrefs.SetString(LastSoundGroupDataKey, name);
        
        public string GetLastSoundGroupData() => 
            PlayerPrefs.GetString(LastSoundGroupDataKey);

        public string GetLastDataTab() =>
            PlayerPrefs.GetString(LastDataTabKey);
        
        public void SaveLastDataTab(string name) =>
            PlayerPrefs.SetString(LastDataTabKey, name);
    }
}