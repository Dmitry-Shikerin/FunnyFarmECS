using System;
using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Trees.Presentation;

namespace Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Systems
{
    public class TreeSwingInitSystem : IProtoInitSystem
    {
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