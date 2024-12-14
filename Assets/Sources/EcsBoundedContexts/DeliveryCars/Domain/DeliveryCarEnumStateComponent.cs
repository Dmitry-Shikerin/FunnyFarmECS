using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain;

namespace Sources.EcsBoundedContexts.DeliveryCars.Domain
{
    public struct DeliveryCarEnumStateComponent : IEnumStateComponent<DeliveryCarState>
    {
        public float Timer;
        
        public DeliveryCarState State { get; set; }
        public bool IsEntered { get; set; }
    }
}