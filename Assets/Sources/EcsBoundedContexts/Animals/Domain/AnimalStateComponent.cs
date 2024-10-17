using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Dogs.Domain;

namespace Sources.EcsBoundedContexts.Animals.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("AnimalState")]
    public struct AnimalStateComponent
    {
        public AnimalState AnimalState;
        public float CurentIdleTime;
        public float TargetIdleTime;
    }
}