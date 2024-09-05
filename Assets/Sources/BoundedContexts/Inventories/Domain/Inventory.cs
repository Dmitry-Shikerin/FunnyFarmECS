using System;
using System.Collections.Generic;
using Sources.Frameworks.Domain.Interfaces.Entities;

namespace Sources.BoundedContexts.Inventories.Domain
{
    public class Inventory : IEntity
    {
        public event Action InventoryChanged;
        
        public IDictionary<string, int> Items { get; set; }
        public string Id { get; set; }
        public Type Type => GetType();

        public void Add(string id, int amount)
        {
            if (Items.ContainsKey(id) == false)
                throw new KeyNotFoundException($"Item {id} not found");
            
            Items[id] += amount;
            InventoryChanged?.Invoke();
        }
    }
}