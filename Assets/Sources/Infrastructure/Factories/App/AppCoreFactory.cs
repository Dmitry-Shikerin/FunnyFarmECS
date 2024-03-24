using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.App;
using Sources.ControllerInterfaces.Scenes;
using Sources.Infrastructure.Factories.Controllers.Scenes;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Factories.Controllers.Scenes;
using Sources.InfrastructureInterfaces.Services.SceneService;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.App
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();

            ProjectContext projectContext = Object.FindObjectOfType<ProjectContext>();
            
            Dictionary<string, Func<object, SceneContext, UniTask<IScene>>> sceneFactories =
                new Dictionary<string, Func<object, SceneContext, UniTask<IScene>>>();
            
            SceneService sceneService = new SceneService(sceneFactories);
            projectContext.Container.Bind<ISceneService>().To<SceneService>().FromInstance(sceneService);

            sceneFactories["MainMenu"] = (payload, context) =>
                context.Container.Resolve<MainMenuSceneFactory>().Create(payload);
            sceneFactories["Gameplay"] = (payload, context) =>
                context.Container.Resolve<GameplaySceneFactory>().Create(payload);
            
            appCore.Construct(sceneService);
            
            return appCore;
        }
    }
}