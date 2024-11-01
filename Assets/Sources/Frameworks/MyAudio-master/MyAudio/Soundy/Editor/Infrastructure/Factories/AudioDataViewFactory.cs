using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class AudioDataViewFactory
    {
        private readonly PreviewSoundPlayerService _previewSoundPlayerService;

        public AudioDataViewFactory(PreviewSoundPlayerService previewSoundPlayerService)
        {
            _previewSoundPlayerService = previewSoundPlayerService ?? 
                                         throw new ArgumentNullException(nameof(previewSoundPlayerService));
        }
        
        public IAudioDataView Create(AudioData audioData, SoundGroupData soundGroupData)
        {
            AudioDataView audioDataView = new AudioDataView();
            AudioDataPresenter presenter = new AudioDataPresenter(
                audioData, soundGroupData, audioDataView, _previewSoundPlayerService);
            AudioDataVisualElement visualElement = new AudioDataVisualElement();
            audioDataView.Construct(presenter, visualElement);
            
            return audioDataView;
        }
    }
}