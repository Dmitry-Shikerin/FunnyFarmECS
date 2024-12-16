using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Animancers.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.Transforms;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.Farmers.Controllers
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
            ref FarmerMovePointComponent movePoint = ref _aspect.FarmerMovePoint.Get(entity);
            ref TargetPointComponent targetPoint = ref _aspect.TargetPoint.Add(entity);
            
            movePoint.TargetPoint = movePoint.Points[Random.Range(0, movePoint.Points.Length)];
            targetPoint.Value = movePoint.TargetPoint.Transform.position;
            _aspect.Animancer.Get(entity).Animancer.Play(_config.Move);
        }

        protected override void Update(ProtoEntity entity)
        {
        }

        private MutableStateTransition<FarmerState> ToRandomTransition()
        {
            return new MutableStateTransition<FarmerState>(
                entity =>
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
                entity => _aspect.TargetPoint.Has(entity) == false);
        }
    }
}