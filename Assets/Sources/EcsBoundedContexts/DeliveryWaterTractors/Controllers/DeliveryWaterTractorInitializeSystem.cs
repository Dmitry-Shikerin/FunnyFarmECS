using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Infrastructure.Factories;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Controllers
{
    public class DeliveryWaterTractorInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _rootGameObject;
        private readonly DeliveryWaterTractorEntityFactory _entityFactory;

        public DeliveryWaterTractorInitializeSystem(
            RootGameObject rootGameObject, 
            DeliveryWaterTractorEntityFactory entityFactory)
        {
            _rootGameObject = rootGameObject;
            _entityFactory = entityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            _entityFactory.Create(_rootGameObject.DeliveryWaterTractorView);
        }
    }
}