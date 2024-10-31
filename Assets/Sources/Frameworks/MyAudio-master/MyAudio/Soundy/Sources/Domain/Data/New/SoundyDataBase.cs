using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Dictionaries;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New
{
    [Serializable]
    [CreateAssetMenu(fileName = "SoundyDatabase", menuName = "Soundy/SoundyDatabase", order = 51)]
    public class SoundyDataBase : ScriptableObject
    {
        [SerializeField] private SoundDataBaseDictionary _dataBases = new ();
        
        public IEnumerable<string> GetDatabaseNames() =>
            _dataBases.Keys.ToList();
        
        public IEnumerable<SoundDataBase> GetSoundDatabases() =>
            _dataBases.Values.ToList();
        
        public bool AddSoundDatabase(SoundDataBase database)
        {
            if (database == null)
                return false;
            
            _dataBases ??= new SoundDataBaseDictionary();
            _dataBases[database.Name] = database;
            
            return true;
        }
        
        public bool Contains(string databaseName) =>
            _dataBases.ContainsKey(databaseName);

        public bool Contains(string databaseName, string soundName) =>
            Contains(databaseName) && GetSoundDatabase(databaseName).Contains(soundName);
        
        public bool CreateSoundDatabase(string databaseName)
        {
            databaseName = databaseName.Trim();

            //TODO добавить здесь эксепшены и ловить их в презентере и высвечивать шоуДиалог
            if (string.IsNullOrEmpty(databaseName))
            {
                return false;
            }

            if (Contains(databaseName))
            {
                return false;
            }
            
            SoundDataBase soundDataBase = new SoundDataBase();
            soundDataBase.Name = databaseName;
            soundDataBase.Initialize();
            AddSoundDatabase(soundDataBase);
            
            return true;
        }
        
        public bool DeleteDatabase(SoundDataBase database)
        {
            if (database == null)
                return false;

            if (_dataBases.ContainsValue(database) == false)
                return false;
            
            _dataBases.Remove(database.Name);
            return true;
        }    
        
        public SoundGroupData GetAudioData(string databaseName, string soundName) => 
            Contains(databaseName) == false ? null : GetSoundDatabase(databaseName).GetData(soundName);
        
        public SoundDataBase GetSoundDatabase(string databaseName)
        {
            if (_dataBases == null)
            {
                _dataBases = new SoundDataBaseDictionary();
                return null;
            }

            return _dataBases.GetValueOrDefault(databaseName);
        }
        
        public void Initialize()
        {
            if (Contains(SoundyManagerConstant.General))
                return;

#if UNITY_EDITOR
            if (Contains(SoundyManagerConstant.General))
                return;
#endif
            SoundDataBase soundDataBase = new SoundDataBase();
            AddSoundDatabase(soundDataBase);
            soundDataBase.Name = SoundyManagerConstant.General;
            soundDataBase.Initialize();
        }
        
        public void InitializeSoundDatabases()
        {
            if (_dataBases == null)
                return;

            foreach (SoundDataBase dataBase in _dataBases.Values)
                dataBase.Initialize();

            //after removing any null references the database is still empty -> initialize it and add the 'General' sound database
            if (_dataBases.Count == 0)
                Initialize();
        }
        
        public void RefreshDatabase()
        {
            Initialize();
            
            foreach (SoundDataBase soundDatabase in _dataBases.Values)
                soundDatabase.RefreshDatabase();
        }
        
        public bool RenameSoundDatabase(SoundDataBase soundDatabase, string newDatabaseName)
        {
            if (soundDatabase == null)
                return false;

            newDatabaseName = newDatabaseName.Trim();

#if UNITY_EDITOR
            //TODO вытащить это в эдитор презентер
            if (string.IsNullOrEmpty(newDatabaseName))
            {
                EditorUtility.DisplayDialog(
                    $"{SoundyDataBaseConst.RenameSoundDatabase} '{soundDatabase.Name}'",
                    SoundyDataBaseConst.EnterDatabaseName,
                    SoundyDataBaseConst.Ok);

                return false;
            }

            if (Contains(newDatabaseName))
            {
                EditorUtility.DisplayDialog(
                    SoundyDataBaseConst.RenameSoundDatabase + " '" + soundDatabase.Name + "'",
                    SoundyDataBaseConst.NewSoundDatabase + ": '" + newDatabaseName + "" + "\n\n" + 
                    SoundyDataBaseConst.AnotherEntryExists,
                    SoundyDataBaseConst.Ok);

                return false;
            }

            _dataBases.Remove(soundDatabase.Name);
            soundDatabase.Name = newDatabaseName;
            _dataBases[soundDatabase.Name] = soundDatabase;
#endif
            return true;
        }
    }
}