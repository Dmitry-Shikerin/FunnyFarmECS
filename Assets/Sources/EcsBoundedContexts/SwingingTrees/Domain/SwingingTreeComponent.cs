using System;
using Sources.MyLeoEcsProto.ComponentContainers.Attributes;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using UnityEngine;

namespace Sources.SwingingTrees.Domain
{
    [Serializable]
    public struct SwingingTreeComponent : IComponent
    {
        [CF] public Transform Tree;
        [CF] public float SpeedX;
        [CF] public float SpeedY;
        [CF] public float MaxAngleX;
        [CF] public float MaxAngleY;
        [CF] public float Direction;
    }
}