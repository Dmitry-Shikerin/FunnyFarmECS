using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Huds.Presentations;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories.Controllers.Implementation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories.Domain.Implementation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories.Views.Implementation;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation.Handlers;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Interfaces.Handlers;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation.Handlers;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces.Handlers;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Views.Interfaces;
using UnityEngine;

namespace Sources.App.Installers.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [Required] [SerializeField] private RootGameObject _rootGameObject;
        [Required] [SerializeField] private GameplayHud _gameplayHud;

        public override void InstallBindings(DiContainer container)
        {
            container.Bind(_rootGameObject);
            container.Bind(_gameplayHud);
            
            container.Bind<ISceneFactory, GameplaySceneFactory>();
            container.Bind<ISceneViewFactory, GameplaySceneViewFactory>();
            
            //ModelsLoader
            container.Bind<GameplayModelsCreatorService>();
            container.Bind<GameplayModelsLoaderService>();
            
            //UiCommands
            container.Bind<IButtonCommandHandler, GameplayButtonCommandHandler>();
            container.Bind<IViewCommandHandler, GameplayViewCommandHandler>();
            
            //AchievementPopUp
            //Container.Bind<AchievementView>().FromInstance(_gameplayHud.PopUpAchievementView).AsSingle();
            //
            ////Advertising
            //Container.Bind<AdvertisingAfterWaveView>().FromInstance(_gameplayHud.AdvertisingView).AsSingle();
        }
    }
}