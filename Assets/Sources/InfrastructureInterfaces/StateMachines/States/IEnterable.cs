namespace Sources.InfrastructureInterfaces.StateMachines.States
{
    public interface IEnterable
    {
        void Enter(object payload = null);
    }
}