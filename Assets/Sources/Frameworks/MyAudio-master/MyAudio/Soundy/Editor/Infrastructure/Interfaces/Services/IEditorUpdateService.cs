namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public interface IEditorUpdateService : IActionRegisterer<float>
    {
        void Initialize();
        void Destroy();
    }
}