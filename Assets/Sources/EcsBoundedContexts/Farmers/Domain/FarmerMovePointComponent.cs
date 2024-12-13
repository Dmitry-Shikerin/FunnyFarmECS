using Sources.EcsBoundedContexts.Farmers.Presentation;

namespace Sources.EcsBoundedContexts.Farmers.Domain
{
    public struct FarmerMovePointComponent
    {
        public FarmerMovePointView TargetPoint;
        public FarmerMovePointView[] Points;
    }
}