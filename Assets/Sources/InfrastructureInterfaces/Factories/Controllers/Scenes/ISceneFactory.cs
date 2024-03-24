using Cysharp.Threading.Tasks;
using Sources.ControllerInterfaces.Scenes;

namespace Sources.InfrastructureInterfaces.Factories.Controllers.Scenes
{
    public interface ISceneFactory
    {
        UniTask<IScene> Create(object payload);
    }
}