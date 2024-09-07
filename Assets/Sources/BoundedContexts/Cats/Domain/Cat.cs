using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.Cats.Domain
{
    public class Cat : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
    }
}