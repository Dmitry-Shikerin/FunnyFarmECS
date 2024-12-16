using System;
using Leopotam.EcsProto.Unity;
using UnityEngine.AI;

namespace Sources.EcsBoundedContexts.NavMeshes.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("NavMeshAgent")]
    public struct NavMeshComponent
    {
        public NavMeshAgent Value;
    }
}