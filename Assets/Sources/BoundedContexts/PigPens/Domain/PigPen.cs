using System;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.PigPens.Domain
{
    public class PigPen : IEntity
    {
        public string Id { get; set; }
        public Type Type => GetType();
    }
}