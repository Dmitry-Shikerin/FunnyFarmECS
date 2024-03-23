using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Infrastructure.StateMachines.SceneStateMachines;
using Sources.InfrastructureInterfaces.Services.SceneService;

namespace Sources.Infrastructure.Services.SceneServices
{
    public class SceneService : ISceneService
    {
        private readonly List<Func<string, UniTask>> _enteringHandlers = new List<Func<string, UniTask>>();
        private readonly List<Func<UniTask>> _exitingHandlers = new List<Func<UniTask>>();
        
        private readonly ISceneStateMachine _stateMachine;

        public SceneService()
        {
            
            _stateMachine = new SceneStateMachine();
        }

        public UniTask ChangeSceneAsync(string sceneName, object payload = null)
        {
            return UniTask.CompletedTask;
        }

        public void Disable() =>
            _stateMachine.Exit();

        public void Update(float deltaTime) =>
            _stateMachine.Update(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) =>
            _stateMachine.UpdateFixed(fixedDeltaTime);

        public void UpdateLate(float deltaTime) =>
            _stateMachine.UpdateLate(deltaTime);
    }
}