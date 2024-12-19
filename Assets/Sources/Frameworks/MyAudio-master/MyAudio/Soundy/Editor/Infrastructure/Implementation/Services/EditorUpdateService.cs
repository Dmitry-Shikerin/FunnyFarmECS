using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public class EditorUpdateService : ActionRegisterer<float>, IEditorUpdateService
    {
        public void Initialize() =>
            EditorApplication.update += Update;

        public void Destroy() =>
            EditorApplication.update -= Update;

        private void Update()
        {
            for (int i = Actions.Count - 1; i >= 0; i--)
                Actions[i].Invoke(Time.deltaTime);
        }
    }
}