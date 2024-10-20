using System;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Configs;
using Sources.EcsBoundedContexts.Trees.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.MyLeoEcsProto.Factories;
using Sources.SwingingTrees.Domain.Configs;
using Sources.Transforms;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Factories
{
    public class TreeSwingEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        public TreeSwingEntityFactory(
            IAssetCollector assetCollector, 
            ProtoWorld world,
            MainAspect aspect) 
            : base(world, aspect)
        {
            _assetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
        }
        
        public ProtoEntity Create(TreeView view)
        {
            TreeSwingerCollector collector = _assetCollector.Get<TreeSwingerCollector>();
            TreeSwingerConfig config = collector.GetById(view.TreeType.ToString());
            ref SweengingTreeComponent treeSwinger = ref Aspect.TreeSwinger.NewEntity(out ProtoEntity entity);
            Initialize(ref treeSwinger, config);
            ref TransformComponent transform = ref Aspect.Transform.Add(entity);
            transform.Transform = view.Transform;
            
            return new ProtoEntity();
        }
        
        private void Initialize(ref SweengingTreeComponent treeSwinger, TreeSwingerConfig config)
        {
            treeSwinger.MaxAngleX = config.SwingMaxAngleX + Random.Range(-config.SwingMaxAngleRandomnessX, config.SwingMaxAngleRandomnessX);
            treeSwinger.MaxAngleY = config.SwingMaxAngleY + Random.Range(-config.SwingMaxAngleRandomnessY, config.SwingMaxAngleRandomnessY);
            treeSwinger.SpeedX = config.SwingSpeedX + Random.Range(-config.SwingSpeedRandomnessX, config.SwingSpeedRandomnessX);
            treeSwinger.SpeedY = config.SwingSpeedY + Random.Range(-config.SwingSpeedRandomnessY, config.SwingSpeedRandomnessY);
            treeSwinger.Direction = config.Direction + Random.Range(-config.DirectionRandomness, config.DirectionRandomness);
            treeSwinger.EnableYAxisSwingingTree = config.EnableYAxisSwinging;
        }
    }
}