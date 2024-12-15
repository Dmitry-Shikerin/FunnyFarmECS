using UnityEngine;

namespace Sources.EcsBoundedContexts.Harvesters.Domain
{
    public struct HarvesterMovePointComponent
    {
        public Vector3 TargetPoint;
        public int TargetPointIndex;
        public Vector3[] ToFieldPoints;
        public Vector3[] ToHomePoints;
    }
}