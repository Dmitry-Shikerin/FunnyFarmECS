using System;
using System.Collections.Generic;
using System.Linq;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using TeoGames.Mesh_Combiner.Scripts.Extension;

namespace Sources.BoundedContexts.SelectableItems.Infrastructure
{
    public class SelectableService : ISelectableService
    {
        private readonly IInputService _inputService;
        private readonly List<ISelectableItem> _items = new List<ISelectableItem>();
        
        private ISelectableItem _current;

        public SelectableService(IInputService inputService)
        {
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
        }

        public void Initialize()
        {
            _inputService.InputData.SelectItem += Select;
        }

        public void Destroy()
        {
            _inputService.InputData.SelectItem -= Select;
        }

        public void Select(ISelectableItem item)
        {
            if (_current != null && _current.Equals(item))
                return;
            
            _items
                .Except(new [] { item, })
                .ForEach(listItem => listItem.Deselect());
            
            item.Select();
            _current = item;
        }

        public void Add(ISelectableItem item)
        {
            if (_items.Contains(item))
                throw new InvalidOperationException(item.GetType().Name);
            
            _items.Add(item);
        }
    }
}