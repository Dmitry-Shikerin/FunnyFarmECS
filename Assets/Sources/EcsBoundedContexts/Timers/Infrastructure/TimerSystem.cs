using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Timers.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Timers.Infrastructure
{
    public class TimerSystem : IProtoRunSystem
    {
        [DI] private readonly MainAspect _aspect;
        [DI] private readonly ProtoIt _it = new (It.Inc<TimerComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ref TimerComponent timer = ref _aspect.Timer.Get(entity);
                
                timer.Value -= Time.deltaTime;

                if (timer.Value > 0)
                    continue;
                
                _aspect.Timer.Del(entity);
            }
        }
    }
}