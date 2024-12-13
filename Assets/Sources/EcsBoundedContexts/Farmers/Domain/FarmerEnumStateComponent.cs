using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain;

namespace Sources.EcsBoundedContexts.Farmers.Domain
{
    public struct FarmerEnumStateComponent : IEnumStateComponent<FarmerState>
    {
        public float Timer;
        
        public FarmerState State { get; set; }
        public bool IsEntered { get; set; }
    }
}