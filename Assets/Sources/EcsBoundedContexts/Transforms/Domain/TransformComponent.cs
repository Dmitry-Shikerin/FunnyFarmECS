using System;
using Leopotam.EcsProto.Unity;
using Sources.MyLeoEcsProto.ComponentContainers.Attributes;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using UnityEngine;

namespace Sources.Transforms
{
    [Serializable] [ProtoUnityAuthoring]
    public struct TransformComponent : IComponent
    {
        [CF] public Transform Transform;
    }
}