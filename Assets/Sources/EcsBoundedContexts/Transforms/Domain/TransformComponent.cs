using System;
using Sources.MyLeoEcsProto.ComponentContainers.Attributes;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using UnityEngine;

namespace Sources.Transforms
{
    [Serializable]
    public struct TransformComponent : IComponent
    {
        [CF] public Transform Transform;
    }
}