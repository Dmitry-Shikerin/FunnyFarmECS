using Sources.ControllerInterfaces.Scenes;
using Sources.InfrastructureInterfaces.Factories.Controllers.Scenes;

namespace Sources.Controllers.Scenes
{
    public class GameplayScene : IScene
    {
        public GameplayScene()
        {
        }

        public void Enter(object payload = null)
        {
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }
    }
}