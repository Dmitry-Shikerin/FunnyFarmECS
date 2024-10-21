using System;
using System.Collections.Generic;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using UnityEngine;

namespace Sources.MyLeoEcsProto.ComponentContainers.Presentation
{
    public class EntityView : MonoBehaviour
    {
        [SerializeReference] private List<ComponentView> _components;
        [SerializeField] private int _componentsCount;

        public List<ComponentView> Components => _components;

        public int ComponentsCount
        {
            get => _componentsCount;
            private set
            {
                if (_componentsCount == value)
                    return;

                if (_componentsCount < 0)
                    throw new ArgumentOutOfRangeException(nameof(ComponentsCount));

                if (value != _components.Count)
                    throw new InvalidOperationException();

                _componentsCount = value;
            }
        }

        public void Remove(ComponentView componentView)
        {
            Debug.Log($"Remove: {componentView.Component.GetType().Name}");
            _components.Remove(componentView);
            ComponentsCount = _components.Count;
        }

        public void Add(ComponentView componentView)
        {
            _components.Add(componentView);
            ComponentsCount = _components.Count;
        }

        public void Clear()
        {
            _components.Clear();
            ComponentsCount = _components.Count;
        }
    }
}