using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.Jeeps.Domain
{
    public class Jeep : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
    }
}