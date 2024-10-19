using System;

namespace Sources.BoundedContexts.UiSelectables.Domain
{
    public class Selectable : ISelectable
    {
        public event Action Selected;
        
        public string Id { get; set; }
        public Type Type => GetType();

        public void Select() =>
            Selected?.Invoke();
    }
}