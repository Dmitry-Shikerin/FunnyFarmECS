using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Jobs;
using Sources.Transforms;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Systems
{
    public class TreeSwingerSystem : IProtoRunSystem
    {
        [DI] private readonly MainAspect _aspect = default;
        [DI] private readonly ProtoIt _swingingTreeInc = new (It.Inc<SweengingTreeComponent, TransformComponent>());

        private NativeArray<SweengingTreeComponent> _treeSwingers;
        private TransformAccessArray _transformAccess;

        public void Run()
        {
            int len = _swingingTreeInc.Len();
            _treeSwingers = new NativeArray<SweengingTreeComponent>(len, Allocator.TempJob);
            _transformAccess = new TransformAccessArray(len);
            
            int index = len - 1;
            
            foreach (ProtoEntity entity in _swingingTreeInc)
            {
                SweengingTreeComponent treeSwinger = _aspect.TreeSwinger.Get(entity);
                ref TransformComponent treeTransform = ref _aspect.Transform.Get(entity);

                _treeSwingers[index] = treeSwinger;
                _transformAccess.Add(treeTransform.Transform);

                index--;
            }

            TreeSwingJob job = new TreeSwingJob(_treeSwingers, Time.time);
            job
                .Schedule(_transformAccess)
                .Complete();
            _treeSwingers.Dispose();
            _transformAccess.Dispose();
        }
    }
}