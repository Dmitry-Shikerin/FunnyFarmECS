using System;
using Leopotam.EcsProto.Unity;
using UnityEngine;

namespace Sources.EcsBoundedContexts.GameObjects
{
    [Serializable] 
    [ProtoUnityAuthoring("GameObject")]
    public struct GameObjectComponent
    {
        public GameObject GameObject;
    }
}