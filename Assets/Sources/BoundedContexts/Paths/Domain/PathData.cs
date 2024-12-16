using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.BoundedContexts.Paths.Domain
{
    [Serializable]
    public struct PathData
    {
        public bool IsDrawGizmos;
        public Color SphereColor;
        public float SphereRadius;
        public Color LineColor;
        public PathTypeSerializedDictionary PathTypes;
        
        [Button]
        public void SetDefaultValues()
        {
            IsDrawGizmos = true;
            SphereColor = Color.green;
            SphereRadius = 0.3f;
            LineColor = Color.green;
        }
    }
}