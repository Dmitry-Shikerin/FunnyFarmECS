using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Scales;
using Sources.EcsBoundedContexts.Vegetations.Domain;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Vegetations.Infrastructure
{
    public class VegetationIdleSystem : EnumStateSystem<VegetationState, VegetationEnumStateComponent>
    {
        [DI] private readonly ProtoIt _it =
            new(It.Inc<
                VegetationEnumStateComponent,
                ScaleComponent,
                TransformComponent>());
        [DI] private readonly MainAspect _aspect;

        protected override ProtoIt ProtoIt => _it;
        protected override ProtoPool<VegetationEnumStateComponent> Pool => _aspect.VegetationState;

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == VegetationState.Idle;

        protected override void Enter(ProtoEntity entity)
        {
            ref TransformComponent transform = ref _aspect.Transform.Get(entity);
            transform.Transform.localScale = Vector3.zero;
        }

        protected override void Update(ProtoEntity entity)
        {
        }
    }
}