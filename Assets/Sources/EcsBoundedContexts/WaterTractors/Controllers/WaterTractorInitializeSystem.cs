using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.WaterTractors.Infrastructure.Factories;

namespace Sources.EcsBoundedContexts.WaterTractors.Controllers
{
    public class WaterTractorInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _rootGameObject;
        private readonly WaterTractorEntityFactory _entityFactory;

        public WaterTractorInitializeSystem(
            RootGameObject rootGameObject, 
            WaterTractorEntityFactory entityFactory)
        {
            _rootGameObject = rootGameObject;
            _entityFactory = entityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            _entityFactory.Create(_rootGameObject.WaterTractorView);
        }
    }
}