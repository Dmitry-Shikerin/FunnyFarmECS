using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class SoundySettingsPresenter : IPresenter
    {
        private readonly SoundySettings _soundySettings;
        private readonly ISoundySettingsView _view;

        public SoundySettingsPresenter(
            SoundySettings soundySettings,
            ISoundySettingsView view)
        {
            _soundySettings = soundySettings ?? throw new ArgumentNullException(nameof(soundySettings));
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Initialize()
        {
            _view.SetAutoKillIdleControllers(_soundySettings.AutoKillIdleControllers);
            _view.SetControllerAutoKillDuration(
                new Vector2Int(
                    (int)SoundySettings.CONTROLLER_IDLE_KILL_DURATION_MIN, 
                    (int)SoundySettings.CONTROLLER_IDLE_KILL_DURATION_MAX),
                (int)_soundySettings.ControllerIdleKillDuration);
            _view.SetIdleCheckInterval(new Vector2Int(
                (int)SoundySettings.IDLE_CHECK_INTERVAL_MIN, 
                (int)SoundySettings.IDLE_CHECK_INTERVAL_MAX),
                (int)_soundySettings.IdleCheckInterval);
            _view.SetMinimumNumberOfControllers(new Vector2Int(
                SoundySettings.MINIMUM_NUMBER_OF_CONTROLLERS_MIN, 
                SoundySettings.MINIMUM_NUMBER_OF_CONTROLLERS_MAX),
                _soundySettings.MinimumNumberOfControllers);
        }

        public void Dispose()
        {
            
        }

        public void ChangeIdleCheckInterval(int value) =>
            _soundySettings.IdleCheckInterval = value;

        public void ChangeKillDuration(int value) =>
            _soundySettings.ControllerIdleKillDuration = value;

        public void ChangeMinControllersCount(int value) =>
            _soundySettings.MinimumNumberOfControllers = value;

        public void ResetKillDuration()
        {
            _soundySettings.ControllerIdleKillDuration = SoundySettings.CONTROLLER_IDLE_KILL_DURATION_DEFAULT_VALUE;
            _view.SetControllerAutoKillDuration(
                new Vector2Int(
                    (int)SoundySettings.CONTROLLER_IDLE_KILL_DURATION_MIN, 
                    (int)SoundySettings.CONTROLLER_IDLE_KILL_DURATION_MAX),
                (int)_soundySettings.ControllerIdleKillDuration);
        }

        public void ResetIdleCheckInterval()
        {
            _soundySettings.IdleCheckInterval = SoundySettings.IDLE_CHECK_INTERVAL_DEFAULT_VALUE;
            _view.SetIdleCheckInterval(
                new Vector2Int(
                    (int)SoundySettings.IDLE_CHECK_INTERVAL_MIN, 
                    (int)SoundySettings.IDLE_CHECK_INTERVAL_MAX),
                (int)_soundySettings.IdleCheckInterval);
        }

        public void ResetMinControllersCount()
        {
            _soundySettings.MinimumNumberOfControllers = SoundySettings.MINIMUM_NUMBER_OF_CONTROLLERS_DEFAULT_VALUE;
            _view.SetMinimumNumberOfControllers(
                new Vector2Int(
                    SoundySettings.MINIMUM_NUMBER_OF_CONTROLLERS_MIN, 
                    SoundySettings.MINIMUM_NUMBER_OF_CONTROLLERS_MAX),
                _soundySettings.MinimumNumberOfControllers);
        }
    }
}