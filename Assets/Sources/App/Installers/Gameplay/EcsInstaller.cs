using System.Timers;
using Leopotam.EcsProto;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.EcsBoundedContexts.AnimalMovements.Infrastructure;
using Sources.EcsBoundedContexts.Animals.Infrastructure;
using Sources.EcsBoundedContexts.Animals.Infrastructure.Features;
using Sources.EcsBoundedContexts.Commons;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryCars.Infrastructure;
using Sources.EcsBoundedContexts.DeliveryCars.Infrastructure.Factories;
using Sources.EcsBoundedContexts.DeliveryCars.Infrastructure.Features;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Infrastructure;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Infrastructure.Factories;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Infrastructure.Features;
using Sources.EcsBoundedContexts.Farmers.Infrastructure;
using Sources.EcsBoundedContexts.Farmers.Infrastructure.Factories;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Factories;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Systems;
using Sources.EcsBoundedContexts.Timers.Infrastructure;
using Sources.EcsBoundedContexts.Vegetations.Infrastructure;
using Sources.EcsBoundedContexts.Vegetations.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Vegetations.Infrastructure.Features;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Features;

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
            container.Bind<IEventBuffer, EventBuffer>();
            container.Bind<SystemsCollector>();
            container.Bind<IFeatureService, FeatureService>();
            
            //Common
            container.Bind<CommonFeature>();
            
            container.Bind<TimerSystem>();
            
            //Animals
            container.Bind<AnimalEntityFactory>();
            container.Bind<AnimalFeature>();
            
            container.Bind<AnimalInitializeSystem>();
            container.Bind<AnimalRunSystem>();
            container.Bind<AnimalWalkSystem>();
            container.Bind<AnimalChangeEnumStateSystem>();
            container.Bind<AnimalIdleSystem>();

            //Trees
            container.Bind<TreeSwingEntityFactory>();
            
            container.Bind<TreeSwingInitSystem>();
            container.Bind<TreeSwingerSystem>();
            
            //Vegetations
            container.Bind<VegetationEntityFactory>();
            container.Bind<VegetationFeature>();
            
            container.Bind<ChangeVegetationStateSystem>();
            container.Bind<VegetationInitializeSystem>();
            container.Bind<VegetationIdleSystem>();
            container.Bind<VegetationGrowSystem>();
            
            //Farmers
            container.Bind<FarmerEntityFactory>();
            container.Bind<FarmerFeature>();
            
            container.Bind<FarmerInitializeSystem>();
            container.Bind<FarmerIdleSystem>();
            container.Bind<FarmerWorkSystem>();
            container.Bind<FarmerMoveSystem>();
            
            //DeliveryCars
            container.Bind<DeliveryCarFeature>();
            container.Bind<DeliveryCarEntityFactory>();
            
            container.Bind<DeliveryCarInitializeSystem>();
            container.Bind<DeliveryCarMoveToHomeSystem>();
            container.Bind<DeliveryCarExitIdleSystem>();
            container.Bind<DeliveryCarMoveToExitSystem>();
            container.Bind<DeliveryCarHomeIdleSystem>();
            
            //DeliveryWaterTractor
            container.Bind<DeliveryWaterTractorEntityFactory>();
            container.Bind<DeliveryWaterTractorFeature>();
            
            container.Bind<DeliveryWaterTractorInitializeSystem>();
            container.Bind<DeliveryWaterTractorHomeSystem>();
            container.Bind<DeliveryWaterTractorPondSystem>();
            container.Bind<DeliveryWaterTractorMoveToHomeSystem>();
            container.Bind<DeliveryWaterTractorMoveToPondSystem>();
        }
    }
}