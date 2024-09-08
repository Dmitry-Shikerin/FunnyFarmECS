using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.Watermills.Domain
{
    public class Watermill : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
    }
}