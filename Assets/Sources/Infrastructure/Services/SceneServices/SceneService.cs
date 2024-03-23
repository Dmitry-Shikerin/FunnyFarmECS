using Cysharp.Threading.Tasks;
using Sources.InfrastructureInterfaces.Services.SceneService;

namespace Sources.Infrastructure.Services.SceneServices
{
    public class SceneService : ISceneService
    {
        public SceneService()
        {
        }

        public UniTask LoadSceneAsync(string sceneName, object payload = null)
        {
            return UniTask.CompletedTask;
        }
    }
}