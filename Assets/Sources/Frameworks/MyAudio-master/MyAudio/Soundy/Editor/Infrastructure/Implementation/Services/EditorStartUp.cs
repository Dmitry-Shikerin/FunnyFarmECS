using UnityEditor;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    [InitializeOnLoad]
    public static class EditorStartUp
    {
        static EditorStartUp()
        {
            EditorServiceLocator.Initialize();
        }
    }
}