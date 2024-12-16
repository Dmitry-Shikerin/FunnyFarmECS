using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Transforms;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.EcsBoundedContexts.NavMeshes.Controllers
{
    public class NavMeshMoveSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                TransformComponent,
                NavMeshComponent,
                TargetPointComponent>());
        [DI] private readonly MainAspect _aspect;

        public void Run()
        {
            foreach (ProtoEntity entity in _protoIt)
            {
                NavMeshComponent navMesh = _aspect.NavMesh.Get(entity);
                ref TargetPointComponent targetPoint = ref _aspect.TargetPoint.Get(entity);
                
                NavMeshAgent agent = navMesh.Value;
                agent.SetDestination(targetPoint.Value);
                
                float stoppingDistance = agent.stoppingDistance + 0.1f;

                if (Vector3.Distance(agent.destination, agent.transform.position) <= stoppingDistance)
                    _aspect.TargetPoint.Del(entity);
            }
        }
    }
}