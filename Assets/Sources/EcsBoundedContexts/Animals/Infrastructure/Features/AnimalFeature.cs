using Sources.EcsBoundedContexts.Animals.Controllers;
using Sources.Frameworks.MyLeoEcsProto.Features;

namespace Sources.EcsBoundedContexts.Animals.Infrastructure.Features
{
    public class AnimalFeature : EcsFeature
    {
        public AnimalFeature(
            AnimalInitializeSystem animalInitializeSystem,
            AnimalIdleSystem animalIdleSystem,
            AnimalRunSystem animalRunSystem,
            AnimalWalkSystem animalWalkSystem,
            AnimalChangeEnumStateSystem animalChangeEnumStateSystem,
            IFeatureService featureService) 
            : base(featureService)
        {
            AddSystem(animalInitializeSystem);
            AddSystem(animalIdleSystem);
            AddSystem(animalRunSystem);
            AddSystem(animalWalkSystem);
            AddSystem(animalChangeEnumStateSystem);
        }

        protected override void Register()
        {
        }
    }
}