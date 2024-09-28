using System;
using Leopotam.EcsProto.Unity;

namespace Sources.EcsBoundedContexts.Animancers.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("Animancer")]
    public struct AnimancerComponent
    {
        public Animancer.AnimancerComponent Animancer;
    }
}