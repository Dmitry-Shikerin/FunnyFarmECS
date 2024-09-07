using System;
using System.Collections.Generic;
using Sources.Frameworks.Domain.Interfaces.Entities;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;

namespace Sources.Frameworks.GameServices.Repositories.Services.Implementation
{
    public class EntityRepository : IEntityRepository
    {
        private readonly Dictionary<string, IEntity> _entities = new Dictionary<string, IEntity>();

        public IReadOnlyDictionary<string, IEntity> Entities => _entities;
        
        public void Add(IEntity entity)
        {
            if (_entities.TryAdd(entity.Id, entity) == false)
                throw new InvalidOperationException($"Entity {entity.Id} with this Id already exists");
        }

        public IEntity Get(string id)
        {
            if (_entities.TryGetValue(id, out IEntity entity) == false)
                throw new InvalidOperationException($"Entity {id} with this Id does not exist");

            return entity;
        }

        public T Get<T>(string id) where T : class, IEntity
        {
            if (_entities.TryGetValue(id, out IEntity entity) == false) 
                throw new KeyNotFoundException($"Entity {id} with this Id does not exist");

            if(entity is not T concreteEntity)
                throw new InvalidCastException($"Entity {id} with this Id does not exist");
            
            return concreteEntity;
        }

        public void Release() =>
            _entities.Clear();

        public IEnumerable<T> GetAll<T>(IEnumerable<string> ids) 
            where T : class, IEntity
        {
            List<T> result = new List<T>();
            
            foreach (string id in ids)
            {
                if (_entities.TryGetValue(id, out var entity) == false)
                    throw new KeyNotFoundException(id);
                
                if (entity is not T concreteEntity)
                    throw new InvalidCastException($"Entity {id} with this Id does not exist");
                
                result.Add(concreteEntity);
            }

            return result;
        }
    }
}