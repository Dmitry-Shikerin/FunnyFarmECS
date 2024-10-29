using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New
{
    [Serializable]
    public class NewSoundDataBase
    {
        public string Name;
        public AudioMixerGroup OutputAudioMixerGroup;
        public List<string> SoundNames = new List<string>();
        public List<NewSoundGroupData> Database = new List<NewSoundGroupData>();

        public bool HasSoundsWithMissingAudioClips => Database.Any(data => data.HasMissingAudioClips);
        
        public bool Add(NewSoundGroupData data)
        {
            if (data == null)
                return false;

            data.DatabaseName = Name;

            return true;
        }
        
        public NewSoundGroupData Add(string soundName, bool performUndo, bool saveAssets)
        {
            soundName = soundName.Trim();
            string newName = soundName;
            int counter = 0;

            while (Contains(newName))
            {
                counter++;
                newName = soundName + " (" + counter + ")";
            }
            
            NewSoundGroupData data = new NewSoundGroupData();
            data.DatabaseName = Name;
            data.SoundName = newName;

            if (Database == null)
                Database = new List<NewSoundGroupData>();

            Database.Add(data);

            return data;
        }
        
        public bool Contains(string soundName)
        {
            if (Database == null)
            {
                Database = new List<NewSoundGroupData>();

                return false;
            }

            foreach (NewSoundGroupData data in Database)
            {
                if (data.SoundName.Equals(soundName))
                    return true;
            }

            return false;
        }

        public bool Contains(NewSoundGroupData soundGroupData)
        {
            if (soundGroupData != null && Database.Contains(soundGroupData))
                return true;

            return false;
        }
        
        public NewSoundGroupData GetData(string soundName)
        {
            foreach (NewSoundGroupData data in Database)
            {
                if (data.SoundName.Equals(soundName))
                    return data;
            }

            return null;
        }
        
        public void Initialize(bool saveAssets) =>
            RefreshDatabase();
        
        public bool Remove(NewSoundGroupData data, bool showDialog = false, bool saveAssets = false)
        {
            if (data == null)
                return false;

            if (Contains(data) == false)
                return false;

            for (int i = Database.Count - 1; i >= 0; i--)
            {
                if (Database[i] == data)
                {
                    Database.RemoveAt(i);

                    break;
                }
            }

            UpdateSoundNames(false);

            return true;
        }
        
        public void RefreshDatabase()
        {
            AddNoSound();
            RemoveUnnamedEntries();
            RemoveDuplicateEntries(false);
            CheckAllDataForCorrectDatabaseName(false);
            Sort();
            UpdateSoundNames(false);
        }
        
        public void RemoveEntriesWithNoAudioClipsReferenced(bool performUndo, bool saveAssets = false)
        {
            for (int i = Database.Count - 1; i >= 0; i--)
            {
                NewSoundGroupData data = Database[i];

                if (data.SoundName.Equals(SoundyManagerConstant.NoSound))
                    continue;

                if (data.Sounds == null)
                {
                    Database.RemoveAt(i);

                    continue;
                }

                for (int j = data.Sounds.Count - 1; j >= 0; j--)
                {
                    if (data.Sounds[j] == null)
                        data.Sounds.RemoveAt(j);
                }

                if (data.Sounds.Count == 0)
                    Database.RemoveAt(i);
            }
        }
        
        public void RemoveDuplicateEntries(bool performUndo, bool saveAssets = false)
        {
            Database = Database
                .GroupBy(data => data.SoundName)
                .Select(data => data.First())
                .ToList();
        }
        
        public void RemoveUnnamedEntries() =>
            Database = Database.Where(data => string.IsNullOrEmpty(data.SoundName.Trim()) == false).ToList();

        public void Sort()
        {
            Database = Database.OrderBy(data => data.SoundName).ToList();
            
            NewSoundGroupData noSoundSoundGroupData = null;

            foreach (NewSoundGroupData audioData in Database)
            {
                if (!audioData.SoundName.Equals(SoundyManagerConstant.NoSound))
                    continue;

                noSoundSoundGroupData = audioData;
                Database.Remove(audioData);

                break;
            }

            if (noSoundSoundGroupData != null)
                Database.Insert(0, noSoundSoundGroupData); //insert back the 'No Sound' entry at the top

            UpdateSoundNames(false);
        }
        
        public void UpdateSoundNames(bool saveAssets)
        {
#if UNITY_EDITOR
            if (SoundNames == null)
                SoundNames = new List<string>();

            if (Database == null)
                Database = new List<NewSoundGroupData>();

            AddNoSound();
#endif
            SoundNames.Clear();
            SoundNames.Add(SoundyManagerConstant.NoSound);

            var list = new List<string>();

            foreach (NewSoundGroupData data in Database)
                list.Add(data.SoundName);

            list.Sort();
            SoundNames.AddRange(list);
        }
        
        private bool AddNoSound()
        {
            if (Contains(SoundyManagerConstant.NoSound))
                return false;

            if (SoundNames == null)
                SoundNames = new List<string>();

            SoundNames.Add(SoundyManagerConstant.NoSound);
            NewSoundGroupData data = new NewSoundGroupData();
            data.DatabaseName = Name;
            data.SoundName = SoundyManagerConstant.NoSound;

            if (Database == null)
                Database = new List<NewSoundGroupData>();

            Database.Add(data);

            return true;
        }
        
        private bool CheckAllDataForCorrectDatabaseName(bool saveAssets)
        {
            bool foundSoundGroupWithWrongDatabaseName = false;

            foreach (NewSoundGroupData data in Database)
            {
                if (data == null)
                    continue;

                if (data.DatabaseName.Equals(Name))
                    continue;

                foundSoundGroupWithWrongDatabaseName = true;
                data.DatabaseName = Name;
            }

            return foundSoundGroupWithWrongDatabaseName;
        }
    }
}