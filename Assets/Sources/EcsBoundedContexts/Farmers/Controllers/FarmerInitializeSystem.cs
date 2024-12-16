using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Farmers.Infrastructure.Factories;

namespace Sources.EcsBoundedContexts.Farmers.Controllers
{
    public class FarmerInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _rootGameObject;
        private readonly FarmerEntityFactory _entityFactory;

        public FarmerInitializeSystem(
            RootGameObject rootGameObject,
            FarmerEntityFactory entityFactory)
        {
            _rootGameObject = rootGameObject;
            _entityFactory = entityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            _entityFactory.Create(_rootGameObject.HouseView.Farmer);
        }
    }
}