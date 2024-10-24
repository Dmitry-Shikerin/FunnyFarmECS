﻿using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.AnimalMovements.Infrastructure;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Systems;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers;

namespace Sources.EcsBoundedContexts.Core
{
    public class SystemsCollector
    {
        private readonly ProtoSystems _protoSystems;
        private readonly IEnumerable<IProtoSystem> _systems;
        
        public SystemsCollector(
            ProtoSystems protoSystems,
            AnimalInitializeSystem animalInitializeSystem,
            AnimalRunSystem animalRunSystem,
            AnimalWalkSystem animalWalkSystem,
            AnimalChangeEnumStateSystem animalChangeEnumStateSystem,
            AnimalIdleSystem animalIdleSystem,
            TreeSwingInitSystem treeSwingInitSystem,
            TreeSwingerSystem treeSwingerSystem,
            TestEventSystem testEventSystem)
        {
            _protoSystems = protoSystems;
            _systems = new IProtoSystem[]
            {
                 animalInitializeSystem,
                 animalRunSystem,
                 animalWalkSystem,
                 animalChangeEnumStateSystem,
                 animalIdleSystem,
                 treeSwingInitSystem,
                 treeSwingerSystem,
                 testEventSystem,
            };
        }

        public void AddSystems()
        {
            foreach (IProtoSystem system in _systems)
                _protoSystems.AddSystem(system);
        }
    }
}