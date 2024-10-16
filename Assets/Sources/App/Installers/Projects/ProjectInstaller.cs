using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.Frameworks.GameServices.SceneLoaderServices.Implementation;
using Sources.Frameworks.GameServices.Volumes.Infrastucture.Factories;
using Sources.Frameworks.YandexSdkFramework.Focuses.Implementation;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.InfrastructureInterfaces.Services.SceneLoaderService;

namespace Sources.App.Installers.Projects
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ISceneLoaderService, SceneLoaderService>();
            // Container.Bind<ISceneLoaderService>().To<AddressableSceneLoaderService>().AsSingle();

            container.Bind<VolumeViewFactory>();
        }
    }
}