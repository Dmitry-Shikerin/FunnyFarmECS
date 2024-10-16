using System;
using Leopotam.EcsProto.Unity;

namespace Sources.EcsBoundedContexts.Movements.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("MoveSpeed")]
    public struct MoveSpeedComponent
    {
        public float Current;
        public float Target;
    }
}