using System;
using Leopotam.EcsProto.Unity;
using Sources.MyLeoEcsProto.ComponentContainers.Attributes;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using UnityEngine;

namespace Sources.Transforms
{
    [Serializable] 
    [ProtoUnityAuthoring("Transform")]
    public struct TransformComponent
    {
        public Transform Transform;
    }
}