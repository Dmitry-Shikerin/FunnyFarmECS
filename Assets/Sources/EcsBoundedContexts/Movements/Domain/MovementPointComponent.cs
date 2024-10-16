using System;
using System.Collections.Generic;
using Leopotam.EcsProto.Unity;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("MovementPoint")]
    public struct MovementPointComponent
    {
        public Vector3 TargetPoint;
    }
}