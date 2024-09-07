using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.Trucks.Domain
{
    public class Truck : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
    }
}