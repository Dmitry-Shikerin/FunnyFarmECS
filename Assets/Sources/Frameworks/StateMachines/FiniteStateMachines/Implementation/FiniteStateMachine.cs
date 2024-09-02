﻿using Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation.States;

namespace Sources.Frameworks.StateMachines.FiniteStateMachines.Implementation
{
    public class FiniteStateMachine
    {
        private FiniteState _currentState;

        protected void Start(FiniteState startState) =>
            MoveNextState(startState);

        protected void Stop() =>
            _currentState.Exit();

        protected void Update(float deltaTime)
        {
            _currentState.Update(deltaTime);
            
            if (_currentState.TryGetNextState(out FiniteState nextState) == false)
                return;
            
            MoveNextState(nextState);
        }
        
        private void MoveNextState(FiniteState nextState)
        {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }
    }
}