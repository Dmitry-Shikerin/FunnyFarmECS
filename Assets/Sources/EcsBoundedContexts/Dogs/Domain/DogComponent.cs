using System;
using Leopotam.EcsProto.Unity;

namespace Sources.EcsBoundedContexts.Dogs.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("Dog")]
    public struct DogComponent
    {
        public AnimalState AnimalState;
        public float Speed;
        public float IdleTimer;
    }
}