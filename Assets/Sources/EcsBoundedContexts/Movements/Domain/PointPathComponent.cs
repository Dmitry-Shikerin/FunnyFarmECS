using System;
using Leopotam.EcsProto.Unity;
using Sources.BoundedContexts.Paths.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("MovementPoint")]
    public struct PointPathComponent
    {
        public Vector3[] Points;
        public PathOwnerType PathOwnerType;
    }
}