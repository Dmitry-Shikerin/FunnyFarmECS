using System;
using Leopotam.EcsProto;
using Sources.MyLeoEcsProto.States.Controllers.Transitions.Interfaces;

namespace Sources.MyLeoEcsProto.States.Controllers.Transitions.Implementation
{
    public readonly struct MutableStateTransition<TEnumState> : ITransition<TEnumState>
        where TEnumState : Enum
    {
        private readonly Func<TEnumState> _nextState;
        private readonly Func<ProtoEntity, bool> _condition;

        public MutableStateTransition(Func<TEnumState> nextState, Func<ProtoEntity, bool> condition)
        {
            _nextState = nextState;
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public TEnumState NextState => _nextState.Invoke();

        public bool CanTransit(ProtoEntity entity) =>
            _condition.Invoke(entity);
    }
}