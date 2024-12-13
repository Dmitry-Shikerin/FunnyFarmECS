using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.EcsBoundedContexts.NavMeshes.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.MyLeoEcsProto.States.Controllers;
using Sources.Transforms;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.Farmers.Infrastructure
{
    public class FarmerMoveSystem : EnumStateSystem<FarmerState, FarmerEnumStateComponent>
    {
        [DI] private readonly ProtoIt _protoIt 
            = new (It.Inc<
            FarmerEnumStateComponent,
            TransformComponent,
            AnimancerEcsComponent,
            FarmerMovePointComponent>());
        [DI] private readonly MainAspect _aspect;
        private readonly IAssetCollector _assetCollector;
        
        private FarmerConfig _config;

        public FarmerMoveSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        protected override ProtoIt ProtoIt => _protoIt;
        protected override ProtoPool<FarmerEnumStateComponent> Pool => _aspect.FarmerState;

        public override void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<FarmerConfig>();
            AddTransition(ToRandomTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == FarmerState.Move;

        protected override void Enter(ProtoEntity entity)
        {
            _aspect.Animancer.Get(entity).Animancer.Play(_config.Move);
            ref FarmerMovePointComponent movePoint = ref _aspect.FarmerMovePoint.Get(entity);
            movePoint.TargetPoint = movePoint.Points[Random.Range(0, movePoint.Points.Length)];
        }

        protected override void Update(ProtoEntity entity)
        {
            ref FarmerEnumStateComponent state = ref Pool.Get(entity);
            state.Timer -= Time.deltaTime;
        }

        private MutableStateTransition<FarmerState> ToRandomTransition()
        {
            return new MutableStateTransition<FarmerState>(
                (entity) =>
                {
                    ref FarmerMovePointComponent movePoint = ref _aspect.FarmerMovePoint.Get(entity);

                    return movePoint.TargetPoint.FarmerPointType
                        switch
                        {
                            FarmerPointType.Idle => FarmerState.Idle,
                            FarmerPointType.Work => FarmerState.Work,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                },
                (entity) =>
                {
                    NavMeshComponent navMesh = _aspect.NavMesh.Get(entity);
                    NavMeshAgent agent = navMesh.Agent;
                    
                    return Vector3.Distance(agent.transform.position, agent.destination) <= agent.stoppingDistance + 0.1f;
                });
        }
    }
}