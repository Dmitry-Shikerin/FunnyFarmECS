using System;
using JetBrains.Annotations;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundDataBaseViewFactory
    {
        private readonly EditorUpdateService _editorUpdateService;
        private readonly PreviewSoundPlayerService _previewSoundPlayerService;

        public SoundDataBaseViewFactory(
            EditorUpdateService editorUpdateService,
            PreviewSoundPlayerService previewSoundPlayerService)
        {
            _editorUpdateService = editorUpdateService ?? throw new ArgumentNullException(nameof(editorUpdateService));
            _previewSoundPlayerService = previewSoundPlayerService ?? throw new ArgumentNullException(nameof(previewSoundPlayerService));
        }

        public ISoundDataBaseView Create(SoundDataBase soundDatabase, SoundyDataBase soundyDatabase)
        {
            SoundGroupViewFactory soundGroupViewFactory = new SoundGroupViewFactory(
                _editorUpdateService, _previewSoundPlayerService);
            SoundDataBaseView view = new SoundDataBaseView();
            SoundDataBasePresenter presenter = new SoundDataBasePresenter(
                soundDatabase, soundyDatabase, view,  soundGroupViewFactory, _editorUpdateService);
            SoundDataBaseVisualElement visualElement = new SoundDataBaseVisualElement();
            view.Construct(presenter, visualElement);
            
            return view;
        }
    }
}