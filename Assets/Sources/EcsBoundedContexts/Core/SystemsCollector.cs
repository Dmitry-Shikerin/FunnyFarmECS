using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Animals.Infrastructure.Features;
using Sources.EcsBoundedContexts.Commons;
using Sources.EcsBoundedContexts.DeliveryCars.Infrastructure.Features;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Infrastructure.Features;
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
            CommonFeature commonFeature,
            AnimalFeature animalFeature,
            VegetationFeature vegetationFeature,
            TreeSwingInitSystem treeSwingInitSystem,
            TreeSwingerSystem treeSwingerSystem,
            FarmerFeature farmerFeature,
            DeliveryCarFeature deliveryCarFeature,
            DeliveryWaterTractorFeature deliveryWaterTractorFeature)
        {
            _protoSystems = protoSystems;
            _systems = new IProtoSystem[]
            {
                commonFeature,
                 animalFeature,
                 vegetationFeature,
                 treeSwingInitSystem,
                 treeSwingerSystem,
                 farmerFeature,
                 deliveryCarFeature,
                 deliveryWaterTractorFeature,
            };
        }

        public void AddSystems()
        {
            foreach (IProtoSystem system in _systems)
                _protoSystems.AddSystem(system);
        }
    }
}