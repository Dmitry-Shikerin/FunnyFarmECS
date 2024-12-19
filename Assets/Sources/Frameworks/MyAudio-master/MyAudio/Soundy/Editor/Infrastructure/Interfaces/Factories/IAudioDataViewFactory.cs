using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public interface IAudioDataViewFactory
    {
        IAudioDataView Create(AudioData audioData, SoundGroupData soundGroupData, SoundyDataBase soundyDataBase);
    }
}