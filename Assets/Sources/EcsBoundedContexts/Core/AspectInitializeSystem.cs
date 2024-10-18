using System.Reflection;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

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
                
                if (value is IProtoPool pool)
                    _mainAspect.Pools.Add(value.GetType(), pool);
            }
            
            AspectExt.Construct(_mainAspect);
        }
    }
}