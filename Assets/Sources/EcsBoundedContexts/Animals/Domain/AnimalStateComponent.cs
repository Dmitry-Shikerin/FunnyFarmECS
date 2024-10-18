using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.EcsBoundedContexts.States.Domain;
using UnityEngine.Serialization;

namespace Sources.EcsBoundedContexts.Animals.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("AnimalState")]
    public struct AnimalStateComponent : IProtoState<AnimalState>
    {
        public AnimalState CurrentState { get; set; }
        public float CurentIdleTime;
        public float TargetIdleTime;
        public bool IsEntered { get; set; }
        public bool IsExited { get; set; }
    }
}