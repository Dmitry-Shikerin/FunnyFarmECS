using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.SwingingTrees.Domain.Configs;
using Sources.Trees.Components;
using UnityEngine;

namespace Sources.EcsBoundedContexts.SwingingTrees.Infrastructure
{
    public class TreeSwingerSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly MainAspect _mainAspect = default;
        [DI] private readonly ProtoIt _swingingTreeInc = new (It.Inc<TreeTag, SweengingTreeComponent>());
        
        private TreeSwingerCollector _configCollector;
        private bool _enableYAxisSwingingTree;
        private bool _isInitialized;

        public void Init(IProtoSystems systems)
        {
            if (_isInitialized)
                return;
            
            _configCollector = Resources.Load<TreeSwingerCollector>(
                "Configs/TreeSwingers/TreeSwingerCollector");
            TreeSwingerConfig config = _configCollector.Configs.First(config => config.Id == "Tree");
            _enableYAxisSwingingTree = config.EnableYAxisSwinging;
            
            foreach (ProtoEntity entity in _swingingTreeInc)
            {
                ref SweengingTreeComponent treeSwinger = ref _mainAspect.TreeSwinger.Get(entity);
                Initialize(ref treeSwinger, config);
                _isInitialized = true;
            }
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _swingingTreeInc)
            {
                ref SweengingTreeComponent treeSwinger = ref _mainAspect.TreeSwinger.Get(entity);
                treeSwinger.Tree.rotation = Quaternion.Euler(
                    treeSwinger.MaxAngleX * 
                    Mathf.Sin(Time.time * treeSwinger.SpeedX), 
                    (_enableYAxisSwingingTree) 
                        ? treeSwinger.MaxAngleY * Mathf.Sin(Time.time * treeSwinger.SpeedY) 
                        : treeSwinger.Direction, 0f);
            }
        }
        
        private void Initialize(ref SweengingTreeComponent treeSwinger, TreeSwingerConfig config)
        {
            treeSwinger.MaxAngleX = config.SwingMaxAngleX + Random.Range(-config.SwingMaxAngleRandomnessX, config.SwingMaxAngleRandomnessX);
            treeSwinger.MaxAngleY = config.SwingMaxAngleY + Random.Range(-config.SwingMaxAngleRandomnessY, config.SwingMaxAngleRandomnessY);
            treeSwinger.SpeedX = config.SwingSpeedX + Random.Range(-config.SwingSpeedRandomnessX, config.SwingSpeedRandomnessX);
            treeSwinger.SpeedY = config.SwingSpeedY + Random.Range(-config.SwingSpeedRandomnessY, config.SwingSpeedRandomnessY);
            treeSwinger.Direction = config.Direction + Random.Range(-config.DirectionRandomness, config.DirectionRandomness);
        }
    }
}