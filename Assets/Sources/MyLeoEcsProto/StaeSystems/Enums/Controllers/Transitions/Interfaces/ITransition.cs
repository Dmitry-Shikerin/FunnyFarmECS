using System;
using Leopotam.EcsProto;

namespace Sources.MyLeoEcsProto.States.Controllers.Transitions.Interfaces
{
    public interface ITransition<out TEnumState>
        where TEnumState : Enum
    {
        TEnumState NextState { get; }
        bool CanTransit(ProtoEntity entity);
    }
}