using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Dictionaries;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data
{
    [Serializable]
    public class SoundDataBase
    {
        public string Name;
        public AudioMixerGroup OutputAudioMixerGroup;
        [SerializeField] private SoundGroupDataDictionary _dataBase = new ();

        public bool HasSoundsWithMissingAudioClips => _dataBase.Values.Any(data => data.HasMissingAudioClips);
        
        public IEnumerable<string> GetSoundNames() =>
            _dataBase.Keys;
        
        public IEnumerable<SoundGroupData> GetSoundDatabases() =>
            _dataBase.Values;
        
        public bool Add(SoundGroupData data)
        {
            if (data == null)
                return false;

            data.DatabaseName = Name;

            return true;
        }
        
        public SoundGroupData Add(string soundName)
        {
            soundName = soundName.Trim();
            string newName = soundName;
            int counter = 0;

            while (Contains(newName))
            {
                counter++;
                newName = soundName + " (" + counter + ")";
            }
            
            SoundGroupData data = new SoundGroupData();
            data.DatabaseName = Name;
            data.SoundName = newName;

            _dataBase ??= new SoundGroupDataDictionary();
            _dataBase[soundName] = data;

            return data;
        }
        
        public bool Contains(string soundName) =>
            _dataBase.ContainsKey(soundName);

        public bool Contains(SoundGroupData soundGroupData) =>
            soundGroupData != null && _dataBase.ContainsValue(soundGroupData);

        public SoundGroupData GetData(string soundName) =>
            _dataBase.GetValueOrDefault(soundName);

        public void Initialize() =>
            RefreshDatabase();
        
        public bool Remove(SoundGroupData data)
        {
            if (data == null)
                return false;

            if (Contains(data) == false)
                return false;

            _dataBase.Remove(data.SoundName);
            AddNoSound();

            return true;
        }
        
        public void RefreshDatabase()
        {
            AddNoSound();
            // RemoveUnnamedEntries();
            // RemoveDuplicateEntries();
            CheckAllDataForCorrectDatabaseName();
        }
        
        //TODO сделать конвертацию для этих методов
        public void RemoveDuplicateEntries() =>
            _dataBase = (SoundGroupDataDictionary)_dataBase.Values
                .GroupBy(data => data.SoundName)
                .Select(data => data.First())
                .ToDictionary(data => data.SoundName, data => data);

        public void RemoveUnnamedEntries() =>
            _dataBase = (SoundGroupDataDictionary)_dataBase.Values
                .Where(data => string.IsNullOrEmpty(data.SoundName.Trim()) == false)
                .ToDictionary(data => data.SoundName, data => data);
        
        private bool AddNoSound()
        {
            if (Contains(SoundyManagerConstant.NoSound))
                return false;
            
            if (_dataBase == null)
                _dataBase = new SoundGroupDataDictionary();

            SoundGroupData data = new SoundGroupData();
            _dataBase[SoundyManagerConstant.NoSound] = data;
            data.DatabaseName = Name;
            data.SoundName = SoundyManagerConstant.NoSound;
            
            return true;
        }
        
        private bool CheckAllDataForCorrectDatabaseName()
        {
            bool foundSoundGroupWithWrongDatabaseName = false;

            foreach (SoundGroupData data in _dataBase.Values)
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