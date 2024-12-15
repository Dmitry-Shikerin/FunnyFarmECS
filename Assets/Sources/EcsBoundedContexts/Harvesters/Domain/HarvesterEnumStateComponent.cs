using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain;

namespace Sources.EcsBoundedContexts.Harvesters.Domain
{
    public struct HarvesterEnumStateComponent : IEnumStateComponent<HarvesterState>
    {
        public HarvesterState State { get; set; }
        public bool IsEntered { get; set; }
    }
}