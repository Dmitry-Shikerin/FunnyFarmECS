using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class AudioDataViewFactory : IAudioDataViewFactory
    {
        private readonly IPreviewSoundPlayerService _previewSoundPlayerService;

        public AudioDataViewFactory(IPreviewSoundPlayerService previewSoundPlayerService)
        {
            _previewSoundPlayerService = previewSoundPlayerService ?? 
                                         throw new ArgumentNullException(nameof(previewSoundPlayerService));
        }
        
        public IAudioDataView Create(AudioData audioData, SoundGroupData soundGroupData, SoundyDataBase soundyDataBase)
        {
            AudioDataView audioDataView = new AudioDataView();
            AudioDataPresenter presenter = new AudioDataPresenter(
                audioData, soundGroupData, soundyDataBase, audioDataView, _previewSoundPlayerService);
            AudioDataVisualElement visualElement = new AudioDataVisualElement();
            audioDataView.Construct(presenter, visualElement);
            
            return audioDataView;
        }
    }
}