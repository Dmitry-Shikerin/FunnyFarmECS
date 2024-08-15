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
            set
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
        // public int ComponentsCount => _components.Count;
    }
}