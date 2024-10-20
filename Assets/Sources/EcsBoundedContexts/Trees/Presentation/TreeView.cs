using Sources.EcsBoundedContexts.Trees.Domain;
using Sources.EcsBoundedContexts.Trees.Domain.Types;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Trees.Presentation
{
    public class TreeView : View
    {
        [field: SerializeField] public TreeType TreeType { get; private set; }
    }
}