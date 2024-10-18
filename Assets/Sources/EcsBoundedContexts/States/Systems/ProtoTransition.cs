using System;
using Leopotam.EcsProto;

namespace Sources.EcsBoundedContexts.States.Systems
{
    public class ProtoTransition<TEnumState>
    {
        private readonly Func<ProtoEntity, bool> _condition;

        public ProtoTransition(TEnumState nextState, Func<ProtoEntity, bool> condition)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            NextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
        }

        public TEnumState NextState { get; }

        public bool TryMoveNext(ProtoEntity entity) =>
            _condition.Invoke(entity);
    }
}