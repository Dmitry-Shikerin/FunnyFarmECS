using Sources.InfrastructureInterfaces.StateMachines.States;

namespace Sources.InfrastructureInterfaces.StateMachines.SceneStateMachine
{
    public interface ISceneStateMachine : IUpdatable, ILateUpdatable, IFixedUpdatable, IExitable
    {
        void ChangeState(IState state, object payload = null);
    }
}