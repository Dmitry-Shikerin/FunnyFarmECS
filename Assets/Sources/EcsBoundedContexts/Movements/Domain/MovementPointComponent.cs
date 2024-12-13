using System;
using Leopotam.EcsProto.Unity;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("MovementPoint")]
    public struct MovementPointComponent
    {
        public Vector3 TargetPoint;
        public Vector3[] Points;
    }
}