using Sources.BoundedContexts.Paths.Domain;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Paths.Presentation
{
    public class PathCollectorView : View
    {
        [SerializeField] private PathsSerializedDictionary _paths;

        private void OnDrawGizmos()
        {
            foreach (PathData pathData in _paths.Values)
            {
                if (pathData.IsDrawGizmos == false)
                    continue;
                
                foreach (PathTypeData pathTypeDate in pathData.PathTypes.Values)
                {
                    int count = pathTypeDate.Points.Count;

                    for (int i = 0; i < count; i++)
                    {
                        if (pathTypeDate.Points[i].Transform == null)
                            continue;

                        Vector3 point = pathTypeDate.Points[i].Transform.position;

                        Gizmos.color = pathData.SphereColor;
                        Gizmos.DrawSphere(point, pathData.SphereRadius);

                        if (i == count - 1)
                            break;

                        Vector3 nextPoint = pathTypeDate.Points[i + 1].Transform.position;
                        Debug.DrawLine(point, nextPoint, pathData.LineColor);
                    }
                }
            }
        }

        private void DrawArrow(Vector3 start, Vector3 end)
        {
            Vector3 direction = end - start;
            
            Vector3 right = Vector3.Cross(direction, Vector3.up).normalized;
            
            //Debug.DrawLine();
        }
    }
}