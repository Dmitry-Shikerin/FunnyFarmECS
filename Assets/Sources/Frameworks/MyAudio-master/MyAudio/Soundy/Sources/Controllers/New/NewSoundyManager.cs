using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyAudios.MyUiFramework.Utils;
using Sirenix.Utilities;
using Sources.Frameworks.GameServices.Singletones.Monobehaviours;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure.Factories;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New
{
    [AddComponentMenu(SoundyManagerConstant.SoundyManagerAddComponentMenuMenuName,
        SoundyManagerConstant.SoundyManagerAddComponentMenuOrder)]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(SoundyExecutionOrder.SoundyManager)]
    public class NewSoundyManager : MonoBehaviourSingleton<NewSoundyManager>
    {
#if UNITY_EDITOR
        [MenuItem(SoundyManagerConstant.SoundyManagerMenuItemItemName, false,
            SoundyManagerConstant.SoundyManagerMenuItemPriority)]
        private static void CreateComponent(MenuCommand menuCommand)
        {
            NewSoundyManager addToScene = AddToScene(true);
        }
#endif
        
        private float _musicVolume;
        private float _soundVolume;        
        private bool _isMusicVolumeMuted;
        private bool _isSoundVolumeMuted;
        private bool _applicationIsQuitting;
        private bool _initialized;
        private SoundControllersPool _pool;
        private NewSoundyControllerViewFactory _factory;

        public event Action<float> Running; 
        
        public static bool IsMuteAllControllers { get; private set; }
        public static bool IsPauseAllControllers { get; private set; }
        public static SoundyDatabase Database => SoundySettings.Database;
        public SoundControllersPool Pool => _pool;
        public NewSoundyControllerViewFactory Factory => _factory;
        
        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        // private static void RunOnStart()
        // {
        //     Instance._applicationIsQuitting = false;
        //     Instance._initialized = false;
        //     Instance._pool = null;
        // }

        private void Awake()
        {
            _initialized = true;
            _pool = new SoundControllersPool(transform, this);
            _factory = new NewSoundyControllerViewFactory(this, _pool);
            _pool.Initialize(_factory);
            Debug.Log($"Awake {_pool}");
            Init();
        }

        private void Update()
        {
             Running?.Invoke(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _pool?.Destroy();
        }

        public static NewSoundyManager AddToScene(bool selectGameObjectAfterCreation = false) =>
            MyUtils.AddToScene<NewSoundyManager>(
                SoundyManagerConstant.SoundyManagerGameObjectName, true, selectGameObjectAfterCreation);

        public static string GetSoundDatabaseFilename(string databaseName) =>
            "SoundDatabase_" + databaseName.Trim();

        public static void Init()
        {
            if (Instance._initialized || s_instance != null)
                return;

            s_instance = Instance;

            for (int i = 0; i < Instance._pool.MinimumNumberOfControllers + 1; i++)
                Instance._pool.Get().Stop();
        }

        public static void Pause(string soundName)
        {
            Instance._pool.Collection
                .Where(controller => controller.Name == soundName)
                .ForEach(controller => controller.Pause());
        }

        public static void UnPause(string soundName)
        {
            Instance._pool.Collection
                .Where(controller => controller.Name == soundName)
                .ForEach(controller => controller.Unpause());
        }

        public static void SetVolumes(float musicVolume, float soundVolume)
        {
            Instance._musicVolume = musicVolume;
            Instance._soundVolume = soundVolume;
        }

        public static void SetMutes(bool isMusicMuted, bool isSoundMuted)
        {
            Instance._isMusicVolumeMuted = isMusicMuted;
            Instance._isSoundVolumeMuted = isSoundMuted;
        }

        public static void DestroyAll()
        {
            Instance._pool.Collection.ForEach(controller => controller.Destroy());
        }

        public static void MuteAll()
        {
            Instance._pool.Collection.ForEach(controller => controller.Mute());
            IsMuteAllControllers = true;
        }

        public static void PauseAll()
        {
            Instance._pool.Collection.ForEach(controller => controller.Pause());
            IsPauseAllControllers = true;
        }

        public static void SetVolume(string soundName, float volume)
        {
            Instance.Pool.Collection
                .Where(controller => controller.Name == soundName)
                .ForEach(controller => controller.AudioSource.volume = volume);
        }

        public static NewSoundyController Play(string databaseName, string soundName, Vector3 position)
        {
            if (Instance._initialized == false)
                s_instance = Instance;

            if (Database == null)
                return null;

            if (soundName.Equals(SoundyManagerConstant.NoSound))
                return null;

            SoundGroupData soundGroupData = Database.GetAudioData(databaseName, soundName);

            if (soundGroupData == null)
                return null;
            
            Debug.Log($"Play");
            NewSoundyController controller = Instance.Pool.Get();
            Instance.Factory.Create(soundGroupData, controller, Database.GetSoundDatabase(databaseName).OutputAudioMixerGroup);
            controller.Play();

            return controller;
        }

        public static NewSoundyController Play(AudioClip audioClip, Vector3 position)
        {
            if (Instance._initialized == false)
                s_instance = Instance;
            
            return Play(audioClip, null, position);
        }

        public static NewSoundyController Play(string databaseName, string soundName, Transform followTarget)
        {
            if (Instance._initialized == false)
                s_instance = Instance;

            if (Database == null)
                return null;

            if (soundName.Equals(SoundyManagerConstant.NoSound))
                return null;

            SoundGroupData soundGroupData = Database.GetAudioData(databaseName, soundName);

            if (soundGroupData == null)
                return null;
            
            //TODO добавил я
            //TODO в остальных местах сделать по аналогии
            NewSoundyController controller = Instance.Pool.Get();
            Instance.Factory.Create(soundGroupData, controller, Database.GetSoundDatabase(databaseName).OutputAudioMixerGroup, followTarget);
            controller.Play();
            //TODO конец добавления

            return controller;
        }

        public static NewSoundyController Play(AudioClip audioClip, Transform followTarget)
        {
            if (Instance._initialized == false)
                s_instance = Instance;

            return Play(audioClip, null, followTarget);
        }

        public static NewSoundyController Play(string databaseName, string soundName)
        {
            if (Instance._initialized == false)
                s_instance = Instance;

            if (Database == null)
                return null;

            if (soundName.Equals(SoundyManagerConstant.NoSound))
                return null;

            if (string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(databaseName.Trim()))
                return null;

            if (string.IsNullOrEmpty(soundName) || string.IsNullOrEmpty(soundName.Trim()))
                return null;

            SoundDatabase soundDatabase = Database.GetSoundDatabase(databaseName);

            if (soundDatabase == null)
                return null;

            SoundGroupData soundGroupData = soundDatabase.GetData(soundName);

            if (soundGroupData == null)
                return null;
            
            //TODO добавил я
            //TODO в остальных местах сделать по аналогии
            NewSoundyController controller = Instance.Pool.Get();
            Instance._factory.Create(soundGroupData, controller);
            controller.Play();
            //TODO конец добавления

            return controller;
        }

        public static NewSoundyController Play(AudioClip audioClip)
        {
            if (Instance._initialized == false)
                s_instance = Instance;

            return Play(audioClip, null, Instance.transform);
        }

        public static NewSoundyController Play(
            AudioClip audioClip,
            AudioMixerGroup outputAudioMixerGroup,
            Vector3 position,
            float volume = 1,
            float pitch = 1,
            bool loop = false,
            float spatialBlend = 1)
        {
            if (Instance._initialized == false)
                s_instance = Instance;

            if (audioClip == null)
                return null;

            NewSoundyController controller = Instance._pool.Get();
            controller.SetSourceProperties(audioClip, volume, pitch, loop, spatialBlend);
            controller.SetOutputAudioMixerGroup(outputAudioMixerGroup);
            controller.SetPosition(position);
            controller.gameObject.name = "[AudioClip]-(" + audioClip.name + ")";
            controller.Name = audioClip.name;
            controller.Play();

            return controller;
        }

        public static NewSoundyController Play(
            AudioClip audioClip,
            AudioMixerGroup outputAudioMixerGroup,
            Transform followTarget = null,
            float volume = 1,
            float pitch = 1,
            bool loop = false,
            float spatialBlend = 1)
        {
            if (Instance._initialized == false)
                s_instance = Instance;

            if (audioClip == null)
                return null;

            NewSoundyController controller = Instance._pool.Get();
            controller.SetSourceProperties(audioClip, volume, pitch, loop, spatialBlend);
            controller.SetOutputAudioMixerGroup(outputAudioMixerGroup);

            if (followTarget == null)
            {
                spatialBlend = 0;
                controller.SetFollowTarget(Instance.transform);
            }
            else
            {
                controller.SetFollowTarget(followTarget);
            }

            controller.gameObject.name = "[AudioClip]-(" + audioClip.name + ")";
            controller.Name = audioClip.name;
            controller.Play();

            return controller;
        }

        public static NewSoundyController Play(SoundyData data)
        {
            if (data == null)
                return null;

            if (Instance._initialized == false)
                s_instance = Instance;

            switch (data.SoundSource)
            {
                case SoundSource.Soundy:
                    return Play(data.DatabaseName, data.SoundName);
                case SoundSource.AudioClip:
                    return Play(data.AudioClip, data.OutputAudioMixerGroup);
                case SoundSource.MasterAudio:
#if dUI_MasterAudio
                    DarkTonic.MasterAudio.MasterAudio.PlaySound(data.SoundName);
#endif
                    break;
            }

            return null;
        }

        public static NewSoundyController Play(SoundyData data, bool isSound)
        {
            NewSoundyController controller = Play(data);
            controller.AudioSource.volume = isSound ? Instance._soundVolume : Instance._musicVolume;
            controller.AudioSource.mute = isSound ? Instance._isSoundVolumeMuted : Instance._isMusicVolumeMuted;
            
            return controller;
        }

        public static void Stop(string databaseName, string soundName)
        {
            Instance._pool.Collection
                .Where(controller => controller.Name == soundName)
                .ForEach(controller => controller.Stop());
        }

        public static void StopAll()
        {
            Instance._pool.Collection.ForEach(controller => controller.Stop());
        }

        public static void UnmuteAll()
        {
            Instance._pool.Collection.ForEach(controller => controller.Unmute());
            IsMuteAllControllers = false;
        }

        public static void UnpauseAll()
        {
            Instance._pool.Collection.ForEach(controller => controller.Unpause());
            IsPauseAllControllers = false;
        }
    }
}