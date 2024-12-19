using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Domain;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    [CreateAssetMenu(fileName = "EditorServiceLocator", menuName = "MyAudio/EditorServiceLocator", order = 51)]
    public class EditorServiceLocator : ScriptableObjectSingleton<EditorServiceLocator>
    {
        [HideInInspector] [SerializeReference] private TypeObjectSerializedDictionary _container = new();
        [HideInInspector] [SerializeField] private bool _isInitialized;

        private static TypeObjectSerializedDictionary s_container => Instance._container;

        public static T Get<T>()
            where T : class
        {
            Type type = typeof(T);

            if (s_container.TryGetValue(type, out object service) == false)
                throw new KeyNotFoundException();

            if (service is not T result)
                throw new InvalidCastException();

            return result;
        }

        private static void Bind<TInterface, TImplementation>(TImplementation service)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            if (s_container.ContainsKey(typeof(TInterface)))
                throw new InvalidOperationException();

            s_container[typeof(TInterface)] = service;
        }

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            AssetName = "EditorServiceLocator";
            ResourcesPath = "Services";

            if (Instance._isInitialized)
                return;

            EditorApplication.quitting += Instance.Uninitialize;

            Bind<IWindowService, WindowService>(new WindowService());
            Bind<IEditorUpdateService, EditorUpdateService>(new EditorUpdateService());
            Bind<ISoundyPrefsStorage, SoundyPrefsStorage>(new SoundyPrefsStorage());
            Bind<IPreviewSoundPlayerService, PreviewSoundPlayerService>(
                new PreviewSoundPlayerService(Get<IEditorUpdateService>()));
            Bind<IAudioDataViewFactory, AudioDataViewFactory>(
                new AudioDataViewFactory(Get<PreviewSoundPlayerService>()));
            Bind<ISoundDataBaseViewFactory, SoundDataBaseViewFactory>(new SoundDataBaseViewFactory(
                Get<IWindowService>(),
                Get<ISoundyPrefsStorage>(),
                Get<IPreviewSoundPlayerService>()));
            Bind<ISoundGroupDataViewFactory, SoundGroupDataViewFactory>(
                new SoundGroupDataViewFactory(
                Get<EditorUpdateService>(),
                Get<PreviewSoundPlayerService>()));
            Bind<ISoundGroupViewFactory, SoundGroupViewFactory>(
                new SoundGroupViewFactory(
                    Get<IWindowService>(),
                Get<ISoundyPrefsStorage>(),
                Get<IPreviewSoundPlayerService>()));
            Bind<ISoundyDataBaseViewFactory, SoundyDataBaseViewFactory>(
                new SoundyDataBaseViewFactory(
                    Get<IWindowService>(),
                Get<ISoundyPrefsStorage>(),
                Get<IPreviewSoundPlayerService>()));
            Bind<ISoundySettingsViewFactory, SoundySettingsViewFactory>(
                new SoundySettingsViewFactory());

            Instance.Enable();
            Instance._isInitialized = true;
            Debug.Log($"Initialize");
        }

        private void Uninitialize()
        {
            Disable();

            EditorApplication.quitting -= Instance.Uninitialize;
        }

        private void Enable()
        {
            Get<EditorUpdateService>().Initialize();
            Get<PreviewSoundPlayerService>().Initialize();
        }

        private void Disable()
        {
            Get<EditorUpdateService>().Destroy();
            Get<PreviewSoundPlayerService>().Destroy();
        }

        [Button(ButtonSizes.Large)]
        private void Reset()
        {
            _isInitialized = false;
            _container.Clear();
            Initialize();
        }

        [Button(ButtonSizes.Large)]
        private void Init()
        {
            Initialize();
        }
    }
}