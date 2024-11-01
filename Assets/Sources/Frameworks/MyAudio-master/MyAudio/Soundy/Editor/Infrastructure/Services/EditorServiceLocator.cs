using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.GameServices.Singletones.ScriptableObjects;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Domain;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    [CreateAssetMenu(fileName = "EditorServiceLocator", menuName = "MyAudio/EditorServiceLocator", order = 51)]
    public class EditorServiceLocator : ScriptableObjectSingleton<EditorServiceLocator>
    {
        [HideInInspector]
        [SerializeReference] private TypeObjectSerializedDictionary _container = new ();
        [HideInInspector]
        [SerializeField] private bool _isInitialized;
        
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

        private static void Bind<T>(T service)
            where T : class
        {
            if (s_container.ContainsKey(typeof(T)))
                throw new InvalidOperationException();
            
            s_container[typeof(T)] = service;
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            AssetName = "EditorServiceLocator";
            ResourcesPath = "Services";
            
            if (Instance._isInitialized)
                return;

            EditorApplication.quitting += Instance.Uninitialize;
            
            Bind(new EditorUpdateService());
            Bind(new SoundyPrefsStorage());
            Bind(new PreviewSoundPlayerService(Get<EditorUpdateService>()));
            Bind(new AudioDataViewFactory(Get<PreviewSoundPlayerService>()));
            Bind(new SoundDataBaseViewFactory(
                Get<SoundyPrefsStorage>(),
                Get<PreviewSoundPlayerService>()));
            Bind(new SoundGroupDataViewFactory(
                Get<EditorUpdateService>(),
                Get<PreviewSoundPlayerService>()));
            Bind(new SoundGroupViewFactory(
                Get<SoundyPrefsStorage>(),
                Get<PreviewSoundPlayerService>()));
            Bind(new SoundyDataBaseViewFactory(
                Get<SoundyPrefsStorage>(),
                Get<PreviewSoundPlayerService>()));
            Bind(new SoundySettingsViewFactory());
            
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