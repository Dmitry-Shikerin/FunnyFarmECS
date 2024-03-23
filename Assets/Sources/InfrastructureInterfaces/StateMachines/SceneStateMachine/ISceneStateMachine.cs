using Sources.InfrastructureInterfaces.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.SceneStateMachines
{
    public interface ISceneStateMachine : IUpdatable, ILateUpdatable, IFixedUpdatable, IExitable
    {
        void ChangeState(IState state, object payload = null);
    }
}