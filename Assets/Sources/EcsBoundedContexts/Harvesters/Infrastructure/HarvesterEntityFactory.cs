using System.Linq;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.GameObjects;
using Sources.EcsBoundedContexts.Harvesters.Domain;
using Sources.EcsBoundedContexts.Harvesters.Presentation;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.WaterTractors.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.MyLeoEcsProto.Factories;
using Sources.Transforms;

namespace Sources.EcsBoundedContexts.Harvesters.Infrastructure
{
    public class HarvesterEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        public HarvesterEntityFactory(
            ProtoWorld world, 
            MainAspect aspect,
            IAssetCollector assetCollector) 
            : base(world, aspect)
        {
            _assetCollector = assetCollector;
        }

        public ProtoEntity Create(HarvesterView view)
        {
            HarvesterConfig config = _assetCollector.Get<HarvesterConfig>();
            
            ref HarvesterEnumStateComponent state = ref Aspect.HarvesterState.NewEntity(out ProtoEntity entity);
            state.State = HarvesterState.Home;
            
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