using System;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Sources.SwingingTrees.Infrastructure;
using UnityEngine;

namespace Sources
{
    // [DefaultExecutionOrder(-9999)]
    public class EcsGameStartUp : MonoBehaviour
    {
        private ProtoWorld _world;
        private MainAspect _aspect;
        private ProtoSystems _systems;
        private ProtoSystems _unitySystems;

        private async void Awake()
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
            _systems.Init();
        }

        private void Update()
        {
            _unitySystems?.Run();
            _systems?.Run();
        }

        private void OnDestroy()
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
