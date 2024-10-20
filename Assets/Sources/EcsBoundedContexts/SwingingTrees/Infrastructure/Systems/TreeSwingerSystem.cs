using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Services;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Systems
{
    public class TreeSwingerSystem : IProtoRunSystem
    {
        private readonly TreeSwingService _treeSwingService;
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _swingingTreeInc = new (It.Inc<SweengingTreeComponent, TransformComponent>());

        public TreeSwingerSystem(TreeSwingService treeSwingService)
        {
            _treeSwingService = treeSwingService ?? throw new ArgumentNullException(nameof(treeSwingService));
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _swingingTreeInc)
            {
                SweengingTreeComponent treeSwinger = _aspect.TreeSwinger.Get(entity);
                ref TransformComponent treeTransform = ref _aspect.Transform.Get(entity);

                treeTransform.Transform.rotation = _treeSwingService.GetSwing(treeSwinger);
            }
        }
    }
}