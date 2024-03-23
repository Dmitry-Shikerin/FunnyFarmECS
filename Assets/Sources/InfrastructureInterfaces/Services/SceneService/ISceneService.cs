using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Services.SceneService
{
    public interface ISceneService
    {
        UniTask LoadSceneAsync(string sceneName, object payload = null);
    }
}