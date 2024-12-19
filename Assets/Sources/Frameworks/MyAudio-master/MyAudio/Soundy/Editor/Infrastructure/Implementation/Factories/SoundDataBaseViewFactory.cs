using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundDataBaseViewFactory : ISoundDataBaseViewFactory
    {
        private readonly IWindowService _windowService;
        private readonly ISoundyPrefsStorage _soundyPrefsStorage;
        private readonly IPreviewSoundPlayerService _previewSoundPlayerService;

        public SoundDataBaseViewFactory(
            IWindowService windowService,
            ISoundyPrefsStorage soundyPrefsStorage,
            IPreviewSoundPlayerService previewSoundPlayerService)
        {
            _windowService = windowService;
            _soundyPrefsStorage = soundyPrefsStorage ?? throw new ArgumentNullException(nameof(soundyPrefsStorage));
            _previewSoundPlayerService = previewSoundPlayerService ?? throw new ArgumentNullException(nameof(previewSoundPlayerService));
        }

        public ISoundDataBaseView Create(SoundDataBase soundDatabase, SoundyDataBase soundyDatabase)
        {
            SoundGroupViewFactory soundGroupViewFactory = new SoundGroupViewFactory(
                _windowService, _soundyPrefsStorage, _previewSoundPlayerService);
            SoundDataBaseView view = new SoundDataBaseView();
            SoundDataBasePresenter presenter = new SoundDataBasePresenter(
                soundDatabase, soundyDatabase, view,  soundGroupViewFactory);
            SoundDataBaseVisualElement visualElement = new SoundDataBaseVisualElement();
            view.Construct(presenter, visualElement);
            
            return view;
        }
    }
}