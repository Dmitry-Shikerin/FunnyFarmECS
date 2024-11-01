using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundGroupDataViewFactory
    {
        private readonly EditorUpdateService _editorUpdateService;
        private readonly PreviewSoundPlayerService _previewSoundPlayerService;

        public SoundGroupDataViewFactory(
            EditorUpdateService editorUpdateService,
            PreviewSoundPlayerService previewSoundPlayerService)
        {
            _editorUpdateService = editorUpdateService ?? throw new ArgumentNullException(nameof(editorUpdateService));
            _previewSoundPlayerService = previewSoundPlayerService ?? 
                                         throw new ArgumentNullException(nameof(previewSoundPlayerService));
        }

        public ISoundGroupDataView Create(SoundGroupData soundGroupData)
        {
            AudioDataViewFactory audioDataViewFactory = new AudioDataViewFactory(_previewSoundPlayerService);
            
            SoundGroupDataView view = new SoundGroupDataView();
            SoundGroupDataPresenter presenter = new SoundGroupDataPresenter(
                soundGroupData, view, audioDataViewFactory, _editorUpdateService);
            SoundGroupDataVisualElement visualElement = new SoundGroupDataVisualElement();
            view.Construct(presenter, visualElement);
            
            return view;
        }
    }
}