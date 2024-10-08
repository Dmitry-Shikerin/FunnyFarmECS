using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Sirenix.Utilities;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Dogs.Infrastructure;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure;
using Zenject;

namespace Sources.EcsBoundedContexts.Core
{
    public class EcsGameStartUp : IEcsGameStartUp
    {
        private readonly DiContainer _container;
        private readonly RootGameObject _rootGameObject;
        private ProtoWorld _world;
        private MainAspect _aspect;
        private ProtoSystems _systems;
        private ProtoSystems _unitySystems;

        public EcsGameStartUp(
            DiContainer container, 
            RootGameObject rootGameObject)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
        }

        public async void Initialize()
        {
            _aspect = new MainAspect();
            _world = new ProtoWorld(_aspect);
            _unitySystems = new ProtoSystems(_world);
            _unitySystems
                .AddModule(new AutoInjectModule())
                .AddModule(new UnityModule())
                .Init();
            _rootGameObject
                .GetComponentsInChildren<ProtoUnityAuthoring>()
                .ForEach(authoring => authoring.ProcessAuthoring());
            await UniTask.Yield();
            _systems = new ProtoSystems(_world);
            _systems.AddModule(new AutoInjectModule());
            AddInit();
            AddRun();
            AddOneFrame();
            _systems.Init();
        }

        public void Update(float deltaTime)
        {
            _unitySystems?.Run();
            _systems?.Run();
        }

        public void Destroy()
        {
            _systems?.Destroy();
            _unitySystems?.Destroy();
        }

        private IProtoSystems AddInit()
        {
            _systems
                .AddSystem(new TreeSwingerSystem())
                .AddSystem(new DogMovementSystem(_container));
            
            return _systems;
        }

        private IProtoSystems AddRun()
        {
            return _systems;
        }

        private IProtoSystems AddOneFrame()
        {
            _systems.DelHere<JumpEvent>();
            return _systems;
        }
    }
}
