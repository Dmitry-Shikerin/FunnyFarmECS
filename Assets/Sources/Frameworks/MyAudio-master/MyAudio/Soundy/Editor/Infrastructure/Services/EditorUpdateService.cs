
using Sources.Frameworks.GameServices.ActionRegisters.Implementation;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services
{
    public class EditorUpdateService : ActionRegisterer<float>, IInitialize, IDestroy
    {
        public void Initialize()
        {
            EditorApplication.update += Update;
            Debug.Log($"Init EditorUpdateService");
        }

        public void Destroy()
        {
            EditorApplication.update -= Update;
            Debug.Log($"Destroy EditorUpdateService");
        }

        private void Update()
        {
            for (int i = Actions.Count; i > 0; i--)
                Actions[i](Time.deltaTime);
            Debug.Log($"Update EditorUpdateService");
        }
    }
}