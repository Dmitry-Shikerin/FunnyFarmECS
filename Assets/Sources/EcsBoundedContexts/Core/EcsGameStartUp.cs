using System;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using MyDependencies.Sources.Containers;
using Sources.BoundedContexts.RootGameObjects.Presentation;

namespace Sources.EcsBoundedContexts.Core
{
    public class EcsGameStartUp : IEcsGameStartUp
    {
        private readonly DiContainer _container;
        private readonly RootGameObject _rootGameObject;
        private readonly ProtoSystems _systems;
        private readonly ProtoWorld _world;
        private readonly MainAspect _aspect;
        private readonly SystemsCollector _systemsCollector;
        private ProtoSystems _unitySystems;
        private bool _isInitialize;

        public EcsGameStartUp(
            DiContainer container, 
            RootGameObject rootGameObject,
            ProtoWorld protoWorld,
            ProtoSystems systems,
            MainAspect aspect,
            SystemsCollector systemsCollector)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
            _world = protoWorld ?? throw new ArgumentNullException(nameof(protoWorld));
            _systems = systems ?? throw new ArgumentNullException(nameof(systems));
            _aspect = aspect ?? throw new ArgumentNullException(nameof(aspect));
            _systemsCollector = systemsCollector ?? throw new ArgumentNullException(nameof(systemsCollector));
        }

        public async void Initialize()
        {
            InitUnitySystems();
            await UniTask.Yield();
            AddModules();
            _systemsCollector.AddSystems();
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

        private void AddModules()
        {
            _systems.AddModule(new AutoInjectModule());
        }

        private void AddOneFrame()
        {
            //_systems.DelHere<JumpEvent>();
        }
        
        private async void Init()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _isInitialize = true;
        }

        private void InitUnitySystems()
        {
            _unitySystems = new ProtoSystems(_world);
            _unitySystems
                .AddModule(new AutoInjectModule())
                .AddModule(new UnityModule())
                .Init();
            //TODO закоменитл
            // _rootGameObject
            //     .GetComponentsInChildren<ProtoUnityAuthoring>()
            //     .ForEach(authoring => authoring.ProcessAuthoring());
        }
    }
}
