namespace Sources.InfrastructureInterfaces.StateMachines.States
{
    public interface ILateUpdatable
    {
        void UpdateLate(float deltaTime);
    }
}