using System;
using System.Runtime.CompilerServices;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation
{
    public class SoundDataBasePresenter : IPresenter
    {
        private readonly SoundDatabase _soundDatabase;
        private readonly SoundyDatabase _soundyDatabase;
        private readonly ISoundDataBaseView _view;
        private readonly SoundGroupViewFactory _soundGroupViewFactory;

        public SoundDataBasePresenter(
            SoundDatabase soundDatabase,
            SoundyDatabase soundyDatabase,
            ISoundDataBaseView view,
            SoundGroupViewFactory soundGroupViewFactory)
        {
            _soundDatabase = soundDatabase ?? throw new ArgumentNullException(nameof(soundDatabase));
            _soundyDatabase = soundyDatabase ?? throw new ArgumentNullException(nameof(soundyDatabase));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _soundGroupViewFactory = soundGroupViewFactory;
        }

        public void Initialize()
        {
            CreateSoundGroups();
            _view.SetName(_soundDatabase.DatabaseName);
            _view.SetAudioMixerGroup(_soundDatabase.OutputAudioMixerGroup);
        }

        public void Dispose()
        {
        }

        private void CreateSoundGroups()
        {
            foreach (SoundGroupData soundGroup in _soundDatabase.Database)
                CreateView(soundGroup);
        }

        public SoundDatabase GetDataBase() =>
            _soundDatabase;

        public void RenameDataBase(string name)
        {
            if (_soundyDatabase.RenameSoundDatabase(_soundDatabase, name) == false)
                return;

            _view.SetName(name);
            _view.RenameDataBaseButtons();
        }

        public void CreateSoundGroup(string value)
        {
            SoundGroupData soundGroup = _soundDatabase.Add(value, true, true);
            CreateView(soundGroup);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateView(SoundGroupData soundGroup)
        {
            ISoundGroupView view = _soundGroupViewFactory.Create(soundGroup, _soundDatabase);
            view.SetDataBase(_view);
            _view.AddSoundGroup(view);
        }

        public void RemoveDataBase()
        {
            _soundyDatabase.DeleteDatabase(_soundDatabase);
            _view.Dispose();
        }

        public void ChangeAudioMixerGroup(AudioMixerGroup audioMixerGroup) =>
            _soundDatabase.OutputAudioMixerGroup = audioMixerGroup;
    }
}