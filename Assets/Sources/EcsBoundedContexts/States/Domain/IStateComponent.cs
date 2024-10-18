using System;

namespace Sources.EcsBoundedContexts.States.Domain
{
    public interface IStateComponent<TEnumState>
        where TEnumState : Enum
    {
        TEnumState CurrentState { get; set; }
        bool IsEntered { get; set; }
        bool IsExited { get; set; }
    }
}