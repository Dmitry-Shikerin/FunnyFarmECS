using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class SoundyDataBasePresenter : IPresenter
    {
        private readonly SoundyDataBase _soundyDatabase;
        private readonly SoundySettings _soundySettings;
        private readonly ISoundyDataBaseView _view;
        private readonly ISoundDataBaseViewFactory _soundDataBaseViewFactory;
        private readonly ISoundySettingsViewFactory _soundySettingsViewFactory;
        private readonly ISoundyPrefsStorage _soundyPrefsStorage;

        public SoundyDataBasePresenter(
            SoundyDataBase soundyDatabase,
            SoundySettings soundySettings,
            ISoundyDataBaseView view,
            ISoundDataBaseViewFactory soundDataBaseViewFactory,
            ISoundySettingsViewFactory soundySettingsViewFactory,
            ISoundyPrefsStorage soundyPrefsStorage)
        {
            _soundyDatabase = soundyDatabase ?? throw new ArgumentNullException(nameof(soundyDatabase));
            _soundySettings = soundySettings ?? throw new ArgumentNullException(nameof(soundySettings));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _soundDataBaseViewFactory = soundDataBaseViewFactory ??
                                        throw new ArgumentNullException(nameof(soundDataBaseViewFactory));
            _soundySettingsViewFactory = soundySettingsViewFactory ??
                                         throw new ArgumentNullException(nameof(soundySettingsViewFactory));
            _soundyPrefsStorage = soundyPrefsStorage ?? throw new ArgumentNullException(nameof(soundyPrefsStorage));
        }

        public void Initialize()
        {
            AddDataBasesButtons();
            InitLastTab();
        }

        public void Dispose()
        {
        }

        private void InitLastTab()
        {
            string tabName = _soundyPrefsStorage.GetLastDataTab();
            
            Debug.Log(tabName);

            if (string.IsNullOrEmpty(tabName))
                return;

            if (tabName == ISoundyPrefsStorage.SoundySettings)
            {
                OpenSettings();

                return;
            }

            _view.ClickDataBaseButton(tabName);
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
                _view.AddDataBaseButton(database.Name, () =>
                {
                    CreateView(database);
                    _soundyPrefsStorage.SaveLastDataTab(database.Name);
                    Debug.Log($"Create view {database.Name}");
                });
        }

        public void CreateNewDataBase()
        {
            _soundyDatabase.CreateSoundDatabase("Default_SoundDatabase");
            _view.RefreshDataBasesButtons();
            AddDataBasesButtons();
        }

        public void RefreshDataBases() =>
            _soundyDatabase.RefreshDatabase();

        public void RenameButtons()
        {
            List<string> names = _soundyDatabase
                .GetSoundDatabases()
                .Select(database => database.Name)
                .ToList();
            _view.RenameDataBaseButtons(names);
        }

        public void UpdateDataBase()
        {
            _view.ClearButtons();
            AddDataBasesButtons();
        }

        public void OpenSettings()
        {
            ISoundySettingsView view = _soundySettingsViewFactory.Create(_soundySettings);
            _view.AddSettings(view);
            _soundyPrefsStorage.SaveLastDataTab(ISoundyPrefsStorage.SoundySettings);
        }
    }
}