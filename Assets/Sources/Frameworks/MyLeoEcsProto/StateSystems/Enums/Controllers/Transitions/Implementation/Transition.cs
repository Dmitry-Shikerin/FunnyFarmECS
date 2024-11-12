using System;
using Leopotam.EcsProto;
using Sources.MyLeoEcsProto.States.Controllers.Transitions.Interfaces;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation
{
    public readonly struct Transition<TEnumState> : ITransition<TEnumState>
        where TEnumState : Enum
    {
        private readonly Func<ProtoEntity, bool> _condition;

        public Transition(TEnumState nextState, Func<ProtoEntity, bool> condition)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            NextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
        }

        public TEnumState NextState { get; }

        public bool CanTransit(ProtoEntity entity) =>
            _condition.Invoke(entity);
    }
}