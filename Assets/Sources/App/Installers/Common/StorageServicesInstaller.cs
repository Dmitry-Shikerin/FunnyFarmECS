using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.Frameworks.GameServices.Loads.Services.Implementation;
using Sources.Frameworks.GameServices.Loads.Services.Implementation.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Prefabs.Implementation;
using Sources.Frameworks.GameServices.Prefabs.Implementation.Composites;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Repositories.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.App.Installers.Common
{
    public class StorageServicesInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<IStorageService, StorageService>();
            container.Bind<IEntityRepository, EntityRepository>();
            container.Bind<IDataService, PlayerPrefsDataService>();
            // Container.Bind<IDataService>().To<EasySaveDataService>().AsSingle();
            
            //Assets
            container.Bind<IAssetCollector, AssetCollector>();
            container.Bind<IResourcesAssetLoader, ResourcesAssetLoader>();
            container.Bind<IAddressablesAssetLoader, AddressablesAssetLoader>();
            container.Bind<ICompositeAssetService, CompositeAssetService>();
        }
    }
}