using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

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
            SetControllerAutoKillDuration();
            SetIdleCheckInterval();
            SetMinimumNumberOfControllers();
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
            _soundySettings.ResetAutoKillIdleControllers();
            SetControllerAutoKillDuration();
        }

        public void ResetIdleCheckInterval()
        {
            _soundySettings.ResetIdleCheckInterval();
            SetIdleCheckInterval();
        }

        public void ResetMinControllersCount()
        {
            _soundySettings.ResetMinimumNumberOfControllers();
            SetMinimumNumberOfControllers();
        }

        private void SetIdleCheckInterval()
        {
            _view.SetIdleCheckInterval(
                SoundySettingsConst.MixMaxIdleCheckInterval,
                (int)_soundySettings.IdleCheckInterval);
        }
        
        private void SetControllerAutoKillDuration()
        {
            _view.SetControllerAutoKillDuration(
                SoundySettingsConst.MinMaxControllersIdleKillDuration,
                (int)_soundySettings.ControllerIdleKillDuration);
        }
        
        private void SetMinimumNumberOfControllers()
        {
            _view.SetMinimumNumberOfControllers(
                SoundySettingsConst.MixMaxMinimumNumberOfControllers,
                _soundySettings.MinimumNumberOfControllers);
        }
    }
}