using System;
using System.Collections.Generic;
using MyAudios.MyUiFramework.Utils;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Dictionaries;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New
{
    [Serializable]
    [CreateAssetMenu(fileName = "SoundyDatabase", menuName = "Soundy/SoundyDatabase", order = 51)]
    public class NewSoundyDataBase : ScriptableObject
    {
        [HideInInspector] [SerializeField] private SoundDataBaseDictionary _dataBases = new ();
        
        public bool AddSoundDatabase(NewSoundDataBase database, bool saveAssets)
        {
            if (database == null)
                return false;
            
            if (_dataBases == null)
                _dataBases = new SoundDataBaseDictionary();
            
            _dataBases[database.Name] = database;
            
            //TODO возможно удалить
            UpdateDatabaseNames();
            SetDirty(saveAssets);
            
            return true;
        }
        
        public bool Contains(string databaseName) =>
            _dataBases.ContainsKey(databaseName);

        public bool Contains(string databaseName, string soundName) =>
            Contains(databaseName) && GetSoundDatabase(databaseName).Contains(soundName);
        
        public bool CreateSoundDatabase(string databaseName, bool saveAssets = false)
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
            
            NewSoundDataBase soundDataBase = new NewSoundDataBase();
            soundDataBase.Name = databaseName;
            soundDataBase.Initialize(false);
            AddSoundDatabase(soundDataBase, false);
            SetDirty(saveAssets);
            
            return true;
        }
        
        public bool DeleteDatabase(NewSoundDataBase database)
        {
            if (database == null)
                return false;

            if (_dataBases.ContainsValue(database) == false)
                return false;
            
            _dataBases.Remove(database.Name);
            UpdateDatabaseNames(true);
            return true;
        }    
        
        public NewSoundGroupData GetAudioData(string databaseName, string soundName) => 
            Contains(databaseName) == false ? null : GetSoundDatabase(databaseName).GetData(soundName);
        
        public NewSoundDataBase GetSoundDatabase(string databaseName)
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
            RemoveNullDatabases();

            if (Contains(SoundyManagerConstant.General))
                return;

#if UNITY_EDITOR
            SearchForUnregisteredDatabases(false);
            
            if (Contains(SoundyManagerConstant.General))
                return;
#endif
            NewSoundDataBase soundDataBase = new NewSoundDataBase();
            AddSoundDatabase(soundDataBase, true);
            soundDataBase.Name = SoundyManagerConstant.General;
            soundDataBase.Initialize(true);
            UpdateDatabaseNames(true);
        }
        
        public void InitializeSoundDatabases()
        {
            if (_dataBases == null)
                return;

            foreach (NewSoundDataBase dataBase in _dataBases.Values)
                dataBase.Initialize(false);

            //after removing any null references the database is still empty -> initialize it and add the 'General' sound database
            if (_dataBases.Count == 0)
                Initialize();
        }
        
        public void RefreshDatabase(bool performUndo = true, bool saveAssets = false)
        {
            if (performUndo)
                UndoRecord(SoundyDataBaseConst.RefreshDatabase);

            Initialize();
            
            foreach (NewSoundDataBase soundDatabase in _dataBases.Values)
                soundDatabase.RefreshDatabase();

            SetDirty(saveAssets);
        }
        
        public void RemoveNullDatabases(bool saveAssets = false)
        {
            bool needsSave = false;
            
            if (SoundDatabases == null)
            {
                SoundDatabases = new List<SoundDatabase>();
                SetDirty(false);
                needsSave = true;
            }

            bool removedDatabase = false;
            
            for (int i = SoundDatabases.Count - 1; i >= 0; i--)
            {
                if (SoundDatabases[i] != null)
                    continue;
                
                SoundDatabases.RemoveAt(i);
                removedDatabase = true;
            }

            UpdateDatabaseNames();

            if (needsSave || removedDatabase)
                SetDirty(saveAssets);
        }
        
        public bool RenameSoundDatabase(SoundDatabase soundDatabase, string newDatabaseName)
        {
            if (soundDatabase == null)
                return false;

            newDatabaseName = newDatabaseName.Trim();

#if UNITY_EDITOR
            if (string.IsNullOrEmpty(newDatabaseName))
            {
                EditorUtility.DisplayDialog(
                    $"{SoundyDataBaseConst.RenameSoundDatabase} '{soundDatabase.DatabaseName}'",
                    SoundyDataBaseConst.EnterDatabaseName,
                    SoundyDataBaseConst.Ok);

                return false;
            }

            if (Contains(newDatabaseName))
            {
                EditorUtility.DisplayDialog(
                    SoundyDataBaseConst.RenameSoundDatabase + " '" + soundDatabase.DatabaseName + "'",
                    SoundyDataBaseConst.NewSoundDatabase + ": '" + newDatabaseName + "" + "\n\n" + 
                    SoundyDataBaseConst.AnotherEntryExists,
                    SoundyDataBaseConst.Ok);

                return false;
            }

            soundDatabase.DatabaseName = newDatabaseName;
            AssetDatabase.RenameAsset(
                AssetDatabase.GetAssetPath(soundDatabase), 
                "SoundDatabase_" + newDatabaseName.Replace(" ",
                    string.Empty));
            UpdateDatabaseNames(true);
#endif
            return true;
        }
        
        public void SearchForUnregisteredDatabases(bool saveAssets)
        {
            bool foundUnregisteredDatabase = false;
            SoundDatabase[] array = Resources.LoadAll<SoundDatabase>("");
            
            if (array == null || array.Length == 0)
                return;
            
            if (SoundDatabases == null)
                SoundDatabases = new List<SoundDatabase>();
            
            foreach (SoundDatabase foundDatabase in array)
            {
                if (SoundDatabases.Contains(foundDatabase))
                    continue;
                
                AddSoundDatabase(foundDatabase, false);
                foundUnregisteredDatabase = true;
            }

            if (foundUnregisteredDatabase == false)
                return;
            
            UpdateDatabaseNames();
            SetDirty(saveAssets);
        }
        
        public void SetDirty(bool saveAssets) =>
            MyUtils.SetDirty(this, saveAssets);
        
        public void UndoRecord(string undoMessage) =>
            MyUtils.UndoRecordObject(this, undoMessage);
        
        public void UpdateDatabaseNames(bool saveAssets = false)
        {
            if (DatabaseNames == null)
                DatabaseNames = new List<string>();
            
            if (SoundDatabases == null) 
                SoundDatabases = new List<SoundDatabase>();
            
            DatabaseNames.Clear();
            bool foundNullDatabaseReference = false;
            
            foreach (SoundDatabase database in SoundDatabases)
            {
                if (database == null)
                {
                    foundNullDatabaseReference = true;
                    
                    continue;
                }

                DatabaseNames.Add(database.DatabaseName);
            }

            DatabaseNames.Sort();
            
            if (foundNullDatabaseReference)
            {
                SoundDatabases = SoundDatabases.Where(soundDatabase => soundDatabase != null).ToList();
                SetDirty(false);
            }

            SetDirty(saveAssets);
        }
    }
}