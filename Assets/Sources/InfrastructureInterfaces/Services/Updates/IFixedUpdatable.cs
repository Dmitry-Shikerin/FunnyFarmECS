namespace Sources.InfrastructureInterfaces.StateMachines.States
{
    public interface IFixedUpdatable
    {
        void UpdateFixed(float fixedDeltaTime);
    }
}