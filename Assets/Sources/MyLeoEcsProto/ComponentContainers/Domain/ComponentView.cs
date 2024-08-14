using System;
using Leopotam.EcsProto;
using Sources.MyLeoEcsProto.ComponentContainers.Presentation;
using UnityEngine;

namespace Sources.MyLeoEcsProto.ComponentContainers.Domain
{
    [Serializable]
    public class ComponentView
    {
        [HideInInspector] public string componentName;
        [SerializeReference] public object Component;
        public ComponentContainer EntityView { get; private set; }
        public Type ComponentType { get; private set; }

        // public int Entity => EntityView.Entity;

        public ComponentView(ComponentContainer entityView)
        {
            EntityView = entityView;
        }

        public ComponentView Init(Type componentType)
        {
            if (ComponentType == componentType)
                return this;

            ComponentType = componentType;
            componentName = componentType.Name;
            // Pool = World.GetPoolByType(componentType);
            return this;
        }

        public void SetValue()
        {
            // if (EntityView.IsAlive) Pool.SetRaw(Entity, Component);
        }

        public void SetValue(object value)
        {
            // if (EntityView.IsAlive) Pool.SetRaw(Entity, Component = value);
        }

        public void Refresh()
        {
        }
    }
}