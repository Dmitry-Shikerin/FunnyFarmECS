using Sources.EcsBoundedContexts.Animals.Domain;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.AnimalMovePoints
{
    public class AnimalMovePoint : View
    {
        [SerializeField] private AnimalType _animalType;
        
        public Vector3 Position => transform.position;
    }
}