using System;
using System.Collections.Generic;
using Sources.BoundedContexts.AnimalMovePoints;
using Sources.BoundedContexts.Paths.Domain;
using Sources.BoundedContexts.Paths.Presentation;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Animals.Domain;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.Animals.Infrastructure
{
    public class AnimalPointsService
    {
        private readonly PathCollectorView _pathCollectorView;
        private readonly Dictionary<PathOwnerType, Vector3[]> _paths = new ();

        public AnimalPointsService(RootGameObject rootGameObject)
        {
            _pathCollectorView = rootGameObject.PathCollector;
        }

        public Vector3 GetNextMovePoint(AnimalType animal)
        {
            Vector3[] dogPoints = GetPath(PathOwnerType.FirstLocationDog);
            Vector3[] catPoints = GetPath(PathOwnerType.FirstLocationCat);
            Vector3[] sheepPoints = GetPath(PathOwnerType.SecondLocationSheep);
            Vector3[] chickenPoints = GetPath(PathOwnerType.FirstLocationChicken);
            
            return animal switch
            {
                AnimalType.Dog => dogPoints[Random.Range(0, dogPoints.Length)],
                AnimalType.Cat => catPoints[Random.Range(0, catPoints.Length)],
                AnimalType.Sheep => sheepPoints[Random.Range(0, sheepPoints.Length)],
                AnimalType.Chicken => chickenPoints[Random.Range(0, chickenPoints.Length)],
                _ => throw new ArgumentException("unknown animal type")
            };
        }

        private Vector3[] GetPath(PathOwnerType pathOwnerType)
        {
            if (_paths.TryGetValue(pathOwnerType, out Vector3[] path))
                return path;
            
            _paths[pathOwnerType] = _pathCollectorView.GetPath(pathOwnerType, PathType.Points);
            
            return _paths[pathOwnerType];
        }
    }
}