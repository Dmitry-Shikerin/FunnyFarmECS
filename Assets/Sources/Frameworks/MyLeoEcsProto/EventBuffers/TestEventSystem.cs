using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using UnityEngine;

namespace Sources.Frameworks.MyLeoEcsProto.EventBuffers
{
    public class TestEventSystem : EventSystem<TestEvent>
    {
        [DI] private readonly MainAspect _aspect;
        [DI] private readonly ProtoIt _it = new (It.Inc<TestEvent>());

        protected override ProtoPool<TestEvent> Pool => _aspect.TestEvent;
        protected override ProtoIt Iterator => _it;
        
        protected override void Receive(ref TestEvent @event)
        {
            Debug.Log($"Test");
        }
    }
}