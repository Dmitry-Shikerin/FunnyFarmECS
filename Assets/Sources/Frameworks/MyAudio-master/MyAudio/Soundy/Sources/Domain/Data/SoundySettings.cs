using System;
using MyAudios.MyUiFramework.Utils;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data
{
    [Serializable]
    public class SoundySettings : ScriptableObject
    {
        [SerializeField] private SoundyDatabase _database;

        public static SoundySettings Instance
        {
            get
            {
                if (s_instance != null)
                    return s_instance;
#if UNITY_EDITOR
                s_instance = AssetDatabase.LoadAssetAtPath<SoundySettings>(
                    "Assets/Resources/Soundy/Settings/SoundySettings.asset");
#endif          

                if (s_instance != null)
                    return s_instance;
                
                s_instance = Resources.Load<SoundySettings>(
                    "Soundy/Settings/SoundySettings");
                
                if (s_instance != null)
                    return s_instance;

#if UNITY_EDITOR
                s_instance = CreateInstance<SoundySettings>();
                AssetDatabase.CreateAsset(s_instance,
                    SoundySettingsConst.ResourcesPath + SoundySettingsConst.FileName + ".asset");
                AssetDatabase.SaveAssets();
#endif
                
                return s_instance;
            }
        }

        private static SoundySettings s_instance;

        public static SoundyDatabase Database
        {
            get
            {
                if (Instance._database != null)
                    return Instance._database;
                
                UpdateDatabase();
                
                return Instance._database;
            }
        }

        public static void UpdateDatabase()
        {
            Instance._database = MyAssetUtils.GetScriptableObject<SoundyDatabase>(
                "_" + SoundyDataBaseConst.FileName, "Assets/Resources/Soundy/DataBases");
#if UNITY_EDITOR
            if (Instance._database == null)
                return;
            
            Instance._database.Initialize();
            Instance._database.SearchForUnregisteredDatabases(false);
            Instance.SetDirty(true);
#endif
        }

        public bool AutoKillIdleControllers = SoundySettingsConst.AutoKillIdleControllersDefaultValue;
        public float ControllerIdleKillDuration = SoundySettingsConst.ControllerIdleKillDurationDefaultValue;
        public float IdleCheckInterval = SoundySettingsConst.IdleCheckIntervalDefaultValue;
        public int MinimumNumberOfControllers = SoundySettingsConst.MinimumNumberOfControllersDefaultValue;

        private void Reset()
        {
            ResetAutoKillIdleControllers();
            ResetControllerIdleKillDuration();
            ResetIdleCheckInterval();
            ResetMinimumNumberOfControllers();
        }
        
        public void Reset(bool saveAssets)
        {
            Reset();
            SetDirty(saveAssets);
        }

        public void ResetComponent()
        {
            
        }
        
        public void ResetControllerIdleKillDuration() =>
            ControllerIdleKillDuration = SoundySettingsConst.ControllerIdleKillDurationDefaultValue;
        
        public void ResetAutoKillIdleControllers() =>
            AutoKillIdleControllers = SoundySettingsConst.AutoKillIdleControllersDefaultValue;
        
        public void ResetIdleCheckInterval() =>
            IdleCheckInterval = SoundySettingsConst.IdleCheckIntervalDefaultValue;
        
        public void ResetMinimumNumberOfControllers() =>
            MinimumNumberOfControllers = SoundySettingsConst.MinimumNumberOfControllersDefaultValue;
        
        /// <summary> [Editor Only] Marks target object as dirty. (Only suitable for non-scene objects) </summary>
        /// <param name="saveAssets"> Write all unsaved asset changes to disk? </param>
        public void SetDirty(bool saveAssets) =>
            MyUtils.SetDirty(this, saveAssets);

        /// <summary> Records any changes done on the object after this function </summary>
        /// <param name="undoMessage"> The title of the action to appear in the undo history (i.e. visible in the undo menu) </param>
        public void UndoRecord(string undoMessage) =>
            MyUtils.UndoRecordObject(this, undoMessage);
    }
}