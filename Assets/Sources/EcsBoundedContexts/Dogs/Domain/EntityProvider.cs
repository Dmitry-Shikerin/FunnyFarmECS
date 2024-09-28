using System;
using Leopotam.EcsProto;
using Sources.Frameworks.StateMachines.ContextStateMachines.Interfaces.Contexts;

namespace Sources.EcsBoundedContexts.Dogs.Domain
{
    public struct EntityProvider : IContext, IDisposable
    {
        public EntityProvider(ProtoEntity entity)
        {
            Entity = entity;
        }
        
        public ProtoEntity Entity { get; }

        public void Dispose()
        {
        }
    }
}