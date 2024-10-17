using System;
using System.Reflection;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Core
{
    public class AspectInitializeSystem : IProtoInitSystem
    {
        [DI] private readonly MainAspect _mainAspect;
        
        public void Init(IProtoSystems systems)
        {
            FieldInfo[] fields = typeof(MainAspect).GetFields(BindingFlags.Public | BindingFlags.Instance);
            
            foreach (var fieldInfo in fields)
            {
                object value = fieldInfo.GetValue(_mainAspect);
                Type fieldType = fieldInfo.FieldType;
                
                if (value is IProtoPool)
                {
                    _mainAspect.Pools.Add(value.GetType(), value as IProtoPool);
                    Debug.Log($"Added pool: {fieldType}, ");
                }
            }
            
            AspectExt.Construct(_mainAspect);
        }
    }
}