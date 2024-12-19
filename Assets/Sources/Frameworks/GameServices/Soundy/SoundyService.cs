using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure
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
            _musicVolume = _entityRepository.Get<Volume>(ModelId.MusicVolume);
            _soundsVolume = _entityRepository.Get<Volume>(ModelId.SoundsVolume);
            
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
        }

        public void Play(string databaseName, string soundName)
        {
            SoundyManager.Play(databaseName, soundName);
            SoundyManager.SetVolume(soundName, _soundsVolume.VolumeValue);
        }

        public List<string> GetSoundNames()
        {
            List<string> soundNames = new List<string>();

            foreach (SoundDataBase soundDatabase in SoundySettings.Database.GetSoundDatabases())
            {
                foreach (SoundGroupData soundGroupData in soundDatabase.GetSoundDatabases())
                {
                    if (soundGroupData.SoundName == _musicSoundName)
                        continue;

                    soundNames.Add(soundGroupData.SoundName);
                }
            }

            return soundNames;
        }

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
                SoundyManager.PauseAll();
                
                return;
            }
            
            SoundyManager.UnpauseAll();
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
            // NewSoundyManager
            //     .GetControllerByName(_musicSoundName)
            //     .AudioSource.volume = _musicVolume.VolumeValue;
        }

        private void OnSoundsVolumeMuted()
        {
            SoundyManager.SetMutes(
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