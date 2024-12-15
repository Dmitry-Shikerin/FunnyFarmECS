using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain;

namespace Sources.EcsBoundedContexts.WaterTractors.Domain
{
    public struct WaterTractorEnumStateComponent : IEnumStateComponent<WaterTractorState>
    {
        public WaterTractorState State { get; set; }
        public bool IsEntered { get; set; }
    }
}