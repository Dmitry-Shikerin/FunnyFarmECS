using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Farmers.Presentation
{
    public class FarmerMovePointView : View
    {
        [field: SerializeField] public FarmerPointType FarmerPointType { get; private set; }
    }
}