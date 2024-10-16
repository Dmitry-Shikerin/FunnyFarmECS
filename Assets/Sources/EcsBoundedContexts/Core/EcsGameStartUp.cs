using System;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using MyDependencies.Sources.Containers;
using Sirenix.Utilities;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.AnimalMovements.Infrastructure;
using Sources.EcsBoundedContexts.Dogs.Infrastructure;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure;

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
        private bool _isInitialize;

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
            Init();
        }

        public void Update(float deltaTime)
        {
            if (_isInitialize == false)
                return;
            
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
                .AddSystem(new AnimalMovementSystem(_container))
                .AddSystem(new AnimalChangeStateSystem(_container))
                ;
            
            return _systems;
        }

        private IProtoSystems AddRun()
        {
            return _systems
                .AddSystem(new AnimalInitializeSystem(_container));
        }

        private IProtoSystems AddOneFrame()
        {
            _systems.DelHere<JumpEvent>();
            return _systems;
        }

        //TODO придумать чтото получше 
        private async void Init()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _isInitialize = true;
        }
    }
}
