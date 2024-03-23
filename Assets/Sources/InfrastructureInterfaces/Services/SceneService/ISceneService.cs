using Cysharp.Threading.Tasks;
using Sources.ControllerInterfaces.Lifetimes;
using Sources.InfrastructureInterfaces.StateMachines.States;

namespace Sources.InfrastructureInterfaces.Services.SceneService
{
    public interface ISceneService : IUpdatable, ILateUpdatable, IFixedUpdatable, IDisable
    {
        UniTask ChangeSceneAsync(string sceneName, object payload = null);
    }
}