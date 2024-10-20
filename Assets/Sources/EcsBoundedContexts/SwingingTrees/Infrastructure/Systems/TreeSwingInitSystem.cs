using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Trees.Presentation;
using Sources.Trees.Components;

namespace Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Systems
{
    public class TreeSwingInitSystem : IProtoInitSystem
    {
        [DI] private readonly ProtoIt _swingingTreeInc = new (It.Inc<TreeTag, SweengingTreeComponent>());
        private readonly TreeSwingEntityFactory _entityFactory;
        private readonly RootGameObject _rootGameObject;

        public TreeSwingInitSystem(
            TreeSwingEntityFactory entityFactory,
            RootGameObject rootGameObject)
        {
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
        }

        public void Init(IProtoSystems systems)
        {
            TreeView[] trees = _rootGameObject.FirstLocationRootTrees.GetComponentsInChildren<TreeView>();

            foreach (TreeView tree in trees)
            {
                _entityFactory.Create(tree);
            }
        }
    }
}