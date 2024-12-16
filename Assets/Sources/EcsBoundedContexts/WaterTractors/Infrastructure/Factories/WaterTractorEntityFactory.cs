using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.WaterTractors.Infrastructure.Factories
{
    public class WaterTractorEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        public WaterTractorEntityFactory(
            ProtoWorld world, 
            MainAspect aspect,
            IAssetCollector assetCollector) 
            : base(world, aspect)
        {
            _assetCollector = assetCollector;
        }
        
        public ProtoEntity Create(WaterTractorView view)
        {
            WaterTractorConfig config = _assetCollector.Get<WaterTractorConfig>();
            
            ref WaterTractorEnumStateComponent state = ref Aspect.WaterTractorState.NewEntity(out ProtoEntity entity);
            state.State = WaterTractorState.Home;
            
            Aspect.PointsPath.Add(entity);
            ref MoveSpeedComponent moveSpeed = ref Aspect.MoveSpeed.Add(entity);
            moveSpeed.MoveSpeed = config.MoveSpeed;
            moveSpeed.RotationSpeed = config.RotationSpeed;
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            ref GameObjectComponent gameObject = ref Aspect.GameObject.Add(entity);
            gameObject.GameObject = view.gameObject;
            
            return entity;
        }
    }
}