﻿using Sirenix.OdinInspector;
using Sources.BoundedContexts.AdvertisingAfterWaves.Presentation;
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
using Sources.Frameworks.MyGameCreator.Achievements.Presentation;
using UnityEngine;
using Zenject;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameplayZenjectInstaller : MonoInstaller
    {
        [Required] [SerializeField] private RootGameObject _rootGameObject;
        [Required] [SerializeField] private GameplayHud _gameplayHud;
        
        public override void InstallBindings()
        {
            Container.Bind<RootGameObject>().FromInstance(_rootGameObject);
            Container.Bind<GameplayHud>().FromInstance(_gameplayHud);
            
            Container.Bind<ISceneFactory>().To<GameplaySceneFactory>().AsSingle();
            Container.Bind<ISceneViewFactory>().To<GameplaySceneViewFactory>().AsSingle();
            
            //ModelsLoader
            Container.Bind<GameplayModelsCreatorService>().AsSingle();
            Container.Bind<GameplayModelsLoaderService>().AsSingle();
            
            //ECS
            Container.Bind<IEcsGameStartUp>().To<EcsGameStartUp>().AsSingle();
            
            //UiCommands
            Container.Bind<IButtonCommandHandler>().To<GameplayButtonCommandHandler>().AsSingle();
            Container.Bind<IViewCommandHandler>().To<GameplayViewCommandHandler>().AsSingle();
            
            //AchievementPopUp
            //Container.Bind<AchievementView>().FromInstance(_gameplayHud.PopUpAchievementView).AsSingle();
            //
            ////Advertising
            //Container.Bind<AdvertisingAfterWaveView>().FromInstance(_gameplayHud.AdvertisingView).AsSingle();
        }
    }
}