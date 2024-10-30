using System;
using System.Linq;
using MyAudios.MyUiFramework.Utils;
using Sirenix.Utilities;
using Sources.Frameworks.GameServices.Singletones.Monobehaviours;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure.Factories;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers
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
        private SoundyControllerFactory _factory;

        public event Action<float> Running;

        public static bool IsMuteAllControllers { get; private set; }
        public static bool IsPauseAllControllers { get; private set; }
        public static SoundyDataBase Database => SoundySettings.Database;
        public static SoundySettings Settings => SoundySettings.Instance;
        public SoundControllersPool Pool => _pool;

        private void Awake()
        {
            _initialized = true;
            _pool = new SoundControllersPool(transform, Settings,this);
            _factory = new SoundyControllerFactory(this, _pool);
            _pool.Initialize(_factory);
            Init();
        }

        private void Update()
        {
            Running?.Invoke(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _pool.Destroy();
        }

        private static void Init()
        {
            for (int i = 0; i < Instance._pool.MinCount + 1; i++)
                Instance._pool.Get().Stop();
        }
        
        public static SoundyController Play(string databaseName, string soundName)
        {
            SoundGroupData data = GetData(out SoundDataBase dataBase, databaseName, soundName);
            //TODO перетащить это в свойство и сделать проверку на нулувую последнюю воспроизведенную аудиодату
            data.ChangeLastPlayedAudioData();

            return Instance.Pool.Get()
                .SetAudioClip(data.LastPlayedAudioData.AudioClip)
                .SetVolume(data.RandomVolume)
                .SetPitch(data.RandomPitch)
                .SetLoop(data.Loop)
                .SetSpatialBlend(data.SpatialBlend)
                .SetOutputAudioMixerGroup(dataBase.OutputAudioMixerGroup)
                .SetPosition(Instance.transform.position)
                .SetName(data.SoundName, data.LastPlayedAudioData.AudioClip.name)
                .Play();
        }

        private static SoundGroupData GetData(out SoundDataBase database, string databaseName, string soundName)
        {
            //TODO эта валидация должна быть в моделях
            if (soundName.Equals(SoundyManagerConstant.NoSound))
                throw new InvalidOperationException(nameof(soundName));

            if (string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(databaseName.Trim()))
                throw new NullReferenceException();

            if (string.IsNullOrEmpty(soundName) || string.IsNullOrEmpty(soundName.Trim()))
                throw new NullReferenceException();

            database = Database.GetSoundDatabase(databaseName);

            if (database == null)
                throw new NullReferenceException();

            return database.GetData(soundName) ?? throw new NullReferenceException();
        }

        public static SoundyController Play(AudioClip audioClip)
        {
            if (audioClip == null)
                throw new NullReferenceException(nameof(AudioClip));

            return Instance.Pool.Get()
                    .SetAudioClip(audioClip)
                    .SetName("AudioClip", audioClip.name)
                    .SetPosition(Instance.transform.position)
                    .Play();
        }

        public static void Stop(string databaseName, string soundName)
        {
            //TODO сделать пул для звуков которые проигрываются
            Instance._pool.Collection
                .Where(controller => controller.Name == soundName)
                .ForEach(controller => controller.Stop());
        }

        public static void Pause(string soundName) =>
            ApplyWhere(soundName, controller => controller.Pause());

        public static void UnPause(string soundName) =>
            ApplyWhere(soundName, controller => controller.Unpause());

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

        public static void DestroyAll() =>
            Instance._pool.Collection.ForEach(controller => controller.Destroy());

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

        public static void SetVolume(string soundName, float volume) =>
            ApplyWhere(soundName, controller => controller.AudioSource.volume = volume);

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

        private static void ApplyWhere(string soundName, Action<SoundyController> action)
        {
            Instance._pool.Collection
                .Where(controller => controller.Name == soundName)
                .ForEach(action.Invoke);
        }
    }
}