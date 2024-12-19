using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public interface ISoundGroupDataViewFactory
    {
        ISoundGroupDataView Create(SoundGroupData soundGroupData, SoundyDataBase soundyDataBase);
    }
}