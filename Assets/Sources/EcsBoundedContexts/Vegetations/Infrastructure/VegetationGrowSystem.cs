using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Scales;
using Sources.EcsBoundedContexts.Vegetations.Domain;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Implementation;
using Sources.MyLeoEcsProto.States.Controllers;
using Sources.Transforms;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Vegetations.Infrastructure
{
    public class VegetationGrowSystem : EnumStateSystem<VegetationState, VegetationEnumStateComponent>
    {
        [DI] private readonly ProtoIt _it =
            new(It.Inc<
                VegetationEnumStateComponent,
                ScaleComponent,
                TransformComponent>());        
        [DI] private readonly ProtoIt _transitionIt =
            new(It.Inc<
                VegetationEnumStateComponent,
                ScaleComponent,
                TransformComponent>());
        [DI] private readonly MainAspect _aspect;

        protected override ProtoIt ProtoIt => _it;
        protected override ProtoPool<VegetationEnumStateComponent> Pool => _aspect.VegetationState;

        public override void Init(IProtoSystems systems)
        {
            AddTransition(ToReadyTransition());
        }

        protected override bool IsState(ProtoEntity entity) =>
            Pool.Get(entity).State == VegetationState.Grow;

        protected override void Enter(ProtoEntity entity)
        {
        }

        protected override void Update(ProtoEntity entity)
        {
            TransformComponent transformComponent = _aspect.Transform.Get(entity);
            ScaleComponent scaleComponent = _aspect.Scale.Get(entity);
            
            transformComponent.Transform.localScale = Vector3.MoveTowards(
                transformComponent.Transform.localScale, 
                scaleComponent.TargetScale,
                0.5f * Time.deltaTime);
        }

        private Transition<VegetationState> ToReadyTransition()
        {
            return new Transition<VegetationState>(
                VegetationState.Ready,
                _ =>
                {
                    foreach (ProtoEntity entity in _transitionIt)
                    {
                        ScaleComponent scaleComponent = _aspect.Scale.Get(entity);
                        TransformComponent transformComponent = _aspect.Transform.Get(entity);
                        
                        if (transformComponent.Transform.localScale != scaleComponent.TargetScale)
                            return false;
                    }

                    return true;
                });
        } 
    }
}