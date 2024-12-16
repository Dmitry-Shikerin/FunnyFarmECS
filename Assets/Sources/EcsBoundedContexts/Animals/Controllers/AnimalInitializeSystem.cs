using System;
using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Animals.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Animals.Presentation;

namespace Sources.EcsBoundedContexts.Animals.Controllers
{
    public class AnimalInitializeSystem : IProtoInitSystem
    {
        private readonly AnimalEntityFactory _animalEntityFactory;
        private readonly RootGameObject _rootGameObject;
        
        public AnimalInitializeSystem(
            AnimalEntityFactory animalEntityFactory,
            RootGameObject rootGameObject)
        {
            _animalEntityFactory = animalEntityFactory ?? throw new ArgumentNullException(nameof(animalEntityFactory));
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
        }
        
        public void Init(IProtoSystems systems)
        {
            _animalEntityFactory.Create(_rootGameObject.DogHouseView.AnimalView);
            _animalEntityFactory.Create(_rootGameObject.CatHouseView.AnimalView);

            foreach (AnimalView sheep in _rootGameObject.SheepPenView.Ships)
                _animalEntityFactory.Create(sheep);

            foreach (AnimalView chicken in _rootGameObject.ChickenCorralView.Chickens)
                _animalEntityFactory.Create(chicken);
        }
    }
}