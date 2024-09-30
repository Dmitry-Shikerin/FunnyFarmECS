using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorVisualizer;

namespace VectorVisualizer
{
    public class VectorVisualizerTester : MonoBehaviour
    {
        public Vector3 Vector3;
        public Vector2 Vector2;

        [VisualizableVector] public Vector3 Vector3WithAttribute;
        [VisualizableVector] public Vector2 Vector2WithAttribute;

        public List<Vector3> Vector3List;
        public List<Vector2> Vector2List;

        [VisualizableVector] public List<Vector3> Vector3ListWithAttribute;
        [VisualizableVector] public List<Vector2> Vector2ListWithAttribute;
    }
}