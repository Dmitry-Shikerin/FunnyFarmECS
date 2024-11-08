using Sources.MyLeoEcsProto.States.Domain;

namespace Sources.EcsBoundedContexts.Farmers.Domain
{
    public struct FarmerEnumStateComponent : IEnumStateComponent<FarmerState>
    {
        public FarmerState CurrentState { get; set; }
        public bool IsEntered { get; set; }
    }
}