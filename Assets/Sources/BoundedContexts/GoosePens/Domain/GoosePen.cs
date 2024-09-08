using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.GoosePens.Domain
{
    public class GoosePen : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}