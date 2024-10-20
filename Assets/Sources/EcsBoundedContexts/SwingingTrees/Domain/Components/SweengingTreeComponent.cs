using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Trees.Domain.Types;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;

namespace Sources.EcsBoundedContexts.SwingingTrees.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("SwingingTree")]
    public struct SweengingTreeComponent : IComponent
    {
        public TreeType TreeType;
        public bool EnableYAxisSwingingTree;
        public float SpeedX;
        public float SpeedY;
        public float MaxAngleX;
        public float MaxAngleY;
        public float Direction;
    }
}