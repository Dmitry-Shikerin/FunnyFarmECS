using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Movements.Domain;
using Sources.EcsBoundedContexts.Timers.Domain;
using UnityEngine;

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
        
        //Components
        //Timer
        public static bool HasTimer(this ProtoEntity entity) =>
            _mainAspect.Timer.Has(entity);
        
        public static ref TimerComponent GetTimer(this ProtoEntity entity) =>
            ref _mainAspect.Timer.Get(entity);
        
        public static ref TimerComponent AddTimer(this ProtoEntity entity, float value)
        {
            ref TimerComponent timer = ref _mainAspect.Timer.Add(entity);
            timer.Value = value;
            return ref timer;
        }

        public static void ReplaceTimer(this ProtoEntity entity, float value)
        {
            ref TimerComponent timer = ref _mainAspect.Timer.Get(entity);
            timer.Value = value;
        }
        
        //TargetPoint
        public static bool HasTargetPoint(this ProtoEntity entity) =>
            _mainAspect.TargetPoint.Has(entity);
        
        public static ref TargetPointComponent GetTargetPoint(this ProtoEntity entity) =>
            ref _mainAspect.TargetPoint.Get(entity);
        
        public static void AddTargetPoint(this ProtoEntity entity) =>
            _mainAspect.TargetPoint.Add(entity);
        
        public static void ReplaceTargetPoint(this ProtoEntity entity, Vector3 value)
        {
            ref TargetPointComponent targetPoint = ref _mainAspect.TargetPoint.Get(entity);
            targetPoint.Value = value;
        }
        
        public static void DelTargetPoint(this ProtoEntity entity) =>
            _mainAspect.TargetPoint.Del(entity);
    }
}