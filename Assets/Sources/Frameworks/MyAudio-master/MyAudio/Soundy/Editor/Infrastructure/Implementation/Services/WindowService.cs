using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public class WindowService : IWindowService
    {
        public void OpenSoundGroupDataEditorWindow()
        {
            SoundGroupDataEditorWindow.Open();
        }
    }
}