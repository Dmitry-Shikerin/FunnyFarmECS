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
        
        public EntityView EntityView { get; private set; }
        public Type ComponentType { get; private set; }

        // public int Entity => EntityView.Entity;

        public ComponentView(EntityView entityView)
        {
            EntityView = entityView;
        }

        public ComponentView Init(Type componentType)
        {
            if (ComponentType == componentType)
                return this;

            ComponentType = componentType;
            componentName = componentType.Name;
            return this;
        }

        public void SetValue()
        {
        }

        public void SetValue(object value)
        {
        }

        public void Refresh()
        {
        }
    }
}