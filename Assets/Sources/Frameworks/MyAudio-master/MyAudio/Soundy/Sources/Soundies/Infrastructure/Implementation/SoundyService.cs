using System;
using System.Collections.Generic;
using System.Threading;
using Doozy.Engine.Soundy;
using MyAudios.Soundy.Sources.AudioControllers.Controllers;
using MyAudios.Soundy.Sources.DataBases.Domain.Data;
using MyAudios.Soundy.Sources.Managers.Controllers;
using MyAudios.Soundy.Sources.Settings.Domain.Configs;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Soundies.Infrastructure.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Soundies.Infrastructure.Implementation
{
    public class SoundyService : ISoundyService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly Dictionary<string, Dictionary<string, CancellationTokenSource>> _tokens;

        private List<string> _soundNames;
        private Volume _musicVolume;
        private Volume _soundsVolume;
        private Pause _pause;

        private string _musicSoundName;

        public SoundyService(
            IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _tokens = new Dictionary<string, Dictionary<string, CancellationTokenSource>>();
        }

        public void Initialize()
        {
            _pause = _entityRepository.Get<Pause>(ModelId.Pause);
            _soundNames = GetSoundNames();
            _musicVolume = _entityRepository.Get<Volume>(ModelId.MusicVolume);
            _soundsVolume = _entityRepository.Get<Volume>(ModelId.SoundsVolume);
            OnSoundsVolumeChanged();
            _pause.PauseChanged += OnPause;
            _pause.PauseSoundChanged += OnPauseSound;
            _musicVolume.VolumeChanged += OnMusicVolumeChanged;
            _soundsVolume.VolumeChanged += OnSoundsVolumeChanged;
            _musicVolume.VolumeMuted += OnMusicVolumeMuted;
            _soundsVolume.VolumeMuted += OnSoundsVolumeMuted;
        }

        public void Destroy()
        {
            _pause.PauseChanged -= OnPause;
            _pause.PauseSoundChanged -= OnPauseSound;
            _musicVolume.VolumeChanged -= OnMusicVolumeChanged;
            _soundsVolume.VolumeChanged -= OnSoundsVolumeChanged;
            _musicVolume.VolumeMuted -= OnMusicVolumeMuted;
            _soundsVolume.VolumeMuted -= OnSoundsVolumeMuted;
            SoundyManager.ClearTokens();
        }

        public void Play(string databaseName, string soundName, Vector3 position) =>
            SoundyManager.Play(databaseName, soundName, position);

        public void Play(string databaseName, string soundName)
        {
            SoundyManager.Play(databaseName, soundName);
            SoundyManager.SetVolume(soundName, _soundsVolume.VolumeValue);
        }

        public List<string> GetSoundNames()
        {
            List<string> soundNames = new List<string>();

            foreach (SoundDatabase soundDatabase in SoundySettings.Database.SoundDatabases)
            {
                foreach (SoundGroupData soundGroupData in soundDatabase.Database)
                {
                    if (soundGroupData.SoundName == _musicSoundName)
                        continue;

                    soundNames.Add(soundGroupData.SoundName);
                }
            }

            return soundNames;
        }

        public void PlaySequence(string databaseName, string soundName)
        {
            _musicSoundName = soundName;
            _soundNames = GetSoundNames();
            SoundyManager.PlaySequence(databaseName, soundName, _musicVolume);
        }

        public void StopSequence(string databaseName, string soundName) =>
            SoundyManager.StopSequence(databaseName, soundName);

        public void Stop(string database, string sound) =>
            SoundyManager.Stop(database, sound);

        private void OnPause(bool isPaused)
        {
            if (isPaused)
            {
                _soundNames.ForEach(SoundyManager.Pause);

                return;
            }

            _soundNames.ForEach(SoundyManager.UnPause);
        }

        private void OnPauseSound(bool isPausedSound)
        {
            if (isPausedSound)
            {
                SoundyManager.PauseAllControllers();
                
                return;
            }
            
            SoundyManager.UnpauseAllControllers();
        }


        private void OnSoundsVolumeChanged()
        {
            SoundyManager.SetVolumes(
                _musicVolume.VolumeValue,
                _soundsVolume.VolumeValue);
            _soundNames?.ForEach(name => SoundyManager.SetVolume(name, _soundsVolume.VolumeValue));
        }

        private void OnMusicVolumeChanged()
        {
            SoundyController
                .GetControllerByName(_musicSoundName)
                .AudioSource.volume = _musicVolume.VolumeValue;
        }

        private void OnSoundsVolumeMuted()
        {
            SoundyManager.SetMutes(
                _musicVolume.IsVolumeMuted,
                _soundsVolume.IsVolumeMuted);
        }

        private void OnMusicVolumeMuted()
        {
            SoundyController
                .GetControllerByName(_musicSoundName)
                .AudioSource.mute = _musicVolume.IsVolumeMuted;
            OnSoundsVolumeMuted();
        }
    }
}