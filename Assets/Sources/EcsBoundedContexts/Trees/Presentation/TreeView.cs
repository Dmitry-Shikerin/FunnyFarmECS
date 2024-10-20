using Sources.EcsBoundedContexts.EntityLinks;
using Sources.EcsBoundedContexts.Trees.Domain.Types;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Trees.Presentation
{
    public class TreeView : EntityView
    {
        [field: SerializeField] public TreeType TreeType { get; private set; }
    }
}