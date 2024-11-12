using Sources.Frameworks.MyLeoEcsProto.CommandBuffers;

namespace Sources.EcsBoundedContexts.Vegetations.Domain.Events
{
    public struct ChangeVegetationStateEvent : IEvent
    {
        public ChangeVegetationStateEvent(VegetationState state, VegetationType type)
        {
            State = state;
            Type = type;
        }

        public VegetationState State { get; }
        public VegetationType Type { get; }
    }
}