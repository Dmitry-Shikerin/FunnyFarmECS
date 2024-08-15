using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Sources.SwingingTrees.Infrastructure;
using UnityEngine;

namespace Sources
{
    public class EcsGameStartUp : MonoBehaviour
    {
        private ProtoWorld _world;
        private MainAspect _aspect;
        private ProtoSystems _systems;

        private void Awake()
        {
            _aspect = new MainAspect();
            _world = new ProtoWorld(_aspect);
            _systems = new ProtoSystems(_world);
        }

        private void Start()
        {
            _systems
                .AddModule(new AutoInjectModule())
                .AddModule(new UnityModule());
            AddInit();
            AddRun();
            AddOneFrame();
            
            _systems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
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
