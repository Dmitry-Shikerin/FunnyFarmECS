using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.AdvertisingAfterWaves.Infrastructure.Services;
using Sources.BoundedContexts.SaveAfterWaves.Infrastructure.Services;
using Sources.BoundedContexts.Tutorials.Services.Implementation;
using Sources.BoundedContexts.Tutorials.Services.Interfaces;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Implementation;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Cameras.Presentation.Implementation;
using Sources.Frameworks.GameServices.InputServices;
using Sources.Frameworks.GameServices.Linecasts.Implementation;
using Sources.Frameworks.GameServices.Linecasts.Interfaces;
using Sources.Frameworks.GameServices.ObjectPools.Implementation.Managers;
using Sources.Frameworks.GameServices.ObjectPools.Interfaces.Managers;
using Sources.Frameworks.GameServices.Overlaps.Implementation;
using Sources.Frameworks.GameServices.Overlaps.Interfaces;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Implementation;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Implementation;
using Sources.Frameworks.MyGameCreator.SkyAndWeathers.Infrastructure.Services.Implementation;
using Sources.Frameworks.MyGameCreator.SkyAndWeathers.Presentation;
using UnityEngine;

namespace Sources.App.Installers.Gameplay
{
    public class GameServicesIntaller : MonoInstaller
    {
        [Required] [SerializeField] private CameraView _cameraView;
        [Required] [SerializeField] private SkyAndWeatherView _skyAndWeatherView;
        
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ITutorialService, TutorialService>();
            container.Bind<IOverlapService, OverlapService>();
            container.Bind<ILinecastService, LinecastService>();
            container.Bind<IPoolManager, PoolManager>();
            container.Bind<ISignalBus, StreamSignalBus>();
            container.BindInterfaces<NewInputService>();
            container.Bind<AdvertisingAfterWaveService>();
            container.Bind<SaveAfterWaveService>();
            container.BindInterfaces<UpdateService>();
            
            //Camera
            container.Bind(_cameraView);
            container.Bind<ICameraService, CameraService>();
            
            //SkyAndWeather
            container.Bind(_skyAndWeatherView);
            container.Bind<ISkyAndWeatherService, SkyAndWeatherService>();
        }
    }
}