using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.States.Domain;

namespace Sources.EcsBoundedContexts.States.Systems
{
    public abstract class StateSystem<TEnumState, TComponent> : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
        where TEnumState : Enum
        where TComponent : struct, IProtoState<TEnumState>    
    {
        private readonly ProtoIt _protoIt;
        private readonly ProtoPool<TComponent> _pool;
        private readonly List<ProtoTransition<TEnumState>> _transitions = new ();

        protected StateSystem(ProtoIt protoIt, ProtoPool<TComponent> pool)
        {
            _protoIt = protoIt ?? throw new ArgumentNullException(nameof(protoIt));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }

        public virtual void Init(IProtoSystems systems)
        {
        }

        protected abstract bool IsState(ProtoEntity entity);
        
        protected virtual void Enter(ProtoEntity entity)
        {
        }

        protected abstract void Update(ProtoEntity entity);

        protected virtual void Exit(ProtoEntity entity)
        {
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _protoIt)
            {
                ref TComponent state = ref _pool.Get(entity);

                if (IsState(entity) == false)
                    continue;
                
                if (state.IsEntered == false)
                {
                    Enter(entity);
                    state.IsEntered = true;
                }
                
                Update(entity);
                
                if (TryChangeState(entity, out TEnumState nextState) == false)
                    return;
                
                Exit(entity);
                state.CurrentState = nextState;
            }
        }

        protected void AddTransition(ProtoTransition<TEnumState> transition) =>
            _transitions.Add(transition);

        private bool TryChangeState(ProtoEntity entity, out TEnumState nextState)
        {
            foreach (ProtoTransition<TEnumState> transition in _transitions)
            {
                if (transition.TryMoveNext(entity) == false)
                    continue;
                
                nextState = transition.NextState;
                return true;
            }

            nextState = default;
            
            return false;
        }

        public virtual void Destroy()
        {
        }
    }
}