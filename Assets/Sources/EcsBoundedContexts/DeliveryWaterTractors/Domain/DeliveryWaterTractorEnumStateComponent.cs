using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain
{
    public struct DeliveryWaterTractorEnumStateComponent : IEnumStateComponent<DeliveryWaterTractorState>
    {
        public float Timer;
        
        public DeliveryWaterTractorState State { get; set; }
        public bool IsEntered { get; set; }
    }
}