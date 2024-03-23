namespace Sources.InfrastructureInterfaces.StateMachines.States
{
    public interface IState : IEnterable, IExitable, IUpdatable, ILateUpdatable, IFixedUpdatable
    {
    }
}