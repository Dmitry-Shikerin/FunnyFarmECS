using UnityEngine;

namespace Sources.EcsBoundedContexts.WaterTractors.Domain
{
    public struct WaterTractorMovePointComponent
    {
        public Vector3 TargetPoint;
        public int TargetPointIndex;
        public Vector3[] ToFieldPoints;
        public Vector3[] ToHomePoints;
    }
}