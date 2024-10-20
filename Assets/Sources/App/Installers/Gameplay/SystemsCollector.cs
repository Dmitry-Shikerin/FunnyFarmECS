using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.AnimalMovements.Infrastructure;
using Sources.EcsBoundedContexts.Core;

namespace Sources.App.Installers.Gameplay
{
    public class SystemsCollector
    {
        private readonly ProtoSystems _protoSystems;
        private readonly IEnumerable<IProtoSystem> _systems;
        
        public SystemsCollector(
            ProtoSystems protoSystems,
            AspectInitializeSystem aspectInitializeSystem,
            AnimalInitializeSystem animalInitializeSystem,
            AnimalRunSystem animalRunSystem,
            AnimalWalkSystem animalWalkSystem,
            AnimalChangeStateSystem animalChangeStateSystem,
            AnimalIdleSystem animalIdleSystem)
        {
            _protoSystems = protoSystems;
            _systems = new IProtoSystem[]
            {
                 aspectInitializeSystem,
                 animalInitializeSystem,
                 animalRunSystem,
                 animalWalkSystem,
                 animalChangeStateSystem,
                 animalIdleSystem
            };
        }

        public void AddSystems()
        {
            foreach (IProtoSystem system in _systems)
                _protoSystems.AddSystem(system);
        }
    }
}