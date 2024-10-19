using System;
using Sources.BoundedContexts.UiSelectables.Domain;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.GoosePens.Domain
{
    public class GoosePen : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}