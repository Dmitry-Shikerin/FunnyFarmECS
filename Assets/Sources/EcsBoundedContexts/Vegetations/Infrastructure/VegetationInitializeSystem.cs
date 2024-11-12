using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Vegetations.Infrastructure.Factories;

namespace Sources.EcsBoundedContexts.Vegetations.Infrastructure
{
    public class VegetationInitializeSystem : IProtoInitSystem
    {
        private readonly VegetationEntityFactory _factory;

        public VegetationInitializeSystem(
            VegetationEntityFactory factory)
        {
            _factory = factory;
        }

        public void Init(IProtoSystems systems)
        {
            
        }
    }
}