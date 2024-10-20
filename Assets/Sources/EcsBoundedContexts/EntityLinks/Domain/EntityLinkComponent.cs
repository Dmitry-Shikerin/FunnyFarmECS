using System;
using Leopotam.EcsProto.Unity;

namespace Sources.EcsBoundedContexts.EntityLinks.Domain
{
    [Serializable]
    [ProtoUnityAuthoring("EntityLink")]
    public struct EntityLinkComponent
    {
        public EntityLink EntityLink;
        public int EntityId;
    }
}