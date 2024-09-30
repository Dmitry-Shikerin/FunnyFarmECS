using System;
using Leopotam.EcsProto.Unity;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.SwingingTrees.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("SwingingTree")]
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