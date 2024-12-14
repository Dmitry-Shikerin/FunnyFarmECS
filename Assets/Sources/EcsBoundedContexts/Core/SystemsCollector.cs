using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Animals.Infrastructure.Features;
using Sources.EcsBoundedContexts.DeliveryCars.Infrastructure.Features;
using Sources.EcsBoundedContexts.Farmers.Infrastructure;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Systems;
using Sources.EcsBoundedContexts.Vegetations.Infrastructure.Features;

namespace Sources.EcsBoundedContexts.Core
{
    public class SystemsCollector
    {
        private readonly ProtoSystems _protoSystems;
        private readonly IEnumerable<IProtoSystem> _systems;
        
        public SystemsCollector(
            ProtoSystems protoSystems,
            AnimalFeature animalFeature,
            VegetationFeature vegetationFeature,
            TreeSwingInitSystem treeSwingInitSystem,
            TreeSwingerSystem treeSwingerSystem,
            FarmerFeature farmerFeature,
            DeliveryCarFeature deliveryCarFeature)
        {
            _protoSystems = protoSystems;
            _systems = new IProtoSystem[]
            {
                 animalFeature,
                 vegetationFeature,
                 treeSwingInitSystem,
                 treeSwingerSystem,
                 farmerFeature,
                 deliveryCarFeature,
            };
        }

        public void AddSystems()
        {
            foreach (IProtoSystem system in _systems)
                _protoSystems.AddSystem(system);
        }
    }
}