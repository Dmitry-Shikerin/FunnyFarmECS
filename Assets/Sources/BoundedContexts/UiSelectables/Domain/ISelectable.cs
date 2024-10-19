using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.UiSelectables.Domain
{
    public interface ISelectable : IEntity
    {
        event Action Selected;
        
        void Select();
    }
}