using System;
using Animancer;
using Leopotam.EcsProto.Unity;

namespace Sources.EcsBoundedContexts.Animancers.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("Animancer")]
    public struct AnimancerEcsComponent
    {
        public AnimancerComponent Animancer;
    }
}