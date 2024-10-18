using System;
using Leopotam.EcsProto;

namespace Sources.EcsBoundedContexts.States.Systems
{
    public interface ITransition<out TEnumState>
        where TEnumState : Enum
    {
        TEnumState NextState { get; }
        bool CanTransit(ProtoEntity entity);
    }
}