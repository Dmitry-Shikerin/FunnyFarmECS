using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Vegetations.Infrastructure.Factories;

namespace Sources.EcsBoundedContexts.Vegetations.Infrastructure
{
    public class VegetationInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _rootGameObject;
        private readonly VegetationEntityFactory _factory;

        public VegetationInitializeSystem(
            RootGameObject rootGameObject,
            VegetationEntityFactory factory)
        {
            _rootGameObject = rootGameObject;
            _factory = factory;
        }

        public void Init(IProtoSystems systems)
        {
            _rootGameObject.CabbagePatchView.Cabbages.ForEach(cabbage => _factory.Create(cabbage));
            _rootGameObject.OnionPatchView.Onions.ForEach(onion => _factory.Create(onion));
            _rootGameObject.TomatoPatchView.Pumpkins.ForEach(tomato => _factory.Create(tomato));
            _rootGameObject.PumpkinPatchView.Pumpkins.ForEach(pumpkin => _factory.Create(pumpkin));
        }
    }
}