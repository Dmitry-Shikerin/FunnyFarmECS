using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.MyLeoEcsProto.States.Domain;

namespace Sources.EcsBoundedContexts.Animals.Domain
{
    [Serializable]
    [ProtoUnityAuthoring("AnimalState")]
    public struct AnimalEnumStateComponent : IEnumStateComponent<AnimalState>
    {
        public float CurentIdleTime;
        public float TargetIdleTime;
        
        public AnimalState CurrentState { get; set; }
        public bool IsEntered { get; set; }
    }
}