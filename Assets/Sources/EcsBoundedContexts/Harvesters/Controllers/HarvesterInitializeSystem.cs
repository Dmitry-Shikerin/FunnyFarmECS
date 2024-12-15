using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Harvesters.Infrastructure;

namespace Sources.EcsBoundedContexts.Harvesters.Controllers
{
    public class HarvesterInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _rootGameObject;
        private readonly HarvesterEntityFactory _entityFactory;

        public HarvesterInitializeSystem(
            RootGameObject rootGameObject,
            HarvesterEntityFactory entityFactory)
        {
            _rootGameObject = rootGameObject;
            _entityFactory = entityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            _entityFactory.Create(_rootGameObject.HarvesterView);
        }
    }
}