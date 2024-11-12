using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain;

namespace Sources.EcsBoundedContexts.Vegetations.Domain
{
    public struct VegetationEnumStateComponent : IEnumStateComponent<VegetationState>
    {
        public float Timer { get; set; }
        
        public VegetationState State { get; set; }
        public bool IsEntered { get; set; }
    }
}