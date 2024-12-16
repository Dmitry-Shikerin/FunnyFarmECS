using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.DeliveryCars.Infrastructure.Factories;

namespace Sources.EcsBoundedContexts.DeliveryCars.Controllers
{
    public class DeliveryCarInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _rootGameObject;
        private readonly DeliveryCarEntityFactory _entityFactory;

        public DeliveryCarInitializeSystem(
            RootGameObject rootGameObject,
            DeliveryCarEntityFactory entityFactory)
        {
            _rootGameObject = rootGameObject;
            _entityFactory = entityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            _entityFactory.Create(_rootGameObject.JeepView.DeliveryCarView);
            _entityFactory.Create(_rootGameObject.TruckView.DeliveryCarView);
        }
    }
}