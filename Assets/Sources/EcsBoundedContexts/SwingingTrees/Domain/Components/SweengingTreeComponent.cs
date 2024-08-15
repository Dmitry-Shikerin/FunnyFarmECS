using System;
using Leopotam.EcsProto.Unity;
using Sources.MyLeoEcsProto.ComponentContainers.Attributes;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using UnityEngine;

namespace Sources.SwingingTrees.Domain
{
    [Serializable] [ProtoUnityAuthoring("SwingingTry")]
    public struct SweengingTreeComponent : IComponent
    {
        public Transform Tree;
        public float SpeedX;
        public float SpeedY;
        public float MaxAngleX;
        public float MaxAngleY;
        public float Direction;
    }
}