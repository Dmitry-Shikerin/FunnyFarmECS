using System;
using JetBrains.Annotations;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundGroupViewFactory : ISoundGroupViewFactory
    {
        private readonly IWindowService _windowService;
        private readonly ISoundyPrefsStorage _soundyPrefsStorage;
        private readonly IPreviewSoundPlayerService _previewSoundPlayerService;

        public SoundGroupViewFactory(
            IWindowService windowService,
            ISoundyPrefsStorage soundyPrefsStorage,
            IPreviewSoundPlayerService previewSoundPlayerService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _soundyPrefsStorage = soundyPrefsStorage ?? throw new ArgumentNullException(nameof(soundyPrefsStorage));
            _previewSoundPlayerService = previewSoundPlayerService ?? 
                                         throw new ArgumentNullException(nameof(previewSoundPlayerService));
        }

        public ISoundGroupView Create(SoundGroupData soundGroup, SoundDataBase soundDatabase)
        {
            SoundGroupView view = new SoundGroupView();
            SoundGroupPresenter presenter = new SoundGroupPresenter(
                soundGroup, soundDatabase, view, _windowService, _previewSoundPlayerService, _soundyPrefsStorage);
            SoundGroupVisualElement visualElement = new SoundGroupVisualElement();
            view.Construct(presenter, visualElement);
            
            return view;
        }
    }
}