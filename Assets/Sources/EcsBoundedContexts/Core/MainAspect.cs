using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.SwingingTrees.Domain;
using Sources.Trees.Components;

namespace Sources
{
    public class MainAspect : ProtoAspectInject
    {
        public readonly ProtoPool<TreeTag> TreePool = new ProtoPool<TreeTag>();
        public readonly ProtoPool<JumpEvent> JumpEventPool = new ProtoPool<JumpEvent>();
        public readonly ProtoPool<SweengingTreeComponent> TreeSwingerPool = new ProtoPool<SweengingTreeComponent>();
    }
}