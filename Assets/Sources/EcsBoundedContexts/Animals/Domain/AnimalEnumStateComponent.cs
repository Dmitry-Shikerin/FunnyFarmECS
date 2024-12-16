using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Dogs.Domain;
using Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain;

namespace Sources.EcsBoundedContexts.Animals.Domain
{
    [Serializable]
    [ProtoUnityAuthoring("AnimalState")]
    public struct AnimalEnumStateComponent : IEnumStateComponent<AnimalState>
    {
        public AnimalState State { get; set; }
        public bool IsEntered { get; set; }
    }
}