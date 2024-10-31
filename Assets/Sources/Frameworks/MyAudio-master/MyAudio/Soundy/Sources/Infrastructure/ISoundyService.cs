using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure
{
    public interface ISoundyService : IInitialize, IDestroy
    {
        void Play(string databaseName, string soundName);
        void Stop(string database, string sound);
    }
}