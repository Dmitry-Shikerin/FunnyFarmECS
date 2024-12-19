using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public interface ISoundyDataBaseViewFactory
    {
        ISoundyDataBaseView Create(SoundyDataBase soundyDatabase, SoundySettings soundySettings);
    }
}