using System;
using System.Collections.Generic;
using System.Linq;
using MyAudios.MyUiFramework.Utils;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Dictionaries;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "SoundyDatabase", menuName = "Soundy/SoundyDatabase", order = 51)]
    public class SoundyDatabase : ScriptableObject
    {
        public List<string> DatabaseNames = new List<string>();
        public List<SoundDatabase> SoundDatabases = new List<SoundDatabase>();

        [HideInInspector] [SerializeField] private SoundDataBaseDictionary _dataBases = new ();
        
        public bool AddSoundDatabase(SoundDatabase database, bool saveAssets)
        {
            if (database == null)
                return false;
            
            if (SoundDatabases == null)
                SoundDatabases = new List<SoundDatabase>();
            
            SoundDatabases.Add(database);
            UpdateDatabaseNames(false);
            SetDirty(saveAssets);
            
            return true;
        }
        
        public bool Contains(string databaseName)
        {
            if (SoundDatabases == null)
            {
                SoundDatabases = new List<SoundDatabase>();
                
                return false;
            }

            bool result = false;
            bool foundNullReference = false;

            foreach (SoundDatabase database in SoundDatabases)
            {
                if (database == null)
                {
                    foundNullReference = true;
                    
                    continue;
                }

                if (database.DatabaseName.Equals(databaseName))
                {
                    result = true;
                    
                    break;
                }
            }

            if (foundNullReference)
                RemoveNullDatabases();

            return result;
        }
        
        public bool Contains(string databaseName, string soundName) =>
            Contains(databaseName) && GetSoundDatabase(databaseName).Contains(soundName);
        
        public bool CreateSoundDatabase(string databaseName, bool showDialog = false, bool saveAssets = false) =>
            CreateSoundDatabase(SoundDataBaseConst.ResourcesPath, databaseName, showDialog, saveAssets);
        
        public bool CreateSoundDatabase(string relativePath, string databaseName, bool showDialog = false, bool saveAssets = false)
        {
            databaseName = databaseName.Trim();

            if (string.IsNullOrEmpty(databaseName))
            {
// #if UNITY_EDITOR
//                 if (showDialog)
//                     EditorUtility.DisplayDialog(UILabels.NewSoundDatabase, UILabels.EnterDatabaseName, UILabels.Ok);
// #endif
                return false;
            }

            if (Contains(databaseName))
            {
// #if UNITY_EDITOR
//                 if (showDialog)
//                     EditorUtility.DisplayDialog(UILabels.NewSoundDatabase, UILabels.DatabaseAlreadyExists, UILabels.Ok);
// #endif
                return false;
            }

#if UNITY_EDITOR
            SoundDatabase soundDatabase = MyAssetUtils.CreateAsset<SoundDatabase>(
                relativePath, $"SoundDataBase_{databaseName}");

#else
            SoundDatabase soundDatabase = ScriptableObject.CreateInstance<SoundDatabase>();
#endif
            soundDatabase.DatabaseName = databaseName;
            soundDatabase.Initialize(false);
            AddSoundDatabase(soundDatabase, false);
            SetDirty(saveAssets);
            
            return true;
        }
        
        public bool DeleteDatabase(SoundDatabase database)
        {
            if (database == null)
                return false;

#if UNITY_EDITOR
            if (EditorUtility.DisplayDialog($"{SoundyDataBaseConst.DeleteDatabase} '{database.DatabaseName }'",
                    SoundyDataBaseConst.AreYouSureYouWantToDeleteDatabase +
                                             "\n\n" +
                                             SoundyDataBaseConst.OperationCannotBeUndone,
                                             SoundyDataBaseConst.Yes,
                                             SoundyDataBaseConst.No) == false)
                return false;

            SoundDatabases.Remove(database);
            AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(database));
            UpdateDatabaseNames(true);
#endif
            return true;
        }    
        
        public SoundGroupData GetAudioData(string databaseName, string soundName) => 
            Contains(databaseName) == false ? null : GetSoundDatabase(databaseName).GetData(soundName);
        
        public SoundDatabase GetSoundDatabase(string databaseName)
        {
            if (SoundDatabases == null)
            {
                SoundDatabases = new List<SoundDatabase>();
                return null;
            }

            foreach (SoundDatabase database in SoundDatabases)
            {
                if (database.DatabaseName.Equals(databaseName))
                    return database;
            }
            
            if (SoundDatabases == null)
            {
                SoundDatabases = new List<SoundDatabase>();
                return null;
            }

            foreach (SoundDatabase database in SoundDatabases)
            {
                if (database.DatabaseName.Equals(databaseName))
                    return database;
            }

            return null;
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

            SoundDatabase soundDatabase = MyAssetUtils.CreateAsset<SoundDatabase>(
                SoundDataBaseConst.ResourcesPath, SoundDataBaseConst.GeneralName);
#else
            SoundDatabase soundDatabase = ScriptableObject.CreateInstance<SoundDatabase>();
#endif
            AddSoundDatabase(soundDatabase, true);
            soundDatabase.DatabaseName = SoundyManagerConstant.General;
            soundDatabase.Initialize(true);
            UpdateDatabaseNames(true);
        }
        
        public void InitializeSoundDatabases()
        {
            if (SoundDatabases == null)
                return;

            //remove any null sound database reference
            bool foundNullReference = false;
            
            for (int i = SoundDatabases.Count - 1; i >= 0; i--)
            {
                SoundDatabase soundDatabase = SoundDatabases[i];
                
                if (soundDatabase == null)
                {
                    SoundDatabases.RemoveAt(i);
                    foundNullReference = true;
                    
                    continue;
                }

                soundDatabase.Initialize(false);
            }

            //after removing any null references the database is still empty -> initialize it and add the 'General' sound database
            if (SoundDatabases.Count == 0)
            {
                Initialize();
                
                return;
            }


            //database is not empty, but at least one null sound database reference was removed -> mark the database as dirty
            if (foundNullReference)
                SetDirty(false);
        }
        
        public void RefreshDatabase(bool performUndo = true, bool saveAssets = false)
        {
            if (performUndo)
                UndoRecord(SoundyDataBaseConst.RefreshDatabase);

            Initialize();
            
            foreach (SoundDatabase soundDatabase in SoundDatabases)
                soundDatabase.RefreshDatabase(false, false);

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