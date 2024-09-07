using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.Houses.Domain
{
    public class House : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
    }
}