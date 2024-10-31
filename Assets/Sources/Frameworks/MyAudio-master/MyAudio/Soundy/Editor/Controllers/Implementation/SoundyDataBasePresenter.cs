using System;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class SoundyDataBasePresenter : IPresenter
    {
        private readonly SoundyDataBase _soundyDatabase;
        private readonly SoundySettings _soundySettings;
        private readonly ISoundyDataBaseView _view;
        private readonly SoundDataBaseViewFactory _soundDataBaseViewFactory;
        private readonly SoundySettingsViewFactory _soundySettingsViewFactory;

        public SoundyDataBasePresenter(
            SoundyDataBase soundyDatabase,
            SoundySettings soundySettings,
            ISoundyDataBaseView view,
            SoundDataBaseViewFactory soundDataBaseViewFactory,
            SoundySettingsViewFactory soundySettingsViewFactory)
        {
            _soundyDatabase = soundyDatabase ?? throw new ArgumentNullException(nameof(soundyDatabase));
            _soundySettings = soundySettings ?? throw new ArgumentNullException(nameof(soundySettings));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _soundDataBaseViewFactory = soundDataBaseViewFactory ?? 
                                        throw new ArgumentNullException(nameof(soundDataBaseViewFactory));
            _soundySettingsViewFactory = soundySettingsViewFactory ?? throw new ArgumentNullException(nameof(soundySettingsViewFactory));
        }

        public void Initialize()
        {
            AddDataBasesButtons();
        }

        public void Dispose()
        {
            
        }

        private void CreateView(SoundDataBase soundDatabase)
        {
            _view.SettingsView?.Dispose();
            ISoundDataBaseView view = _soundDataBaseViewFactory.Create(soundDatabase, _soundyDatabase);
            view.SetSoundyDataBaseView(_view);
            _view.SetSoundDataBase(view);
        }

        private void AddDataBasesButtons()
        {
            foreach (SoundDataBase database in _soundyDatabase.GetSoundDatabases())
                _view.AddDataBaseButton(database.Name,() => CreateView(database));
        }

        public void CreateNewDataBase()
        {
            _soundyDatabase.CreateSoundDatabase("Default");
            _view.RefreshDataBasesButtons();
            AddDataBasesButtons();
        }

        public void RefreshDataBases() =>
            _soundyDatabase.RefreshDatabase();

        public void RenameButtons()
        {
            for (int i = 0; i < _view.DatabaseButtons.Count; i++)
                _view.DatabaseButtons[i].SetLabelText(_soundyDatabase.GetSoundDatabases().ToList()[i].Name);
        }

        public void UpdateDataBase()
        {
            _view.ClearButtons();
            AddDataBasesButtons();
        }

        public void OpenSettings()
        {
            ISoundySettingsView view =_soundySettingsViewFactory.Create(_soundySettings);
            _view.AddSettings(view);
        }
    }
}