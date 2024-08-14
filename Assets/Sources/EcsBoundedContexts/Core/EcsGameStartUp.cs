using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Sources
{
    public class EcsGameStartUp : MonoBehaviour
    {
        private ProtoWorld _world;
        private ProtoAspect _aspect;
        private ProtoSystems _systems;

        private void Awake()
        {
            _aspect = new ProtoAspect();
            _world = new ProtoWorld(_aspect);
            _systems = new ProtoSystems(_world);
        }

        private void Start()
        {
            _systems.AddModule(new AutoInjectModule());
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
