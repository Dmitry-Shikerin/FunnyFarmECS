using System;
using System.Collections.Generic;
using Leopotam.EcsProto;

namespace Sources.EcsBoundedContexts.Core
{
    public static class AspectExt
    {
        private static MainAspect _mainAspect;

        public static void Construct(MainAspect mainAspect) =>
            _mainAspect = mainAspect ?? throw new ArgumentNullException(nameof(mainAspect));

        public static void Replace<T>(this ProtoEntity entity, T value) 
            where T : struct
        {
            ProtoPool<T> pool = GetPool<T>();
            ref T currentValue = ref pool.Get(entity);
            currentValue = value;
        }
        
        public static bool Has<T>(this ProtoEntity entity) 
            where T : struct
        {
            ProtoPool<T> pool = GetPool<T>();
            return pool.Has(entity);
        }
        
        public static ref T Get<T>(this ProtoEntity entity) 
            where T : struct
        {
            ProtoPool<T> pool = GetPool<T>();
            return ref pool.Get(entity);
        }
        
        public static void Add<T>(this ProtoEntity entity, T value)
            where T : struct
        {
            ProtoPool<T> pool = GetPool<T>();
            ref T valueRef = ref pool.Add(entity);
            valueRef = value;
        }    
        
        public static void Add<T>(this ProtoEntity entity)
            where T : struct
        {
            ProtoPool<T> pool = GetPool<T>();
            pool.Add(entity);
        }
        
        private static ProtoPool<T> GetPool<T>() 
            where T : struct
        {
            if (_mainAspect.Pools.TryGetValue(typeof(ProtoPool<T>), out IProtoPool pool) == false)
                throw new KeyNotFoundException();
        
            if (pool is not ProtoPool<T> concretePool)
                throw new InvalidCastException();
            
            return concretePool;
        }
    }
}