using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Huds.Presentations;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories.Controllers.Implementation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories.Domain.Implementation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories.Views.Implementation;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation.Handlers;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Interfaces.Handlers;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation.Handlers;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Interfaces.Handlers;
using Sources.Frameworks.GameServices.DailyRewards.Infrastructure.Factories;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Views.Interfaces;
using UnityEngine;

namespace Sources.App.Installers.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            //TODO покашто так
            MainMenuHud hud = Instantiate(Resources.Load<MainMenuHud>("Ui/MainMenuHud"));
            container.Bind(hud);
            container.Bind<ISceneViewFactory, MainMenuSceneViewFactory>();
            container.Bind<ISceneFactory, MainMenuSceneFactory>();

            //ModelsLoader
            container.Bind<MainMenuModelsCreatorService>();
            container.Bind<MainMenuModelsLoaderService>();

            //UiCommands
            container.Bind<IButtonCommandHandler, MainMenuButtonCommandHandler>();
            container.Bind<IViewCommandHandler, MainMenuViewCommandHandler>();

            //daily
            container.Bind<DailyRewardViewFactory>();

            //achievements
            //Container.Bind<AchievementView>().FromInstance(_mainMenuHud.EmptyAchievementView).AsSingle();
        }
    }
}