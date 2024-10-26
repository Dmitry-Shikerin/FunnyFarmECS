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
    public class SoundyManager : MonoBehaviourSingleton<SoundyManager>
    {
#if UNITY_EDITOR
        [MenuItem(SoundyManagerConstant.SoundyManagerMenuItemItemName, false,
            SoundyManagerConstant.SoundyManagerMenuItemPriority)]
        private static void CreateComponent(MenuCommand menuCommand)
        {
            SoundyManager addToScene = MyUtils.AddToScene<SoundyManager>(
                SoundyManagerConstant.SoundyManagerGameObjectName, true, true);
        }
#endif
        
        private float _musicVolume;
        private float _soundVolume;        
        private bool _isMusicVolumeMuted;
        private bool _isSoundVolumeMuted;
        private bool _applicationIsQuitting;
        private bool _initialized;
        private SoundControllersPool _pool;
        private SoundyControllerViewFactory _factory;

        public event Action<float> Running; 
        
        public static bool IsMuteAllControllers { get; private set; }
        public static bool IsPauseAllControllers { get; private set; }
        public static SoundyDatabase Database => SoundySettings.Database;
        public SoundControllersPool Pool => _pool;
        public SoundyControllerViewFactory Factory => _factory;

        private void Awake()
        {
            _initialized = true;
            _pool = new SoundControllersPool(transform, this);
            _factory = new SoundyControllerViewFactory(this, _pool);
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
        
        private static void Init()
        {
            for (int i = 0; i < Instance._pool.MinimumNumberOfControllers + 1; i++)
                Instance._pool.Get().Stop();
        }

        public static void Pause(string soundName)
        {
            ApplyWhere(soundName, controller => controller.Pause());
        }

        public static void UnPause(string soundName)
        {
            ApplyWhere(soundName, controller => controller.Unpause());
        }

        private static void ApplyWhere(string soundName, Action<SoundyController> action)
        {
            Instance._pool.Collection
                .Where(controller => controller.Name == soundName)
                .ForEach(action.Invoke);
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
            ApplyWhere(soundName, controller => controller.AudioSource.volume = volume);
        }

        public static SoundyController Play(string databaseName, string soundName, Vector3 position)
        {
            if (soundName.Equals(SoundyManagerConstant.NoSound))
                throw new NullReferenceException(soundName);

            SoundGroupData soundGroupData = Database.GetAudioData(databaseName, soundName);

            if (soundGroupData == null)
                throw new NullReferenceException(soundGroupData.SoundName);
            
            Debug.Log($"Play");
            SoundyController controller = Instance.Pool.Get();
            Instance.Factory.Create(soundGroupData, controller, Database.GetSoundDatabase(databaseName).OutputAudioMixerGroup);
            controller.Play();

            return controller;
        }

        public static SoundyController Play(AudioClip audioClip, Vector3 position) =>
            Play(audioClip, null, position);

        public static SoundyController Play(string databaseName, string soundName, Transform followTarget)
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
            SoundyController controller = Instance.Pool.Get();
            Instance.Factory.Create(soundGroupData, controller, Database.GetSoundDatabase(databaseName).OutputAudioMixerGroup, followTarget);
            controller.Play();
            //TODO конец добавления

            return controller;
        }

        public static SoundyController Play(AudioClip audioClip, Transform followTarget)
        {
            if (Instance._initialized == false)
                s_instance = Instance;

            return Play(audioClip, null, followTarget);
        }

        public static SoundyController Play(string databaseName, string soundName)
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
            SoundyController controller = Instance.Pool.Get();
            Instance._factory.Create(soundGroupData, controller);
            controller.Play();
            //TODO конец добавления

            return controller;
        }

        public static SoundyController Play(AudioClip audioClip) =>
            Play(audioClip, null, Instance.transform);

        public static SoundyController Play(
            AudioClip audioClip,
            AudioMixerGroup outputAudioMixerGroup,
            Vector3 position,
            float volume = 1,
            float pitch = 1,
            bool loop = false,
            float spatialBlend = 1)
        {
            if (audioClip == null)
                throw new NullReferenceException(nameof(AudioClip));

            SoundyController controller = Instance.Pool.Get();
            Instance.Factory.Create(controller, audioClip, outputAudioMixerGroup, position, volume, pitch, loop, spatialBlend);
            controller.Play();

            return controller;
        }

        public static SoundyController Play(
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

            SoundyController controller = Instance._pool.Get();
            Transform target = followTarget ?? Instance.transform;
            Instance.Factory.Create(controller, audioClip, outputAudioMixerGroup, target, volume, pitch, loop, spatialBlend);
            controller.Play();

            return controller;
        }

        public static SoundyController Play(SoundyData data)
        {
            if (data == null)
                return null;

            if (Instance._initialized == false)
                s_instance = Instance;

            return data.SoundSource switch
            {
                SoundSource.Soundy => Play(data.DatabaseName, data.SoundName),
                SoundSource.AudioClip => Play(data.AudioClip, data.OutputAudioMixerGroup),
                SoundSource.MasterAudio =>  null, // no action, or throw an exception, or return a default value
                _ => null
            };
        }

        public static SoundyController Play(SoundyData data, bool isSound)
        {
            SoundyController controller = Play(data);
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