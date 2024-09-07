using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.Dogs.Domain
{
    public class Dog : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
    }
}