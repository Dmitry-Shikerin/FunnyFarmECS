using System;
using Leopotam.EcsProto.Unity;
using UnityEngine.Serialization;

namespace Sources.EcsBoundedContexts.Movements.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("MoveSpeed")]
    public struct MoveSpeedComponent
    {
        public float MoveSpeed;
        public float RotationSpeed;
    }
}