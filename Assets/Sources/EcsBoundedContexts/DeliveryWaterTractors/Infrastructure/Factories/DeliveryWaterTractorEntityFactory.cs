using System.Linq;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Presentation;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.DeliveryWaterTractors.Infrastructure.Factories
{
    public class DeliveryWaterTractorEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        public DeliveryWaterTractorEntityFactory(
            ProtoWorld world,
            MainAspect aspect,
            IAssetCollector assetCollector) 
            : base(world, aspect)
        {
            _assetCollector = assetCollector;
        }

        public ProtoEntity Create(DeliveryWaterTractorView view)
        {
            DeliveryWaterTractorConfig config = _assetCollector.Get<DeliveryWaterTractorConfig>();
            
            ref DeliveryWaterTractorEnumStateComponent state = ref Aspect.DeliveryWaterTractorState.NewEntity(out ProtoEntity entity);
            state.State = DeliveryWaterTractorState.Home;
            
            Aspect.PointsPath.Add(entity);
            ref MoveSpeedComponent moveSpeed = ref Aspect.MoveSpeed.Add(entity);
            moveSpeed.MoveSpeed = config.MoveSpeed;
            moveSpeed.RotationSpeed = config.RotationSpeed;
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            ref GameObjectComponent gameObject = ref Aspect.GameObject.Add(entity);
            gameObject.GameObject = view.gameObject;
            
            return new ProtoEntity();
        }
    }
}