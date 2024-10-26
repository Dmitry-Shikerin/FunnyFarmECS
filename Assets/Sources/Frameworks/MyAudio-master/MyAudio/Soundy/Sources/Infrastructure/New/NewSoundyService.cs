using System;
using System.Collections.Generic;
using System.Threading;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure.New
{
    public class NewSoundyService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly Dictionary<string, Dictionary<string, CancellationTokenSource>> _tokens;

        private List<string> _soundNames;
        private Volume _musicVolume;
        private Volume _soundsVolume;
        private Pause _pause;

        private string _musicSoundName;

        public NewSoundyService(
            Pause pause, 
            Volume soundsVolume,
            Volume musicVolume)
        {
            _pause = pause;
            _soundsVolume = soundsVolume;
            _musicVolume = musicVolume;
            _tokens = new Dictionary<string, Dictionary<string, CancellationTokenSource>>();
        }

        public void Initialize()
        {
            _soundNames = GetSoundNames();
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
            NewSoundyManager.Play(databaseName, soundName);
            NewSoundyManager.SetVolume(soundName, _soundsVolume.VolumeValue);
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

        public void Stop(string database, string sound) =>
            NewSoundyManager.Stop(database, sound);

        private void OnPause(bool isPaused)
        {
            if (isPaused)
            {
                _soundNames.ForEach(NewSoundyManager.Pause);

                return;
            }

            _soundNames.ForEach(NewSoundyManager.UnPause);
        }

        private void OnPauseSound(bool isPausedSound)
        {
            if (isPausedSound)
            {
                NewSoundyManager.PauseAll();
                
                return;
            }
            
            NewSoundyManager.UnpauseAll();
        }


        private void OnSoundsVolumeChanged()
        {
            NewSoundyManager.SetVolumes(
                _musicVolume.VolumeValue,
                _soundsVolume.VolumeValue);
            _soundNames?.ForEach(name => NewSoundyManager.SetVolume(name, _soundsVolume.VolumeValue));
        }

        private void OnMusicVolumeChanged()
        {
            // NewSoundyManager
            //     .GetControllerByName(_musicSoundName)
            //     .AudioSource.volume = _musicVolume.VolumeValue;
        }

        private void OnSoundsVolumeMuted()
        {
            NewSoundyManager.SetMutes(
                _musicVolume.IsVolumeMuted,
                _soundsVolume.IsVolumeMuted);
        }

        private void OnMusicVolumeMuted()
        {
            // NewSoundyManager
            //     .GetControllerByName(_musicSoundName)
            //     .AudioSource.mute = _musicVolume.IsVolumeMuted;
            OnSoundsVolumeMuted();
        }
    }
}