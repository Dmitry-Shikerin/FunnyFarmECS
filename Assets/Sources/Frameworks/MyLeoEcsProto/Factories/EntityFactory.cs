using System;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;

namespace Sources.MyLeoEcsProto.Factories
{
    public class EntityFactory
    {
        public EntityFactory(ProtoWorld world, MainAspect aspect)
        {
            World = world ?? throw new ArgumentNullException(nameof(world));
            Aspect = aspect ?? throw new ArgumentNullException(nameof(aspect));
        }

        protected ProtoWorld World { get; }
        protected MainAspect Aspect { get; }
    }
}