using System;
using Sources.BoundedContexts.UiSelectables.Domain;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.SheepPens.Domain
{
    public class SheepPen : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}