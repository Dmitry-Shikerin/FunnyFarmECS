using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DeliveryCars.Domain;
using Sources.EcsBoundedContexts.DeliveryCars.Presentation;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.DeliveryCars.Infrastructure.Factories
{
    public class DeliveryCarEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        public DeliveryCarEntityFactory(
            ProtoWorld world,
            MainAspect aspect,
            IAssetCollector assetCollector) 
            : base(world, aspect)
        {
            _assetCollector = assetCollector;
        }
        
        public ProtoEntity Create(DeliveryCarView view)
        {
            DeliveryCarConfig config = _assetCollector.Get<DeliveryCarConfig>();
            
            ref DeliveryCarEnumStateComponent state = ref Aspect.DeliveryCarState.NewEntity(out ProtoEntity entity);
            state.State = DeliveryCarState.HomeIdle;
            
            ref MoveSpeedComponent moveSpeed = ref Aspect.MoveSpeed.Add(entity);
            moveSpeed.MoveSpeed = config.MoveSpeed;
            moveSpeed.RotationSpeed = config.RotationSpeed;
            ref PointPathComponent pointPath = ref Aspect.PointsPath.Add(entity);
            pointPath.PathOwnerType = view.PathOwnerType;
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            ref GameObjectComponent gameObject = ref Aspect.GameObject.Add(entity);
            gameObject.GameObject = view.gameObject;
            
            return new ProtoEntity();
        }
    }
}