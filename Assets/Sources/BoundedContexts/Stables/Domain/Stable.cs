using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.Stables.Domain
{
    public class Stable : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
    }
}