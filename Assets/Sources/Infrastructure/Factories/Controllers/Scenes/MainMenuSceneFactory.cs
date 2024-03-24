using Cysharp.Threading.Tasks;
using Sources.ControllerInterfaces.Scenes;
using Sources.Controllers.Scenes;
using Sources.InfrastructureInterfaces.Factories.Controllers.Scenes;

namespace Sources.Infrastructure.Factories.Controllers.Scenes
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        public MainMenuSceneFactory()
        {
        }

        public async UniTask<IScene> Create(object payload)
        {
            return new MainMenuScene();
        }
    }
}