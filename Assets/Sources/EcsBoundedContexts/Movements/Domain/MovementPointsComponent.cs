using System;
using Leopotam.EcsProto.Unity;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("MovementPoints")]
    public struct MovementPointsComponent
    {
        public Vector3 TargetPoint;
    }
}