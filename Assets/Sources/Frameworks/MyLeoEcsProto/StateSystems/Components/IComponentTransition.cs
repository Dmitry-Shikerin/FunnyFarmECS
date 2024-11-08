using Leopotam.EcsProto;

namespace Sources.MyLeoEcsProto.StaeSystems.Components
{
    public interface IComponentTransition
    {
        void Transit(ProtoEntity entity);
        bool CanTransit(ProtoEntity entity);
    }
}

