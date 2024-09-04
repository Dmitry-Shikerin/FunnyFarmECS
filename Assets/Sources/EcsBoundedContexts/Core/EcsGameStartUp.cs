using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Sirenix.Utilities;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.SwingingTrees.Infrastructure;

namespace Sources.EcsBoundedContexts.Core
{
    public class EcsGameStartUp : IEcsGameStartUp
    {
        private readonly RootGameObject _rootGameObject;
        private ProtoWorld _world;
        private MainAspect _aspect;
        private ProtoSystems _systems;
        private ProtoSystems _unitySystems;

        public EcsGameStartUp(RootGameObject rootGameObject)
        {
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
            await UniTask.Yield();
            _systems = new ProtoSystems(_world);
            _systems.AddModule(new AutoInjectModule());
            AddInit();
            AddRun();
            AddOneFrame();
            _rootGameObject
                .GetComponentsInChildren<ProtoUnityAuthoring>()
                .ForEach(authoring => authoring.ProcessAuthoring());
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
                .AddSystem(new TreeSwingerSystem());
            
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
