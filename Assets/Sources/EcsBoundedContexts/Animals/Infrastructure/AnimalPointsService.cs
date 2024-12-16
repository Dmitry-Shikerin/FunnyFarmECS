using System;
using System.Collections.Generic;
using Sources.BoundedContexts.AnimalMovePoints;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Animals.Domain;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.Animals.Infrastructure
{
    public class AnimalPointsService
    {
        private readonly RootGameObject _rootGameObject;

        public AnimalPointsService(RootGameObject rootGameObject)
        {
            _rootGameObject = rootGameObject;
        }

        public Vector3 GetNextMovePoint(AnimalType animal)
        {
            IReadOnlyList<AnimalMovePoint> dogPoints = _rootGameObject.DogHouseView.Points;
            IReadOnlyList<AnimalMovePoint> catPoints = _rootGameObject.CatHouseView.Points;
            IReadOnlyList<AnimalMovePoint> sheepPoints = _rootGameObject.SheepPenView.Points;
            IReadOnlyList<AnimalMovePoint> chickenPoints = _rootGameObject.ChickenCorralView.Points;
            
            return animal switch
            {
                AnimalType.Dog => dogPoints[Random.Range(0, dogPoints.Count)].Position,
                AnimalType.Cat => catPoints[Random.Range(0, catPoints.Count)].Position,
                AnimalType.Sheep => sheepPoints[Random.Range(0, sheepPoints.Count)].Position,
                AnimalType.Chicken => chickenPoints[Random.Range(0, chickenPoints.Count)].Position,
                _ => throw new ArgumentException("unknown animal type")
            };
        }
    }
}