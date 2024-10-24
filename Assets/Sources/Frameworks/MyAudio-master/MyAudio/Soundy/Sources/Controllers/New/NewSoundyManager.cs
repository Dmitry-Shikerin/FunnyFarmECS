using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyAudios.MyUiFramework.Utils;
using Sources.Frameworks.GameServices.Singletones.Monobehaviours;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
using TeoGames.Mesh_Combiner.Scripts.Extension;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New
{
    [AddComponentMenu(SoundyManagerConstant.SoundyManagerAddComponentMenuMenuName, SoundyManagerConstant.SoundyManagerAddComponentMenuOrder)]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(SoundyExecutionOrder.SoundyManager)]
    public class NewSoundyManager : MonoBehaviourSingleton<NewSoundyManager>
    {
//         private SoundControllersPool _pool;
//         private float _musicVolume;
//         private float _soundVolume;
//         private bool _isMusicVolumeUnMuted;
//         private bool _isSoundVolumeUnMuted;
//         private bool _aplicationIsQuitting;
//         private bool _initialized;
//
//         private SoundyDatabase Database => SoundySettings.Database;
//         
// #if UNITY_EDITOR
//         [MenuItem(SoundyManagerConstant.SoundyManagerMenuItemItemName, false,
//             SoundyManagerConstant.SoundyManagerMenuItemPriority)]
//         private static void CreateComponent(MenuCommand menuCommand)
//         {
//             SoundyManager addToScene = AddToScene(true);
//         }
// #endif
//         
//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
//         private static void RunOnStart()
//         {
//             _aplicationIsQuitting = false;
//             _initialized = false;
//             s_pooler = null;
//         }
//         
//         public static SoundyManager AddToScene(bool selectGameObjectAfterCreation = false) =>
//             MyUtils.AddToScene<SoundyManager>(
//                 SoundyManagerConstant.SoundyManagerGameObjectName, true, selectGameObjectAfterCreation);
//         
//         private void Awake()
//         {
//             _pool = new SoundControllersPool(transform);
//             _pool.Initialize();
//             _initialized = true;
//         }
//
//         private void OnDestroy()
//         {
//             _pool.Destroy();
//         }
//         
//
//         public static SoundyController CreateController() =>
//             SoundyController.CreateController();
//
//         public static string GetSoundDatabaseFilename(string databaseName) =>
//             "SoundDatabase_" + databaseName.Trim();
//
//         public static void Init()
//         {
//             if (_initialized || s_instance != null)
//                 return;
//
//             s_instance = Instance;
//
//             for (int i = 0; i < SoundyPooler.MinimumNumberOfControllers + 1; i++)
//                 SoundyPooler.GetControllerFromPool().Stop();
//         }
//
//         public static void Pause(string soundName)
//         {
//             // SoundyController.Pause(soundName);
//             Instance._pool.Controllers
//                 .Where(controller => controller.Name == soundName)
//                 .ForEach(controller => controller.Pause());
//         }
//
//         public static void UnPause(string soundName)
//         {
//             // SoundyController.Unpause(soundName);
//             Instance._pool.Controllers
//                 .Where(controller => controller.Name == soundName)
//                 .ForEach(controller => controller.Unpause());
//         }
//
//         public static void SetVolumes(float musicVolume, float soundVolume)
//         {
//             Instance._musicVolume = musicVolume;
//             Instance._soundVolume = soundVolume;
//
//             //Todo сделать выборку по звуку и музыке и изменить все громкости
//         }
//
//         public static void SetMutes(bool isMusicMuted, bool isSoundMuted)
//         {
//             Instance._isMusicVolumeUnMuted = isMusicMuted;
//             Instance._isSoundVolumeUnMuted = isSoundMuted;
//         }
//
//         public static void KillAllControllers() =>
//             SoundyController.KillAll();
//
//         public static void MuteAllControllers() =>
//             SoundyController.MuteAll();
//
//         public static void MuteAllSounds()
//         {
//             MuteAllControllers();
// #if dUI_MasterAudio
//             DarkTonic.MasterAudio.MasterAudio.MuteEverything();
// #endif
//         }
//
//         public static void PauseAllControllers() =>
//             SoundyController.PauseAll();
//
//         public static void PauseAllSounds()
//         {
//             PauseAllControllers();
// #if dUI_MasterAudio
//             DarkTonic.MasterAudio.MasterAudio.PauseEverything();
// #endif
//         }
//
//         public static void SetVolume(string soundName, float volume) =>
//             SoundyController.SetVolume(soundName, volume);
//
//         public static async void PlaySequence(
//             string databaseName, 
//             string soundName,
//             Volume musicVolume)
//         {
//             CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
//
//             if (_tokens.ContainsKey(databaseName) == false)
//                 _tokens[databaseName] = new Dictionary<string, CancellationTokenSource>();
//
//             _tokens[databaseName].Add(soundName, cancellationTokenSource);
//
//             try
//             {
//                 while (cancellationTokenSource.Token.IsCancellationRequested == false)
//                 {
//                     SoundyController soundyController = Play(databaseName, soundName);
//                     SetVolume(soundName, musicVolume.VolumeValue);
//                     AudioSource audioSource = soundyController.AudioSource;
//                     audioSource.mute = musicVolume.IsVolumeMuted;
//
//                     // await UniTask.WaitUntil(
//                     //     () => audioSource.time + 0.1f > audioSource.clip.length,
//                     //     cancellationToken: cancellationTokenSource.Token);
//                     await UniTask.WaitUntil(
//                         () => soundyController.IsPlaying == false);
//                 }
//             }
//             catch (OperationCanceledException)
//             {
//                 Stop(databaseName, soundName);
//             }
//         }
//
//         public static void StopSequence(string databaseName, string soundName)
//         {
//             Dictionary<string, CancellationTokenSource> tokens = _tokens[databaseName];
//             tokens[soundName].Cancel();
//             Stop(databaseName, soundName);
//         }
//
//         public static SoundyController Play(string databaseName, string soundName, Vector3 position)
//         {
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             if (Database == null)
//                 return null;
//
//             if (soundName.Equals(SoundyManagerConstant.NoSound))
//                 return null;
//
//             SoundGroupData soundGroupData = Database.GetAudioData(databaseName, soundName);
//
//             if (soundGroupData == null)
//                 return null;
//
//             return soundGroupData.Play(position, Database.GetSoundDatabase(databaseName).OutputAudioMixerGroup);
//         }
//
//         public static SoundyController Play(AudioClip audioClip, Vector3 position)
//         {
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             return Play(audioClip, null, position);
//         }
//
//         public static SoundyController Play(string databaseName, string soundName, Transform followTarget)
//         {
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             if (Database == null)
//                 return null;
//
//             if (soundName.Equals(SoundyManagerConstant.NoSound))
//                 return null;
//
//             SoundGroupData soundGroupData = Database.GetAudioData(databaseName, soundName);
//
//             if (soundGroupData == null)
//                 return null;
//
//             return soundGroupData.Play(followTarget, Database.GetSoundDatabase(databaseName).OutputAudioMixerGroup);
//         }
//
//         public static SoundyController Play(AudioClip audioClip, Transform followTarget)
//         {
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             return Play(audioClip, null, followTarget);
//         }
//
//         public static SoundyController Play(string databaseName, string soundName)
//         {
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             if (Database == null)
//                 return null;
//
//             if (soundName.Equals(SoundyManagerConstant.NoSound))
//                 return null;
//
//             if (string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(databaseName.Trim()))
//                 return null;
//
//             if (string.IsNullOrEmpty(soundName) || string.IsNullOrEmpty(soundName.Trim()))
//                 return null;
//
//             SoundDatabase soundDatabase = Database.GetSoundDatabase(databaseName);
//
//             if (soundDatabase == null)
//                 return null;
//
//             SoundGroupData soundGroupData = soundDatabase.GetData(soundName);
//
//             if (soundGroupData == null)
//                 return null;
//
//             return soundGroupData.Play(Pooler.transform, soundDatabase.OutputAudioMixerGroup);
//         }
//
//         public static SoundyController Play(AudioClip audioClip)
//         {
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             return Play(audioClip, null, Pooler.transform);
//         }
//
//         public static SoundyController Play(
//             AudioClip audioClip,
//             AudioMixerGroup outputAudioMixerGroup,
//             Vector3 position,
//             float volume = 1,
//             float pitch = 1,
//             bool loop = false,
//             float spatialBlend = 1)
//         {
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             if (audioClip == null)
//                 return null;
//
//             SoundyController controller = SoundyPooler.GetControllerFromPool();
//             controller.SetSourceProperties(audioClip, volume, pitch, loop, spatialBlend);
//             controller.SetOutputAudioMixerGroup(outputAudioMixerGroup);
//             controller.SetPosition(position);
//             controller.gameObject.name = "[AudioClip]-(" + audioClip.name + ")";
//             controller.Name = audioClip.name;
//             controller.Play();
//
//             return controller;
//         }
//
//         public static SoundyController Play(
//             AudioClip audioClip,
//             AudioMixerGroup outputAudioMixerGroup,
//             Transform followTarget = null,
//             float volume = 1,
//             float pitch = 1,
//             bool loop = false,
//             float spatialBlend = 1)
//         {
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             if (audioClip == null)
//                 return null;
//
//             SoundyController controller = SoundyPooler.GetControllerFromPool();
//             controller.SetSourceProperties(audioClip, volume, pitch, loop, spatialBlend);
//             controller.SetOutputAudioMixerGroup(outputAudioMixerGroup);
//
//             if (followTarget == null)
//             {
//                 spatialBlend = 0;
//                 controller.SetFollowTarget(Pooler.transform);
//             }
//             else
//             {
//                 controller.SetFollowTarget(followTarget);
//             }
//
//             controller.gameObject.name = "[AudioClip]-(" + audioClip.name + ")";
//             controller.Name = audioClip.name;
//             controller.Play();
//
//             return controller;
//         }
//
//         public static SoundyController Play(SoundyData data)
//         {
//             if (data == null)
//                 return null;
//
//             if (_initialized == false)
//                 s_instance = Instance;
//
//             switch (data.SoundSource)
//             {
//                 case SoundSource.Soundy:
//                     return Play(data.DatabaseName, data.SoundName);
//                 case SoundSource.AudioClip:
//                     return Play(data.AudioClip, data.OutputAudioMixerGroup);
//                 case SoundSource.MasterAudio:
// #if dUI_MasterAudio
//                     DarkTonic.MasterAudio.MasterAudio.PlaySound(data.SoundName);
// #endif
//                     break;
//             }
//
//             return null;
//         }
//
//         public static SoundyController Play(SoundyData data, bool isSound)
//         {
//             SoundyController controller = Play(data);
//             controller.AudioSource.volume = isSound ? _soundVolume : s_musicVolume;
//             controller.AudioSource.mute = isSound ? _isSoundVolumeUnMuted : _isMusicVolumeUnMuted;
//             
//             return controller;
//         }
//
//         public static void Stop(string databaseName, string soundName) =>
//             SoundyController.Stop(databaseName, soundName);
//
//         public static void StopAllControllers() =>
//             SoundyController.StopAll();
//
//         public static void StopAllSounds()
//         {
//             StopAllControllers();
// #if dUI_MasterAudio
//             DarkTonic.MasterAudio.MasterAudio.StopEverything();
// #endif
//         }
//
//         public static void UnmuteAllControllers() =>
//             SoundyController.UnmuteAll();
//
//         public static void UnmuteAllSounds()
//         {
//             UnmuteAllControllers();
// #if dUI_MasterAudio
//             DarkTonic.MasterAudio.MasterAudio.UnmuteEverything();
// #endif
//         }
//
//         public static void UnpauseAllControllers() =>
//             SoundyController.UnpauseAll();
//
//         public static void UnpauseAllSounds()
//         {
//             UnpauseAllControllers();
// #if dUI_MasterAudio
//             DarkTonic.MasterAudio.MasterAudio.UnpauseEverything();
// #endif
//         }
    }
}