using System;
using Leopotam.EcsProto.Unity;

namespace Sources.EcsBoundedContexts.Animals.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("AnimalType")]
    public struct AnimalTypeComponent
    {
        public AnimalType AnimalType;
    }
}