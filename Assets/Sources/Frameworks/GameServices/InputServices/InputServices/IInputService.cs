using Sources.Domain.Models.Inputs;

namespace Sources.InfrastructureInterfaces.Services.InputServices
{
    public interface IInputService
    {
        InputData InputData { get; }
    }
}