using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using UnityEditor;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New
{
    [Serializable]
    public class NewSoundDataBase
    {
        public string Name;
        public AudioMixerGroup OutputAudioMixerGroup;
        public List<string> SoundNames = new List<string>();
        public List<SoundGroupData> Database = new List<SoundGroupData>();
        
        public bool HasSoundsWithMissingAudioClips
        {
            get
            {
                foreach (SoundGroupData soundGroupData in Database)
                {
                    if (soundGroupData.HasMissingAudioClips)
                        return true;
                }

                return false;
            }
        }
        
        public bool Add(SoundGroupData data, bool saveAssets)
        {
            if (data == null)
                return false;

            data.DatabaseName = Name;
            AddObjectToAsset(data);
            SetDirty(saveAssets);

            return true;
        }

        public void RenameSoundGroup(SoundGroupData soundGroupData, string newName, bool isApplyFirstSoundName = false)
        {
            if (Database.Contains(soundGroupData) == false)
                return;

            if (string.IsNullOrEmpty(newName))
                return;

            // if (isApplyFirstSoundName)
            // {
            //     if (soundGroupData.Sounds.Count == 0)
            //         return;
            //     
            //     AssetDatabase.RenameAsset(
            //         AssetDatabase.GetAssetPath(soundGroupData),
            //         soundGroupData.Sounds[0].AudioClip.name);
            //     
            //     return;
            // }
            //
            //AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(soundGroupData), "SoundGroupData_" + newName);
        }
        
        public SoundGroupData Add(string soundName, bool performUndo, bool saveAssets)
        {
            soundName = soundName.Trim();
            string newName = soundName;
            int counter = 0;

            while (Contains(newName))
            {
                counter++;
                newName = soundName + " (" + counter + ")";
            }

            if (performUndo)
                UndoRecord(SoundDataBaseConst.AddItemConst);

            SoundGroupData data = CreateInstance<SoundGroupData>();
            data.DatabaseName = Name;
            data.SoundName = newName;
            data.name = data.SoundName;
            data.SetDirty(false);

            if (Database == null)
                Database = new List<SoundGroupData>();

            Database.Add(data);
            AddObjectToAsset(data);
            SetDirty(saveAssets);

            return data;
        }
        
        public bool Contains(string soundName)
        {
            if (Database == null)
            {
                Database = new List<SoundGroupData>();

                return false;
            }

            foreach (SoundGroupData data in Database)
            {
                if (data.SoundName.Equals(soundName))
                    return true;
            }

            return false;
        }

        public bool Contains(SoundGroupData soundGroupData)
        {
            if (soundGroupData != null && Database.Contains(soundGroupData))
                return true;

            return false;
        }
        
        public SoundGroupData GetData(string soundName)
        {
            foreach (SoundGroupData data in Database)
            {
                if (data.SoundName.Equals(soundName))
                    return data;
            }

            return null;
        }
        
        public void Initialize(bool saveAssets) =>
            RefreshDatabase(false, saveAssets);
        
        public bool Remove(SoundGroupData data, bool showDialog = false, bool saveAssets = false)
        {
            if (data == null)
                return false;

            if (Contains(data) == false)
                return false;

            for (int i = Database.Count - 1; i >= 0; i--)
            {
                if (Database[i] == data)
                {
                    if (data != null)
                    {
#if UNITY_EDITOR
                        AssetDatabase.RemoveObjectFromAsset(data);
                        DestroyImmediate(data, true);
#endif
                    }

                    Database.RemoveAt(i);

                    break;
                }
            }

            UpdateSoundNames(false);
            SetDirty(saveAssets);

            return true;
        }
        
        public void RefreshDatabase(bool performUndo, bool saveAssets)
        {
            if (performUndo)
                UndoRecord(SoundDataBaseConst.RefreshDatabaseConst);

            bool addedTheNoSoundSoundGroup = AddNoSound();
            RemoveUnreferencedData();
            RemoveUnnamedEntries(false);
            RemoveDuplicateEntries(false);
            bool foundDataWithWrongDatabaseName = CheckAllDataForCorrectDatabaseName(false);
            Sort(false);
            UpdateSoundNames(false);
            SetDirty(saveAssets && (addedTheNoSoundSoundGroup || foundDataWithWrongDatabaseName));
        }
        
        public void RemoveEntriesWithNoAudioClipsReferenced(bool performUndo, bool saveAssets = false)
        {
            if (performUndo)
                UndoRecord(SoundDataBaseConst.RemovedEntryConst);

            for (int i = Database.Count - 1; i >= 0; i--)
            {
                SoundGroupData data = Database[i];

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

            SetDirty(saveAssets);
        }
        
        public void RemoveDuplicateEntries(bool performUndo, bool saveAssets = false)
        {
            if (performUndo)
                UndoRecord(SoundDataBaseConst.RemovedDuplicateEntriesConst);

            Database = Database.GroupBy(data => data.SoundName)
                .Select(n => n.First())
                .ToList();

            SetDirty(saveAssets);
        }
        
        public void RemoveUnnamedEntries(bool performUndo, bool saveAssets = false)
        {
            if (performUndo)
                UndoRecord(SoundDataBaseConst.RemoveEmptyEntriesConst);

            Database = Database.Where(data => !string.IsNullOrEmpty(data.SoundName.Trim())).ToList();
            SetDirty(saveAssets);
        }
        
        public void Sort(bool performUndo, bool saveAssets = false)
        {
            if (performUndo)
                UndoRecord(SoundDataBaseConst.SortDatabaseConst);

            Database = Database.OrderBy(data => data.SoundName).ToList();
            
            SoundGroupData noSoundSoundGroupData = null;

            foreach (SoundGroupData audioData in Database)
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
            SetDirty(saveAssets);
        }
        
        public void UpdateSoundNames(bool saveAssets)
        {
#if UNITY_EDITOR
            if (SoundNames == null)
                SoundNames = new List<string>();

            if (Database == null)
                Database = new List<SoundGroupData>();

            AddNoSound();
#endif
            SoundNames.Clear();
            SoundNames.Add(SoundyManagerConstant.NoSound);

            var list = new List<string>();

            foreach (SoundGroupData data in Database)
                list.Add(data.SoundName);

            list.Sort();
            SoundNames.AddRange(list);
            SetDirty(saveAssets);
        }
        
        private bool AddNoSound(bool saveAssets = false)
        {
            if (Contains(SoundyManagerConstant.NoSound))
                return false;

            if (SoundNames == null)
                SoundNames = new List<string>();

            SoundNames.Add(SoundyManagerConstant.NoSound);
            SoundGroupData data = CreateInstance<SoundGroupData>();
            data.DatabaseName = Name;
            data.SoundName = SoundyManagerConstant.NoSound;
            data.name = data.SoundName;
            data.SetDirty(false);

            if (Database == null)
                Database = new List<SoundGroupData>();

            Database.Add(data);
            AddObjectToAsset(data);
            SetDirty(saveAssets);

            return true;
        }
        
        private bool CheckAllDataForCorrectDatabaseName(bool saveAssets)
        {
            bool foundSoundGroupWithWrongDatabaseName = false;

            foreach (SoundGroupData data in Database)
            {
                if (data == null)
                    continue;

                if (data.DatabaseName.Equals(Name))
                    continue;

                foundSoundGroupWithWrongDatabaseName = true;
                data.DatabaseName = Name;
                data.SetDirty(false);
            }

            SetDirty(saveAssets);

            return foundSoundGroupWithWrongDatabaseName;
        }
        
        private void RemoveUnreferencedData(bool saveAssets = false)
        {
#if UNITY_EDITOR
            Object[] objects =
                AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this)); //load all of the data assets

            if (objects == null)
                return;

            //make sure they are not null
            List<SoundGroupData>
                foundAudioData =
                    objects.OfType<SoundGroupData>().ToList(); //create a temp list of all the found sub assets data

            if (Database == null)
                Database = new List<SoundGroupData>(); //sanity check

            bool save = false;

            //mark true if any sub asset was destroyed
            foreach (SoundGroupData data in foundAudioData)
            {
                if (Database.Contains(data))
                    continue; //reference was FOUND in the list -> continue

                DestroyImmediate(data, true); //reference was NOT FOUND in the list -> destroy the asset
                save = true; //mark true to set as dirty and save
            }

            if (!save)
                return; //if no sub asset was destroyed -> stop here

            SetDirty(saveAssets); //save database
#endif
        }
    }
}