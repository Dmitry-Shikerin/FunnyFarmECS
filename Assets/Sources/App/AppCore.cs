using System;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Services.SceneService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.App
{
    public class AppCore : MonoBehaviour
    {
        private ISceneService _sceneService;

        private void Awake() => 
            DontDestroyOnLoad(this);

        private void Start() =>
            _sceneService.ChangeSceneAsync(SceneManager.GetActiveScene().name, null);

        private void Update() =>
            _sceneService.Update(Time.deltaTime);

        private void LateUpdate() =>
            _sceneService.UpdateLate(Time.deltaTime);

        private void FixedUpdate() =>
            _sceneService.UpdateFixed(Time.fixedDeltaTime);

        public void Construct(ISceneService sceneService) => 
            _sceneService = sceneService ?? throw new NullReferenceException(nameof(sceneService));
    }
}

