using System.Collections.Generic;
using System.Linq;
using DrawXXL;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Paths.Domain;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Paths.Presentation
{
    public class PathCollectorView : View
    {
        [EnumToggleButtons]
        [SerializeField] private DrawGizmosType _drawGizmosType;

        [SerializeField] private PathOwnerType _pathOwnerType;
        [Range(0.01f, 0.17f)]
        [SerializeField] private float _vectorWidth = 0.05f;        
        [Range(0.01f, 0.17f)]
        [SerializeField] private float _lineWidth = 0.05f;
        
        [Space(10)]
        [SerializeField] private PathsSerializedDictionary _paths;

        public IReadOnlyDictionary<PathOwnerType, PathData> Paths => _paths;

        private void OnDrawGizmos()
        {
            if (_drawGizmosType != DrawGizmosType.Default)
                return;
            
            DrawPaths();
        }

        private void OnDrawGizmosSelected()
        {
            if (_drawGizmosType != DrawGizmosType.Selected)
                return;
            
            DrawPaths();
        }

        public Vector3[] GetPath(PathOwnerType pathOwnerType, PathType pathType, bool isReverse = false)
        {
            if (_paths.ContainsKey(pathOwnerType) == false)
                throw new KeyNotFoundException($"PathOwnerType {pathOwnerType} not found");
            
            if (_paths[pathOwnerType].PathTypes.ContainsKey(pathType) == false)
                throw new KeyNotFoundException($"PathType {pathType} not found");
            
            IEnumerable<Vector3> path = _paths[pathOwnerType]
                .PathTypes[pathType]
                .Points
                .Select(pointData => pointData.Transform.position);

            if (isReverse)
                path = path.Reverse();
            
            return path.ToArray();
        }

        private void DrawPaths()
        {
            foreach (PathData pathData in _paths.Values)
            {
                if (pathData.IsDrawGizmos == false)
                    continue;
                
                foreach (KeyValuePair<PathType, PathTypeData> pathType in pathData.PathTypes)
                {
                    int count = pathType.Value.Points.Count;

                    for (int i = 0; i < count; i++)
                    {
                        if (pathType.Value.Points[i].Transform == null)
                            continue;

                        Vector3 point = pathType.Value.Points[i].Transform.position;
                        
                        DrawBasics.Point(point, pathData.SphereColor);
                        DrawShapes.Sphere(point, pathData.SphereRadius, pathData.SphereColor, struts: 6);

                        if (i == count - 1)
                            break;

                        if (pathType.Key == PathType.Points)
                            continue;

                        Vector3 nextPoint = pathType.Value.Points[i + 1].Transform.position;
                        Vector3 vector = nextPoint - point;
                        Vector3 normalizedVector = vector.normalized;
                        DrawBasics.Line(point, nextPoint, pathData.LineColor, _lineWidth, animationSpeed: 0);
                        DrawBasics.VectorFrom(point, normalizedVector, pathData.LineColor, lineWidth: _vectorWidth);
                        DrawBasics.VectorTo(normalizedVector, nextPoint, pathData.LineColor, lineWidth: _vectorWidth);
                    }
                }
            }
        }
    }
}