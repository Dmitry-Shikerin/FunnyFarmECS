using System.Collections.Generic;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using UnityEngine;

namespace Sources.MyLeoEcsProto.ComponentContainers.Presentation
{
    public class ComponentContainer : MonoBehaviour
    {
        [SerializeReference] private List<ComponentView> _components;
        
        public List<ComponentView> Components => _components;
    }
}