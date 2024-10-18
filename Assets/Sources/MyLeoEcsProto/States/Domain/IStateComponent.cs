using System;

namespace Sources.MyLeoEcsProto.States.Domain
{
    public interface IStateComponent<TEnumState>
        where TEnumState : Enum
    {
        TEnumState CurrentState { get; set; }
        bool IsEntered { get; set; }
    }
}