using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class AudioDataViewFactory
    {
        public IAudioDataView Create(AudioData audioData, SoundGroupData soundGroupData)
        {
            AudioDataView audioDataView = new AudioDataView();
            AudioDataPresenter presenter = new AudioDataPresenter(audioData, soundGroupData, audioDataView);
            AudioDataVisualElement visualElement = new AudioDataVisualElement();
            audioDataView.Construct(presenter, visualElement);
            
            return audioDataView;
        }
    }
}