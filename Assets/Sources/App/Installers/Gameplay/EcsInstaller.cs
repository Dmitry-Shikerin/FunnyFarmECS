using Leopotam.EcsProto;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.EcsBoundedContexts.AnimalMovements.Infrastructure;
using Sources.EcsBoundedContexts.Animals.Infrastructure;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure;

namespace Sources.App.Installers.Gameplay
{
    public class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<IEcsGameStartUp, EcsGameStartUp>();
            MainAspect aspect = new MainAspect();
            ProtoWorld world = new ProtoWorld(aspect);
            ProtoSystems systems = new ProtoSystems(world);
            container.Bind(world);
            container.Bind(aspect);
            container.Bind(systems);
            container.Bind<SystemsCollector>();
            
            //Animals
            container.Bind<AnimalEntityFactory>();
            
            container.Bind<AnimalInitializeSystem>();
            container.Bind<AnimalRunSystem>();
            container.Bind<AnimalWalkSystem>();
            container.Bind<AnimalChangeEnumStateSystem>();
            container.Bind<AnimalIdleSystem>();

            //Trees
            container.Bind<TreeSwingerSystem>();
        }
    }
}